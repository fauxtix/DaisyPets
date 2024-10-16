using MauiPets.Mvvm.ViewModels.Settings;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.Views.Settings.Expenses;

public partial class ExpenseSettingsPage : ContentPage
{
    ExpensesSettingsViewModel _viewModel;
    private readonly ITipoDespesaService _tipoDespesaService;

    public ExpenseSettingsPage(ExpensesSettingsViewModel viewModel, ITipoDespesaService tipoDespesaService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        _tipoDespesaService = tipoDespesaService;
        BindingContext = _viewModel;
    }


    private void SelectedTipoDespesaIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (BindingContext is ExpensesSettingsViewModel viewModel && sender is Picker picker)
            {
                var sit = picker.SelectedItem as LookupTableVM;

                //if (PickerTipoDespesa.SelectedIndex != -1 && _viewModel.DespesaDto is not null)
                if (_viewModel.DespesaDto is not null && sit is not null)
                {
                    _viewModel.DespesaDto.IdTipoDespesa = sit.Id;
                }

            }
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error while 'SelectedTipoDespesaIndexChanged", ex.Message, "Ok");
        }
    }

}