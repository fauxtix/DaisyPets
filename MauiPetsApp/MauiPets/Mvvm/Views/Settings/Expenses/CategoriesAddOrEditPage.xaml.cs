using MauiPets.Mvvm.ViewModels.Settings;

namespace MauiPets.Mvvm.Views.Settings.Expenses;

public partial class CategoriesAddOrEditPage : ContentPage
{
    private ExpenseTypesSettingsViewModel _viewModel;

    public CategoriesAddOrEditPage(ExpenseTypesSettingsViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}