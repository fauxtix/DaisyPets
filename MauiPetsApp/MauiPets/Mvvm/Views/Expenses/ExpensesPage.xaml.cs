using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPetsApp.Core.Application.ViewModels.Despesas;

namespace MauiPets.Mvvm.Views.Expenses
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class ExpensesPage : ContentPage
    {
        public ExpensesPage(ExpensesViewModel viewModel)
        {
            InitializeComponent();
            BindingContext = viewModel;
        }

        protected override void OnAppearing()
        {
            base.OnAppearing();
            if (BindingContext is ExpensesViewModel viewModel)
            {
                viewModel.GetExpensesCommand.Execute(null);
            }
        }

        async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
        {
            if (args.SelectedItem is DespesaVM item)
            {
                await Shell.Current.GoToAsync($"{nameof(ExpensesAddOrEditPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"DespesaVM", item },
                        {"EdtCaption","Editar Despesa"},
                        {"IsEditing", true },
                    });
            }
        }

        private void expensesView_Scrolled(object sender, ItemsViewScrolledEventArgs e)
        {

        }
    }
}
