using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Mvvm.Views.Expenses;

public partial class ExpensesPage : ContentPage
{

    public ExpensesPage(ExpensesViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    //protected override void OnAppearing()
    //{
    //    base.OnAppearing();
    //    //expensesView.ItemsSource = _viewModel.DespesasVM;
    //}

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        DespesaVM item = args.SelectedItem as DespesaVM;
        await Shell.Current.GoToAsync($"{nameof(ExpensesAddOrEditPage)}", true,
            new Dictionary<string, object>
            {
                {"DespesaVM", item}
             });
    }
}