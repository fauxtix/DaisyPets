using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.Views.Expenses;

//[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ExpensesAddOrEditPage : ContentPage
{
    private ExpenseAddOrEditViewModel _viewModel;
    private ITipoDespesaService _tipoDespesaService;

    public ExpensesAddOrEditPage(ExpenseAddOrEditViewModel viewModel,
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
            if (BindingContext is ExpenseAddOrEditViewModel viewModel && sender is Picker picker)
            {
                _viewModel.TipoDespesasFiltradas.Clear();

                var sit = picker.SelectedItem as LookupTableVM;

                if (_viewModel.DespesaDto is not null && sit is not null)
                {
                    _viewModel.DespesaDto.IdCategoriaDespesa = sit.Id;
                    var despesas_Categorias = (await _tipoDespesaService.GetTipoDespesa_ByCategoria(sit.Id)).ToList();
                    if (despesas_Categorias.Any())
                    {
                        _viewModel.TipoDespesas.Clear();
                        foreach (var item in despesas_Categorias)
                        {
                            _viewModel.TipoDespesasFiltradas. Add(new LookupTableVM { Id = item.Id, Descricao = item.Descricao });
                        }

                        foreach (var item in _viewModel.TipoDespesasFiltradas)
                        {
                            _viewModel.TipoDespesas.Add(item);
                        }
                        
                        _viewModel.IsEditing = true;
                    }
                    else
                    {
                        // TODO - implement 'no  ExpenseTypes for the selected Category'
                    }
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'GetLookupData", ex.Message, "Ok");
        }
    }

    private void SelectedTipoDespesaIndexChanged(object sender, EventArgs e)
    {
        try
        {

            if (BindingContext is ExpenseAddOrEditViewModel viewModel && sender is Picker picker)
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