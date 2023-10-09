using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Mvvm.Views.Expenses;

public partial class ExpensesPage : ContentPage
{
    private readonly IDespesaService _service;
    private ExpensesViewModel _viewModel;

    public ExpensesPage(IDespesaService service, ExpensesViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _service = service;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        expensesView.ItemsSource = _viewModel.ExpensesVM;
    }

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        DespesaVM item = args.SelectedItem as DespesaVM;
        await Shell.Current.GoToAsync($"{nameof(ExpensesDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"DespesaVM", item}
             });
    }
}