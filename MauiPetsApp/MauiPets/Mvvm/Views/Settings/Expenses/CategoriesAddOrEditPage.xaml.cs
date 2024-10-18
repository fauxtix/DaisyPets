using MauiPets.Mvvm.ViewModels.Settings;

namespace MauiPets.Mvvm.Views.Settings.Expenses;

public partial class CategoriesAddOrEditPage : ContentPage
{
    private ExpensesSettingsViewModel _viewModel;

    public CategoriesAddOrEditPage(ExpensesSettingsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}