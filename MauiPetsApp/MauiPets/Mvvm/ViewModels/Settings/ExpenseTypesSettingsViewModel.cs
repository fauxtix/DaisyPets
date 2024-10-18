using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    [QueryProperty(nameof(ExpenseTypeRecordSelected), "ExpenseTypeRecordSelected")]

    public partial class ExpenseTypesSettingsViewModel : SettingsBaseViewModel, IQueryAttributable
    {
        public List<LookupTableVM> CategoriaDespesas { get; } = new();

        [ObservableProperty]
        private LookupTableVM _tipoCategoriaDespesaSelecionada;
        [ObservableProperty]
        private int _indiceCategoriaDespesa;

        [ObservableProperty]
        private string _descricaoCategoria;

        private readonly ITipoDespesaService _tipoDespesaService;
        private readonly ILookupTableService _lookupTablesService;
        private readonly IMapper _mapper;

        public ExpenseTypesSettingsViewModel(ITipoDespesaService tipoDespesaService,
            ILookupTableService lookupTablesService, IMapper mapper)
        {
            _tipoDespesaService = tipoDespesaService;
            _mapper = mapper;
            _lookupTablesService = lookupTablesService;

            GetCategoriesAsync();
        }

        public async void GetCategoriesAsync()
        {
            try
            {
                var result = (await _lookupTablesService.GetLookupTableData("CategoriaDespesa")).ToList();
                if (result is null)
                {
                    return;
                }

                CategoriaDespesas.Clear();
                CategoriaDespesas.AddRange(result);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'GetCategoriesAsync", ex.Message, "Ok");
            }

        }


        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ExpenseTypeRecordSelected = query[nameof(ExpenseTypeRecordSelected)] as TipoDespesaDto;
            var idxCategoria = ExpenseTypeRecordSelected.IdCategoriaDespesa;
            IndiceCategoriaDespesa = CategoriaDespesas.FindIndex(cd => cd.Id == idxCategoria);

            IdCategoriaDespesa = ExpenseTypeRecordSelected.IdCategoriaDespesa;
            DescricaoCategoria = (await _lookupTablesService.GetRecordById(IdCategoriaDespesa, "CategoriaDespesa")).Descricao;
            EditCaption = query[nameof(EditCaption)] as string;
            IsEditing = (bool)query[nameof(IsEditing)];
            TableName = (string)query[nameof(TableName)];
            Title = (string)query[nameof(Title)];
        }

        [RelayCommand]
        async Task SaveCategoryType()
        {
            try
            {
                if (ExpenseTypeRecordSelected.Id == 0)
                {
                    try
                    {
                        var insertedId = await _tipoDespesaService.Insert(ExpenseTypeRecordSelected);
                        if (insertedId == -1)
                        {
                            await Shell.Current.DisplayAlert("Error while inserting record",
                                $"Please contact administrator..", "OK");
                            return;
                        }

                        ShowToastMessage("Registo criado com sucesso");

                        await Shell.Current.GoToAsync("..", true);

                    }
                    catch (Exception ex)
                    {
                        ShowToastMessage($"Erro ao inserir registo {ex.Message}");
                    }
                }
                else
                {
                    try
                    {
                        await _tipoDespesaService.Update(IdCategoriaDespesa, ExpenseTypeRecordSelected);
                        ShowToastMessage("Registo atualizado com sucesso");
                        //GetLookupData(TableName);

                        await Shell.Current.GoToAsync("..", true);
                    }
                    catch (Exception ex)
                    {
                        ShowToastMessage($"Erro ao atualizar registo {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                ShowToastMessage($"Erro na transação {ex.Message}");
            }
        }


        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..", true);
        }



        private async void ShowToastMessage(string text)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }


    }


}
