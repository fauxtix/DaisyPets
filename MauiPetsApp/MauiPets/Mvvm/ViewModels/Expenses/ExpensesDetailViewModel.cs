using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Mvvm.ViewModels.Expenses;

[QueryProperty(nameof(DespesaVM), "DespesaVM")]

public partial class ExpensesDetailViewModel : ExpensesBaseViewModel, IQueryAttributable
{
    private readonly IDespesaService _service;
    private readonly IMapper _mapper;

    public ExpensesDetailViewModel(IDespesaService service, IMapper mapper)
    {
        _service = service;
        _mapper = mapper;
        BackButtonBehavior = new BackButtonBehavior { IsVisible = false };
    }
    public BackButtonBehavior BackButtonBehavior { get; set; }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        WorkingExpenseVM = query[nameof(WorkingExpenseVM)] as DespesaVM;
        OnPropertyChanged(nameof(WorkingExpenseVM));

    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}");
    }

    [RelayCommand]
    private async Task AddOrEditExpenseAsync()
    {
        if (WorkingExpenseVM is null)
        {
            return;
        }
        try
        {
            var _expense = await _service.GetByIdAsync(WorkingExpenseVM.Id);
            await Shell.Current.GoToAsync($"//{nameof(ExpensesAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"WorkingDto", _expense}
                });

        }
        catch (Exception ex)
        {
            var s = ex.Message;
            throw;
        }
    }

    [RelayCommand]
    private async Task DeleteDetailAsync()
    {
        if (WorkingExpenseVM is null)
        {
            return;
        }
        try
        {
            var _expense = await _service.GetVMByIdAsync(WorkingExpenseVM.Id);
            await _service.DeleteAsync(_expense.Id);
            await ShowToastMessage("Despesa apagada com sucesso");
            await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true);
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar despesa! ({ex.Message})");
            await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true);
        }
    }

    private async Task ShowToastMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }
}
