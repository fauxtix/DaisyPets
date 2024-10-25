using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Extensions;
using MauiPets.Mvvm.Views.Settings.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using Serilog;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    [QueryProperty(nameof(LookupRecordSelected), "LookupRecordSelected")]

    public partial class ExpensesSettingsViewModel : SettingsBaseViewModel, IQueryAttributable
    {
        private readonly IDespesaService _service;
        private readonly ITipoDespesaService _tipoDespesaService;
        private readonly ILookupTableService _lookupTablesService;
        private readonly IMapper _mapper;

        public ObservableCollection<TipoDespesaDto> TipoDespesas { get; set; } = new();
        public ObservableCollection<LookupTableVM> CategoriaDespesas { get; } = new();
        public List<TipoDespesaDto> TipoDespesasFiltradas { get; set; } = new();

        [ObservableProperty] public DespesaVM _despesaVM;
        [ObservableProperty] public DespesaDto _despesaDto;



        [ObservableProperty]
        private LookupTableVM _tipoDespesaSelecionada;
        [ObservableProperty]
        private LookupTableVM _tipoCategoriaDespesaSelecionada;

        [ObservableProperty]
        private int _indiceTipoDespesa;
        [ObservableProperty]
        private int _indiceCategoriaDespesa;

        [ObservableProperty]
        private bool isEditing;
        [ObservableProperty]
        private bool expand = false;

        [ObservableProperty]
        private string categoriaDespesa = string.Empty;


        public ExpensesSettingsViewModel(IDespesaService service,
            ITipoDespesaService tipoDespesaService,
            ILookupTableService lookupTablesService,
            IMapper mapper)
        {
            _service = service;
            _tipoDespesaService = tipoDespesaService;
            _lookupTablesService = lookupTablesService;
            _mapper = mapper;

            SetupLookupTables();
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            LookupRecordSelected = query[nameof(LookupRecordSelected)] as LookupTableVM;

            EditCaption = query[nameof(EditCaption)] as string;
            IsEditing = (bool)query[nameof(IsEditing)];
            TableName = (string)query[nameof(TableName)];
            Title = (string)query[nameof(Title)];
        }

        [RelayCommand]
        private async Task AddExpenseCategory()
        {
            IsEditing = false;
            EditCaption = "Nova Categoria";
            TableName = "CategoriaDespesa";
            LookupTableVM newCategory = new()
            {
                Descricao = string.Empty,
                Tabela = "CategoriaDespesa"
            };

            await NavigateToCategoryPage(newCategory);


        }

        [RelayCommand]
        private async Task AddExpenseCategoryType()
        {
            IsEditing = false;
            EditCaption = "Novo Tipo de Categoria";
            TableName = "CategoriaDespesa";
            TipoDespesaDto newCategoryType = new()
            {
                Descricao = string.Empty,
                IdCategoriaDespesa = IdCategoriaDespesa
            };

            await NavigateToCategoryTypePage(newCategoryType);
            Expand = false;

        }

        [RelayCommand]
        async Task SaveLookupData()
        {
            try
            {
                if (LookupRecordSelected.Id == 0)
                {
                    try
                    {
                        var insertedId = await _lookupTablesService.CriaNovoRegisto(LookupRecordSelected);
                        if (insertedId == -1)
                        {
                            await Shell.Current.DisplayAlert("Error while inserting record",
                                $"Please contact administrator..", "OK");
                            return;
                        }

                        ShowToastMessage("Registo criado com sucesso");
                        await Shell.Current.GoToAsync("..", true);



                        //LookupTableVM lookupTableVM = new LookupTableVM();
                        //                        await Shell.Current.GoToAsync($"{nameof(ExpenseSettingsPage)}", true, new Dictionary<string, object>
                        //                {
                        //                    { "LookupRecordSelected", lookupTableVM },
                        //                    { "Title", "" },
                        //                    { "EditCaption", "Teste"},
                        //                    { "IsEditing", false},
                        //                    { "TableName", "CategoriaDespesa" },

                        //                });

                    }
                    catch (Exception ex)
                    {
                        ShowToastMessage($"Erro ao inserir registo {ex.Message}", ToastDuration.Long);
                    }
                }
                else
                {
                    try
                    {
                        LookupRecordSelected.Tabela = TableName;
                        await _lookupTablesService.ActualizaDetalhes(LookupRecordSelected);
                        ShowToastMessage("Registo atualizado com sucesso");
                        GetLookupData(TableName);

                        await Shell.Current.GoToAsync("..", true);

                    }
                    catch (Exception ex)
                    {
                        ShowToastMessage($"Erro ao atualizar registo {ex.Message}", ToastDuration.Long);
                    }
                }
            }
            catch (Exception ex)
            {
                ShowToastMessage($"Erro na transação {ex.Message}", ToastDuration.Long);
            }
        }


        [RelayCommand]
        private async Task DeleteCategoryAsync(LookupTableVM category)
        {
            if (category == null) return;

            try
            {
                bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga registo '{category.Descricao}'?", "Sim", "Não");
                if (okToDelete)
                {
                    IsBusy = true;
                    var categoryHasChildren = await _lookupTablesService.CheckFKInUse(category.Id, "IdCategoriaDespesa", "TipoDespesa");
                    if (categoryHasChildren)
                    {
                        ShowToastMessage($"Categoria '{category.Descricao}' tem registos associados. Operação cancelada", ToastDuration.Long);
                        Log.Warning($"Tentativa de apagar Categoria '{category.Descricao}' com registos associados na tabela de Tipo de Despesas");
                        return;
                    }
                    bool categoryDeleted = await _lookupTablesService.DeleteRegisto(category.Id, "CategoriaDespesa");
                    if (categoryDeleted)
                    {
                        ShowToastMessage($"Categoria '{category.Descricao}' apagada com sucesso");
                        Log.Warning($"Apagada Categoria de Despesas '{category.Descricao}'");
                    }
                    else
                    {
                        ShowToastMessage($"Erro ao apagar Categoria '{category.Descricao}'. Verifique log, p.f. ", ToastDuration.Long);
                    }
                    LookupTableVM lookupTableVM = new LookupTableVM();
                    await Shell.Current.GoToAsync($"{nameof(ExpenseSettingsPage)}", true, new Dictionary<string, object>
                {
                    { "LookupRecordSelected", lookupTableVM },
                    { "Title", "" },
                    { "EditCaption", "Teste"},
                    { "IsEditing", false},
                    { "TableName", "CategoriaDespesa" },

                });
                }

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, ex);
            }
            finally
            {
                Expand = false;
                IsBusy = false;
            }
        }



        [RelayCommand]
        private async Task DeleteCategoryTypeAsync(TipoDespesaDto expenseType)
        {
            if (expenseType == null) return;

            bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga registo '{expenseType.Descricao}'?", "Sim", "Não");
            if (okToDelete)
            {
                IsBusy = true;
                var categoryTypeExistInExpenses = await _lookupTablesService.CheckFKInUse(expenseType.Id, "IdTipoDespesa", "Despesa");
                if (categoryTypeExistInExpenses)
                {
                    ShowToastMessage($"Tipo de Categoria '{expenseType.Descricao}' tem registos associados em Despesas. Operação cancelada", ToastDuration.Long);
                    Log.Warning($"Tentativa de apagar Tipo de Categoria '{expenseType.Descricao}' com registos associados na tabela de Despesas");
                    return;
                }

                await _tipoDespesaService.Delete(expenseType.Id);
                ShowToastMessage($"Tipo de Despesa '{expenseType.Descricao}' apagada com sucesso");
                Expand = false;


                await Shell.Current.GoToAsync("..", true);
            }
            IsBusy = false;
        }


        [RelayCommand]
        async Task GoBackToParent()
        {
            LookupTableVM lookupTableVM = new LookupTableVM();
            await Shell.Current.GoToAsync($"{nameof(ExpenseSettingsPage)}", true, new Dictionary<string, object>
                {
                    { "LookupRecordSelected", lookupTableVM },
                    { "Title", "" },
                    { "EditCaption", "Teste"},
                    { "IsEditing", false},
                    { "TableName", "CategoriaDespesa" },

                });
        }
        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"..");
        }

        private async void ShowToastMessage(string text, ToastDuration duration = ToastDuration.Short)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }



        private void SetupLookupTables()
        {
            GetLookupData("CategoriaDespesa");
        }

        [RelayCommand]
        public async Task GetTiposDespesaAsync(LookupTableVM tableVM)
        {
            Expand = false;
            CategoriaDespesa = tableVM.Descricao;
            IdCategoriaDespesa = tableVM.Id;

            var id = tableVM.Id;
            var result = (await _tipoDespesaService.GetTipoDespesa_ByCategoria(id)).ToList();
            TipoDespesas.Clear();
            TipoDespesas.AddRange(result);
            Expand = true;
        }

        public async void GetLookupData(string tableName)
        {
            try
            {
                var result = (await _lookupTablesService.GetLookupTableData(tableName)).ToList();
                if (result is null)
                {
                    return;
                }

                if (tableName.ToLower() == "categoriadespesa")
                {
                    CategoriaDespesas.Clear();
                    CategoriaDespesas.AddRange(result);
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'GetLookupData", ex.Message, "Ok");
            }
            finally
            {
            }
        }

        [RelayCommand]
        private async Task EditExpenseCategoryAsync(LookupTableVM record)
        {
            if (record?.Id > 0)
            {
                IsEditing = true;
                TableName = "CategoriaDespesa";
                EditCaption = "Editar Categoria";

                var response = await _lookupTablesService.GetRecordById(record.Id, "CategoriaDespesa");
                if (response != null)
                {
                    var mappedRecord = _mapper.Map<LookupTableVM>(response);
                    await NavigateToCategoryPage(mappedRecord);
                }
            }
        }

        [RelayCommand]
        private async Task EditExpenseCategoryTypeAsync(TipoDespesaDto categoryType)
        {
            if (categoryType?.Id > 0)
            {
                IsEditing = true;
                TableName = "TipoDespesa";
                EditCaption = "Editar Tipo de Categoria";

                var response = await _tipoDespesaService.Get_ById(categoryType.Id);
                if (response != null)
                {
                    await NavigateToCategoryTypePage(response);
                    Expand = false;
                }
            }
        }


        [RelayCommand]
        private void RefreshLookupDataAsync()
        {
            if (!string.IsNullOrEmpty(TableName))
                GetLookupData(TableName);
        }


        private async Task NavigateToCategoryPage(LookupTableVM lookupDto)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoriesAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"LookupRecordSelected", lookupDto},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },
                    {"TableName", TableName},
                    {"Title", Title},
                });
        }

        private async Task NavigateToCategoryTypePage(TipoDespesaDto dto)
        {
            await Shell.Current.GoToAsync($"{nameof(CategoryTypesAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"ExpenseTypeRecordSelected", dto},
                    {"ExpenseTypeCategoryId", IdCategoriaDespesa},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },
                    {"TableName", TableName},
                    {"Title", Title},
                });
        }


    }
}