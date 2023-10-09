using MauiPets.Mvvm.ViewModels.Expenses;

namespace MauiPets.Mvvm.Views.Expenses;

public partial class ExpensesAddOrEditPage : ContentPage
{
    private ExpenseAddOrEditViewModel _viewModel;

    public ExpensesAddOrEditPage(ExpenseAddOrEditViewModel viewModel)
    {
        InitializeComponent();

        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    private void SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (BindingContext is ExpenseAddOrEditViewModel viewModel && sender is Picker picker)
            {
                //var sit = picker.SelectedItem as LookupTableVM;
                //switch (picker)
                //{
                //    case var _ when picker == SpeciesPicker:
                //        _viewModel.PetDto.IdEspecie = sit.Id;
                //        break;
                //    case var _ when picker == TemperamentsPicker:
                //        _viewModel.PetDto.IdTemperamento = sit.Id;
                //        break;
                //    case var _ when picker == SituationsPicker:
                //        _viewModel.PetDto.IdSituacao = sit.Id;
                //        break;
                //    case var _ when picker == BreedsPicker:
                //        _viewModel.PetDto.IdRaca = sit.Id;
                //        break;
                //    case var _ when picker == SizesPicker:
                //        _viewModel.PetDto.IdTamanho = sit.Id;
                //        break;
                //    default:
                //        // Handle any other cases if necessary
                //        break;
                //}
            }

        }
        catch (Exception ex)
        {
            throw;
        }
    }

}