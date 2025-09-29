using AutoMapper;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.ViewModels.Expenses;

[QueryProperty(nameof(DespesaDto), "DespesaDto")]
public partial class ExpenseAddOrEditViewModel : ExpensesBaseViewModel, IQueryAttributable
{
    private readonly IDespesaService _service;
    private readonly ITipoDespesaService _tipoDespesaService;
    private readonly ILookupTableService _lookupTablesService;
    private readonly IMapper _mapper;

    public ExpenseAddOrEditViewModel(IDespesaService service,
                                     ILookupTableService lookupTablesService,
                                     IMapper mapper,
                                     ITipoDespesaService tipoDespesaService)
    {
        _service = service;
        _lookupTablesService = lookupTablesService;
        _mapper = mapper;
        _tipoDespesaService = tipoDespesaService;

        SetupLookupTables();
    }

    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        EditCaption = query.TryGetValue(nameof(EditCaption), out var editCaptionObj) ? editCaptionObj as string : null;
        DespesaDto = query.TryGetValue(nameof(DespesaDto), out var despesaDtoObj) ? despesaDtoObj as DespesaDto : null;

        if (DespesaDto != null)
        {
            TipoCategoriaDespesaSelecionada = CategoriaDespesas.FirstOrDefault(cd => cd.Id == DespesaDto.IdCategoriaDespesa);
            await LoadTipoDespesasAsync(DespesaDto.IdCategoriaDespesa);
            TipoDespesaSelecionada = TipoDespesas.FirstOrDefault(td => td.Id == DespesaDto.IdTipoDespesa);
        }
        else
        {
            TipoCategoriaDespesaSelecionada = null;
            TipoDespesaSelecionada = null;
        }
    }

    private void SetupLookupTables()
    {
        IsBusy = true;
        GetLookupData("CategoriaDespesa");
        IsBusy = false;
    }

    public async void GetLookupData(string tableName)
    {
        IsBusy = true;
        try
        {
            var result = (await _lookupTablesService.GetLookupTableData(tableName))?.ToList();
            if (result == null)
            {
                IsBusy = false;
                return;
            }

            if (tableName.Equals("categoriadespesa", StringComparison.OrdinalIgnoreCase))
            {
                CategoriaDespesas.Clear();
                foreach (var item in result)
                    CategoriaDespesas.Add(item);
            }
            else if (tableName.Equals("tipodespesa", StringComparison.OrdinalIgnoreCase))
            {
                TipoDespesas.Clear();
                foreach (var item in result)
                    TipoDespesas.Add(item);
            }
        }
        finally
        {
            IsBusy = false;
        }
    }


    private async Task LoadTipoDespesasAsync(int categoriaId)
    {
        var despesas = await _tipoDespesaService.GetTipoDespesa_ByCategoria(categoriaId) ?? new List<TipoDespesaDto>();
        TipoDespesas.Clear();
        foreach (var item in despesas)
        {
            TipoDespesas.Add(new LookupTableVM { Id = item.Id, Descricao = item.Descricao });
        }
        IsEditing = TipoDespesas.Count > 0;
        IsBusy = false;
    }


    partial void OnTipoCategoriaDespesaSelecionadaChanged(LookupTableVM value)
    {
        if (value == null)
        {
            TipoDespesas.Clear();
            TipoDespesaSelecionada = null;
            IsEditing = false;
            return;
        }
        IsBusy = true;
        TipoDespesas.Clear();
        _ = LoadTipoDespesasAsync(value.Id);
    }

    partial void OnTipoDespesaSelecionadaChanged(LookupTableVM value)
    {
        if (DespesaDto != null && value != null)
            DespesaDto.IdTipoDespesa = value.Id;
    }

    [RelayCommand]
    async Task SaveExpenseData()
    {
        try
        {
            IsBusy = true;
            await Task.Delay(100);
            var errorMessages = _service.RegistoComErros(DespesaDto);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }

            if (DespesaDto.Id == 0)
            {
                try
                {
                    var insertedId = await _service.InsertAsync(DespesaDto);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while inserting expense",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    var expenseVM = _mapper.Map<DespesaVM>(await _service.GetVMByIdAsync(insertedId));

                    await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"DespesaVM", expenseVM}
                        });
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro ao inserir despesa", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
            else // Update existing record
            {
                try
                {
                    await Task.Delay(200);
                    await _service.UpdateAsync(DespesaDto.Id, DespesaDto);

                    await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"DespesaDto", DespesaDto}
                        });
                }
                catch (Exception ex)
                {
                    await Shell.Current.DisplayAlert("Erro ao atualizar registo", ex.Message, "OK");
                }
                finally
                {
                    IsBusy = false;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Erro na transação", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task GoBack()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}");
        }
        finally
        {
            IsBusy = false;
        }
    }
}
