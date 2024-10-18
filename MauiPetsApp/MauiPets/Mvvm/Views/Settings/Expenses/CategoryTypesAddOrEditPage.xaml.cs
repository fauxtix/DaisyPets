using MauiPets.Mvvm.ViewModels.Settings;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.Views.Settings.Expenses;

public partial class CategoryTypesAddOrEditPage : ContentPage
{
    private ExpenseTypesSettingsViewModel _viewModel;
    private ITipoDespesaService _tipoDespesaService;


    public CategoryTypesAddOrEditPage(ExpenseTypesSettingsViewModel viewModel,
        ITipoDespesaService tipoDespesaService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _tipoDespesaService = tipoDespesaService;
    }

    private async void SelectedCategoriaDespesaIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is ExpenseTypesSettingsViewModel viewModel && sender is Picker picker)
            {

                var sit = picker.SelectedItem as LookupTableVM;

                if (_viewModel is not null && sit is not null)
                {
                    _viewModel.IdCategoriaDespesa = sit.Id;
                    _viewModel.IsEditing = true;
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("SelectedCategoriaDespesaIndexChanged", ex.Message, "Ok");
        }
    }

}