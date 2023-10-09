using CommunityToolkit.Mvvm.Input;
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

        public ObservableCollection<DespesaVM> ExpensesVM { get; set; } = new();


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
                if (ExpensesVM.Count > 0)
                {
                    ExpensesVM.Clear();
                }
                var expenses = (await _service.GetAllVMAsync()).ToList();


                foreach (var expense in expenses)
                {
                    ExpensesVM.Add(expense);
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
    }
}
