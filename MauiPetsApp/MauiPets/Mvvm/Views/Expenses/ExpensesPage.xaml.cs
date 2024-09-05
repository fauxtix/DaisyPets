using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using Microsoft.Maui.Controls;
using System.Collections.Generic;
using System.Threading.Tasks;

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

        // Optionally, override OnAppearing if you need to refresh data when the page appears
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
