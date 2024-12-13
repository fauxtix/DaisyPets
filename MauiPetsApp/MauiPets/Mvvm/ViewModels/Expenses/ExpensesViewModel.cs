using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Expenses;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Expenses
{
    public partial class ExpensesViewModel : ExpensesBaseViewModel
    {
        private readonly IDespesaService _service;

        public ObservableCollection<DespesaVM> Expenses { get; } = new();

        [ObservableProperty]
        private bool _isBusy;

        [ObservableProperty]
        private int _pageSize = 7;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _totalPages = 1;

        [ObservableProperty]
        private bool _isPaginationVisible = false;

        [ObservableProperty]
        private string _pageInfo = string.Empty;

        [ObservableProperty]
        string filterText = "Despesas";

        [ObservableProperty]
        private string _searchText = string.Empty;
        public ExpensesViewModel(IDespesaService service)
        {
            _service = service;
            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            IsBusy = true;
            await Task.Delay(100);
            await GetExpensesAsync();
            await GetTotalExpensesAsync();
            IsBusy = false;
        }

        [RelayCommand]
        private async Task AddExpenseAsync()
        {
            IsEditing = false;
            EditCaption = "Nova despesa";

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
            await GetTotalExpensesAsync();

            await RefreshExpensesAsync();
        }

        [RelayCommand]
        private async Task EditExpenseAsync(DespesaVM expense)
        {
            if (expense?.Id > 0)
            {
                IsEditing = true;
                EditCaption = "Editar Despesa";

                var response = await _service.GetByIdAsync(expense.Id);
                if (response != null)
                {
                    await NavigateToEditPage(response);
                    await GetTotalExpensesAsync();
                }
            }
        }

        [RelayCommand]
        private async Task GetExpensesAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                var expenses = (await _service.GetAllVMAsync()).ToList();

                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    expenses = expenses
                        .Where(e =>
                            e.DescricaoCategoriaDespesa.Contains(SearchText, StringComparison.OrdinalIgnoreCase) ||
                            e.DescricaoTipoDespesa.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }
                else
                    FilterText = "Despesas";

                TotalGeralDespesas = expenses.Sum(c => c.ValorPago);

                TotalPages = (int)Math.Ceiling((double)expenses.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                UpdatePagedData(expenses);
                UpdatePageInfo();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task GetTotalExpensesAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(200);

                var expenses = (await _service.GetAllVMAsync()).ToList();
                TotalGeralDespesas = expenses.Sum(c => c.ValorPago);
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }


        private bool CanNavigatePrevious() => CurrentPage > 1;
        private bool CanNavigateNext() => CurrentPage < TotalPages;

        [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
        private async Task PreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await GetExpensesAsync();
            }
        }

        [RelayCommand(CanExecute = nameof(CanNavigateNext))]
        private async Task NextPageAsync()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                await GetExpensesAsync();
            }
        }

        [RelayCommand]
        private async Task FirstPageAsync()
        {
            CurrentPage = 1;
            await GetExpensesAsync();
        }

        [RelayCommand]
        private async Task LastPageAsync()
        {
            CurrentPage = TotalPages;
            await GetExpensesAsync();
        }

        private void UpdatePagedData(List<DespesaVM> expenses)
        {
            var pagedExpenses = expenses.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            RefreshExpenseList(pagedExpenses);
        }

        private async Task RefreshExpensesAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(100);
                var expenses = (await _service.GetAllVMAsync()).ToList();


                TotalPages = (int)Math.Ceiling((double)expenses.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(expenses);

                UpdatePageInfo();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
        private void RefreshExpenseList(IEnumerable<DespesaVM> expenses)
        {
            Expenses.Clear();
            foreach (var expense in expenses)
            {
                Expenses.Add(expense);
            }

            IsPaginationVisible = TotalPages > 1;
        }

        private void UpdatePageInfo()
        {
            PageInfo = TotalPages > 0 ? $"Pagª {CurrentPage} de {TotalPages}" : "";
        }

        [RelayCommand]
        private async Task FilterExpensesByYearAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                Expenses.Clear();
                var currentYear = DateTime.Now.Year;
                var expenses = (await _service.GetExpensesByYearAsync(currentYear)).ToList();
                TotalPages = (int)Math.Ceiling((double)expenses.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;
                FilterText = "Este ano";
                TotalGeralDespesas = expenses.Sum(c => c.ValorPago);


                CurrentPage = 1;
                UpdatePagedData(expenses);
                UpdatePageInfo();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task FilterExpensesByMonthAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);
                Expenses.Clear();
                var now = DateTime.Now;
                var currentYear = now.Year;
                var currentMonth = now.Month;
                var expenses = (await _service.GetExpensesByMonthAsync(currentYear, currentMonth)).ToList();
                FilterText = "Este mês";

                TotalPages = (int)Math.Ceiling((double)expenses.Count / PageSize);
                TotalGeralDespesas = expenses.Sum(c => c.ValorPago);

                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(expenses);
                UpdatePageInfo();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SearchExpensesAsync(string searchText)
        {
            SearchText = searchText;
            await GetExpensesAsync();
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

                await GetTotalExpensesAsync();

                await Shell.Current.GoToAsync($"//{nameof(ExpensesPage)}", true);
            }
            IsBusy = false;
        }
        private async Task NavigateToEditPage(DespesaDto expenseDto)
        {
            await Shell.Current.GoToAsync($"{nameof(ExpensesAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"DespesaDto", expenseDto},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },
                });
        }

        [RelayCommand]
        private async Task NavigateToGroupedExpensesAsync()
        {
            await Shell.Current.GoToAsync(nameof(GroupedExpensesPage));
        }
        private async Task ShowToastMessage(string text)
        {
            var toast = Toast.Make(text, ToastDuration.Short, 14);
            await toast.Show();
        }
    }
}
