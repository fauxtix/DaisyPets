using MauiPets.Mvvm.ViewModels.Expenses;

namespace MauiPets.Mvvm.Views.Expenses;

//[XamlCompilation(XamlCompilationOptions.Compile)]
public partial class ExpensesAddOrEditPage : ContentPage
{
    private ExpenseAddOrEditViewModel _viewModel;

    public ExpensesAddOrEditPage(ExpenseAddOrEditViewModel viewModel)
    {

        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

}