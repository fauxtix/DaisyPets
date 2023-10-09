using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Mvvm.ViewModels.Expenses;

[QueryProperty(nameof(WorkingExpenseDto), "WorkingDto")]

public partial class ExpenseAddOrEditViewModel : ExpensesBaseViewModel, IQueryAttributable
{
    private readonly IConnectivity _connectivity;

    private readonly IDespesaService _service;
    private readonly ILookupTableService _lookupTablesService;


    public ExpenseAddOrEditViewModel(IConnectivity connectivity, IDespesaService service, ILookupTableService lookupTablesService)
    {
        _connectivity = connectivity;
        _service = service;
        _lookupTablesService = lookupTablesService;

        SetupLookupTables();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {

        WorkingExpenseDto = query[nameof(WorkingExpenseDto)] as DespesaDto;

        if (WorkingExpenseDto is null)
        {
            IndiceTipoDespesa = 0;
            IndiceCategoriaDespesaSelecionada = 0;
        }
        else
        {
            IndiceTipoDespesa = TipoDespesas.FindIndex(item => item.Id == WorkingExpenseDto.IdTipoDespesa);
            IndiceCategoriaDespesaSelecionada = CategoriaDespesas.FindIndex(item => item.Id == WorkingExpenseDto.IdCategoriaDespesa);
        }
    }

    private void SetupLookupTables()
    {
        GetLookupData("CategoriaDespesa");
        GetLookupData("TipoDespesa");
    }

    private async void GetLookupData(string tableName)
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
                CategoriaDespesas.AddRange(result);
            }
            else
            {
                TipoDespesas.AddRange(result);
            }
        }
        catch (Exception ex)
        {
            //WarningMessage = ex.Message;
        }
    }

    [RelayCommand]
    async Task SaveExpenseData()
    {
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            var errorMessages = _service.RegistoComErros(WorkingExpenseDto);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }

            if (WorkingExpenseDto.Id == 0)
            {
                var insertedId = await _service.InsertAsync(WorkingExpenseDto);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while inserting expense",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var expenseDto = await _service.GetByIdAsync(insertedId);

                ShowToastMessage("Despesa criada com sucesso");

                await Shell.Current.GoToAsync($"//{nameof(ExpensesDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"WorkingDto", expenseDto}
                    });
            }
            else
            {
                await _service.UpdateAsync(WorkingExpenseDto.Id, WorkingExpenseDto);
                var expenseDto = await _service.GetByIdAsync(WorkingExpenseDto.Id);
                ShowToastMessage("Registo atualizado com sucesso");
                await Shell.Current.GoToAsync($"//{nameof(ExpensesDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"WorkingDto", expenseDto}
                    });
            }
        }
        catch (Exception ex)
        {

            throw;
        }
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
