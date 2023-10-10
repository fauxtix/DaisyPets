using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.Expenses
{

    public partial class ExpensesViewModel : ExpensesBaseViewModel
    {
        private readonly IDespesaService _service;
        private IConnectivity _connectivity;
        public ObservableCollection<DespesaVM> Expenses { get; set; } = new();


        public ExpensesViewModel(IDespesaService service, IConnectivity connectivity)
        {
            _service = service;
            _connectivity = connectivity;
        }

        [RelayCommand]
        private async Task GetExpensesAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }
                if (IsBusy)
                    return;

                IsBusy = true;
                if (Expenses.Count > 0)
                {
                    Expenses.Clear();
                }
                var expenses = (await _service.GetAllVMAsync()).ToList();


                foreach (var expense in expenses)
                {
                    Expenses.Add(expense);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get expenses: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task EditExpenseAsync(DespesaVM expense)
        {
            var expenseId = expense.Id;
            if (expenseId > 0)
            {
                var response = await _service.GetByIdAsync(expenseId);

                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(ExpensesAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"DespesaDto", response},
                        });

                }
            }
        }
    }
}
