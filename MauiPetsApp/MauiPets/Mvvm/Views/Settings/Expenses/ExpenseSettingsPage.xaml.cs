
using MauiPets.Mvvm.ViewModels.Settings;
using MauiPetsApp.Core.Application.Interfaces.Services;

namespace MauiPets.Mvvm.Views.Settings.Expenses;

public partial class ExpenseSettingsPage : ContentPage
{
    ExpensesSettingsViewModel _viewModel;
    private readonly ITipoDespesaService _tipoDespesaService;

    public ExpenseSettingsPage(ExpensesSettingsViewModel viewModel,
        ITipoDespesaService tipoDespesaService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _tipoDespesaService = tipoDespesaService;
        BindingContext = _viewModel;
    }

    protected override void OnAppearing()
    {
        if (BindingContext is ExpensesSettingsViewModel viewModel)
        {
            base.OnAppearing();
            _viewModel.RefreshLookupDataAsyncCommand.Execute(null);
        }
    }

}
