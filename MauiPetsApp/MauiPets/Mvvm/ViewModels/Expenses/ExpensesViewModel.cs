using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using System.Collections.ObjectModel;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using MauiPets.Mvvm.Views.Expenses;

namespace MauiPets.Mvvm.ViewModels.Expenses
{
    public partial class ExpensesViewModel : ExpensesBaseViewModel
    {
        private readonly IDespesaService _service;

        public ObservableCollection<DespesaVM> Expenses { get; } = new();
        //public decimal TotalDespesas { get; set; }

        [ObservableProperty]
        private bool _isBusy;

        public ExpensesViewModel(IDespesaService service)
        {
            _service = service;
        }

        [RelayCommand]
        private async Task AddExpenseAsync()
        {
            IsEditing = false;
            DespesaDto = new()
            {
                DataCriacao = DateTime.Now.Date.ToShortDateString(),
                DataMovimento = DateTime.Now.Date.ToShortDateString(),
                ValorPago = 0M,
                Descricao = "",
                Notas = "",
                TipoMovimento = ""
            };

            await NavigateToEditPage(DespesaDto);
        }

        [RelayCommand]
        private async Task EditExpenseAsync(DespesaVM expense)
        {
            if (expense?.Id > 0)
            {
                var response = await _service.GetByIdAsync(expense.Id);
                if (response != null)
                {
                    await NavigateToEditPage(response);
                }
            }
        }

        [RelayCommand]
        private async Task GetExpensesAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            await RefreshExpensesAsync();
            IsBusy = false;
        }

        [RelayCommand]
        private async Task FilterExpensesByYearAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            Expenses.Clear();
            var currentYear = DateTime.Now.Year;
            var expenses = (await _service.GetExpensesByYearAsync(currentYear)).ToList();
            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }
            TotalDespesas = Expenses.Sum(c => c.ValorPago);
            IsBusy = false;
        }

        [RelayCommand]
        private async Task FilterExpensesByMonthAsync()
        {
            if (IsBusy) return;

            IsBusy = true;
            Expenses.Clear();
            var now = DateTime.Now;
            var currentYear = now.Year;
            var currentMonth = now.Month;
            var expenses = (await _service.GetExpensesByMonthAsync(currentYear, currentMonth)).ToList();
            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }
            TotalDespesas = Expenses.Sum(c => c.ValorPago);
            IsBusy = false;
        }

        [RelayCommand]
        private async Task DeleteExpenseAsync(DespesaVM expense)
        {
            if (expense == null) return;

            bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apagar a despesa de {expense.DataMovimento}?", "Sim", "Não");
            if (okToDelete)
            {
                IsBusy = true;
                await _service.DeleteAsync(expense.Id);
                await RefreshExpensesAsync();
                await ShowToastMessage($"Despesa de {expense.DataMovimento} apagada com sucesso");
                await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true);
            }
            IsBusy = false;
        }

        private async Task RefreshExpensesAsync()
        {
            Expenses.Clear();
            var expenses = (await _service.GetAllVMAsync()).ToList();
            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }
            TotalDespesas = Expenses.Sum(c => c.ValorPago);
        }

        private async Task NavigateToEditPage(DespesaDto expenseDto)
        {
            await Shell.Current.GoToAsync($"{nameof(ExpensesAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"DespesaDto", expenseDto}
                });
        }

        private async Task ShowToastMessage(string text)
        {
            var toast = Toast.Make(text, ToastDuration.Short, 14);
            await toast.Show();
        }
    }
}
