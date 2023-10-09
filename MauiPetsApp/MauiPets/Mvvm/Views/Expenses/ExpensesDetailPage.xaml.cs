using MauiPets.Mvvm.ViewModels.Expenses;

namespace MauiPets.Mvvm.Views.Expenses;

public partial class ExpensesDetailPage : ContentPage
{
	public ExpensesDetailPage(ExpensesDetailViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }
}