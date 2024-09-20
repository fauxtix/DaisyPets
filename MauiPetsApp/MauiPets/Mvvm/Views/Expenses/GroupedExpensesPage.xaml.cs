using MauiPets.Mvvm.ViewModels.Expenses;

namespace MauiPets.Mvvm.Views.Expenses;

public partial class GroupedExpensesPage : ContentPage
{
    private readonly GroupedExpensesViewModel _viewModel;
    public GroupedExpensesPage(GroupedExpensesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _viewModel = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        // Carregar os dados ao aparecer a página
        _viewModel.LoadGroupedExpensesCommand.Execute(null);
    }
}