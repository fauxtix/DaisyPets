using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Extensions;
using MauiPets.Mvvm.Views.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.Todo
{
    public partial class TodoViewModel : TodoBaseViewModel
    {
        public ObservableCollection<ToDoDto> Todos { get; set; } = new();
        private List<ToDoDto> FullTodos { get; set; } = new();
        private List<ToDoDto> FilteredTodos { get; set; } = new();
        private readonly IToDoService _service;
        public List<LookupTableVM> TodoCategories = new();
        [ObservableProperty]
        private LookupTableVM _todoCategory;

        [ObservableProperty]
        private int _selectedCategoryType;

        [ObservableProperty]
        private int _todoCategorySelectedIndex;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string filterText = string.Empty;

        [ObservableProperty]
        private int pageSize = 6;

        [ObservableProperty]
        private int currentPage = 1;

        [ObservableProperty]
        private int totalPages = 1;

        [ObservableProperty]
        private bool isPaginationVisible = false;

        [ObservableProperty]
        private string pageInfo = string.Empty;

        public ILookupTableService _lookupTablesService { get; set; }

        public TodoViewModel(IToDoService service, ILookupTableService lookupTablesService)
        {
            _service = service;
            _lookupTablesService = lookupTablesService;

            FillCategoryTypes();
            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            IsBusy = true;
            await Task.Delay(100);
            await GetTodosAsync();
            IsBusy = false;
        }

        [RelayCommand]
        private async Task AddTodoAsync()
        {
            EditCaption = "Novo registo";

            IsEditing = false;
            SelectedTodo = new()
            {
                StartDate = DateTime.Now.Date.ToShortDateString(),
                EndDate = DateTime.Now.Date.AddDays(1).ToShortDateString(),
                Description = "",
                CategoryId = 1, // dummy value
                Completed = 0,
                Generated = 1,
            };

            await Shell.Current.GoToAsync($"{nameof(TodoAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                        {"SelectedTodo", SelectedTodo},
                        {"EditCaption", EditCaption},
                        {"IsEditing", IsEditing },
                });
        }

        private void FillCategoryTypes()
        {
            try
            {
                var result = _lookupTablesService.GetLookupTableData("TodoCategories").Result;
                foreach (var categoryType in result)
                {
                    TodoCategories.Add(categoryType);
                }
            }
            catch (Exception ex)
            {
                Shell.Current.DisplayAlert("Error while 'FillCategoryTypes", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task GetTodosAsync()
        {
            try
            {
                IsBusy = true;
                await Task.Delay(200);

                var todos = (await _service.GetAllVMAsync())
                    .OrderByDescending(c => DateTime.Parse(c.StartDate.ToString()))
                    .ToList();

                FullTodos = todos;
                FilteredTodos = todos;
                TotalPages = (int)Math.Ceiling((double)todos.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;
                FilterText = "Todas as tarefas";
                CurrentPage = 1;
                UpdatePagedData(todos);
                UpdatePageInfo();
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get todos: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

        [RelayCommand]
        private async Task SearchTodosAsync(string searchText)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(searchText))
                {
                    await GetTodosAsync();
                }
                else
                {
                    var filteredTodos = FullTodos.Where(t => t.Description.Contains(searchText, StringComparison.OrdinalIgnoreCase)).ToList();
                    FilteredTodos = filteredTodos;
                    RefreshTodoList(filteredTodos);
                    FilterText = $"Filtrado por: {searchText}";
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'SearchTodosAsync", ex.Message, "Ok");
            }
        }

        private void UpdatePagedData(List<ToDoDto> todos)
        {
            var pagedTodos = todos.Skip((CurrentPage - 1) * PageSize).Take(PageSize).ToList();
            RefreshTodoList(pagedTodos);
        }

        private void RefreshTodoList(IEnumerable<ToDoDto> todos, string filterType = null)
        {
            Todos.Clear();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }

            if (filterType != null)
            {
                FilterText = filterType;
            }

            IsPaginationVisible = TotalPages > 1;
        }

        private void UpdatePageInfo()
        {
            PageInfo = $"Pagª {CurrentPage} de {TotalPages}";
        }

        private bool CanNavigatePrevious() => CurrentPage > 1;
        private bool CanNavigateNext() => CurrentPage < TotalPages;

        [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
        private void PreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                UpdatePagedData(FilteredTodos);
                UpdatePageInfo();
            }
        }

        [RelayCommand(CanExecute = nameof(CanNavigateNext))]
        private void NextPageAsync()
        {
            if (CurrentPage < TotalPages)
            {
                CurrentPage++;
                UpdatePagedData(FilteredTodos);
                UpdatePageInfo();
            }
        }

        [RelayCommand]
        private void FirstPageAsync()
        {
            CurrentPage = 1;
            UpdatePagedData(FilteredTodos);
            UpdatePageInfo();
        }

        [RelayCommand]
        private void LastPageAsync()
        {
            CurrentPage = TotalPages;
            UpdatePagedData(FilteredTodos);
            UpdatePageInfo();
        }

        [RelayCommand]
        private async Task EditTodoAsync(ToDoDto todo)
        {
            try
            {
                IsEditing = true;
                EditCaption = "Edição de registo";

                var todoId = todo.Id;
                if (todoId > 0)
                {
                    var response = await _service.FindByIdAsync(todoId);

                    if (response is not null)
                    {
                        await Shell.Current.GoToAsync($"{nameof(TodoAddOrEditPage)}", true,
                            new Dictionary<string, object>
                            {
                                {"SelectedTodo", response},
                                {"EditCaption", EditCaption},
                                {"IsEditing", IsEditing },
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'EditTodoAsync", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task FilterCompletedAsync()
        {
            try
            {
                var completed = FullTodos.Where(c => c.Completed == 1).ToList();

                if (!completed.Any())
                {
                    await Shell.Current.DisplayAlert("Tarefas concluídas", "Sem dados para mostrar", "Ok");
                    await GetTodosAsync();
                    FilterText = "Todas as tarefas";
                    FilteredTodos = FullTodos;
                    CurrentPage = 1;
                    UpdatePageInfo();
                    return;
                }

                FilteredTodos = completed;
                TotalPages = (int)Math.Ceiling((double)completed.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(completed);
                UpdatePageInfo();

                FilterText = "Tarefas concluídas";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterCompletedAsync", ex.Message, "Ok");
            }
        }
        [RelayCommand]
        private async Task FilterPendingAsync()
        {
            try
            {
                var pending = FullTodos.Where(c => c.Completed == 0).ToList();

                if (!pending.Any())
                {
                    await Shell.Current.DisplayAlert("Tarefas pendentes", "Sem dados para mostrar", "Ok");
                    await GetTodosAsync();
                    FilterText = "Todas as tarefas";
                    FilteredTodos = FullTodos;
                    CurrentPage = 1;
                    UpdatePageInfo();
                    return;
                }

                FilteredTodos = pending;
                TotalPages = (int)Math.Ceiling((double)pending.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(pending);
                UpdatePageInfo();

                FilterText = "Tarefas pendentes";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterPendingAsync", ex.Message, "Ok");
            }
        }
        [RelayCommand]
        private async Task FilterThisWeekAsync()
        {
            try
            {
                var now = DateTime.Now;
                var startOfWeek = now.StartOfWeek(DayOfWeek.Sunday);
                var endOfWeek = startOfWeek.AddWeeks(1).AddDays(-1);

                var thisWeekTodos = FullTodos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfWeek && startDate <= endOfWeek
                ).ToList();

                if (!thisWeekTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para esta semana", "Sem dados para mostrar", "Ok");
                    await GetTodosAsync();
                    FilterText = "Todas as tarefas";
                    FilteredTodos = FullTodos;
                    CurrentPage = 1;
                    UpdatePageInfo();
                    return;
                }

                FilteredTodos = thisWeekTodos;
                TotalPages = (int)Math.Ceiling((double)thisWeekTodos.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(thisWeekTodos);
                UpdatePageInfo();

                FilterText = "Para esta semana";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterThisWeekAsync", ex.Message, "Ok");
            }
        }
        [RelayCommand]
        private async Task FilterNextWeekAsync()
        {
            try
            {
                var now = DateTime.Now;
                var startOfWeek = now.StartOfWeek(DayOfWeek.Sunday).AddWeeks(1);
                var endOfWeek = startOfWeek.AddWeeks(1).AddDays(-1);

                var nextWeekTodos = FullTodos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfWeek && startDate <= endOfWeek
                ).ToList();

                if (!nextWeekTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para a próxima semana", "Sem dados para mostrar", "Ok");
                    await GetTodosAsync();
                    FilterText = "Todas as tarefas";
                    FilteredTodos = FullTodos;
                    CurrentPage = 1;
                    UpdatePageInfo();
                    return;
                }

                FilteredTodos = nextWeekTodos;
                TotalPages = (int)Math.Ceiling((double)nextWeekTodos.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(nextWeekTodos);
                UpdatePageInfo();

                FilterText = "Para a próxima semana";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterNextWeekAsync", ex.Message, "Ok");
            }
        }
        [RelayCommand]
        private async Task FilterThisMonthAsync()
        {
            try
            {
                var now = DateTime.Now;
                var startOfMonth = now.StartOfMonth();
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                var thisMonthTodos = FullTodos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfMonth && startDate <= endOfMonth
                ).ToList();

                if (!thisMonthTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para este mês", "Sem dados para mostrar", "Ok");
                    await GetTodosAsync();
                    FilterText = "Todas as tarefas";
                    FilteredTodos = FullTodos;
                    CurrentPage = 1;
                    UpdatePageInfo();
                    return;
                }

                FilteredTodos = thisMonthTodos;
                TotalPages = (int)Math.Ceiling((double)thisMonthTodos.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(thisMonthTodos);
                UpdatePageInfo();

                FilterText = "Para este mês";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterThisMonthAsync", ex.Message, "Ok");
            }
        }
        [RelayCommand]
        private async Task FilterNextMonthAsync()
        {
            try
            {
                var now = DateTime.Now;
                var startOfNextMonth = now.StartOfMonth().AddMonths(1);
                var endOfNextMonth = startOfNextMonth.AddMonths(1).AddDays(-1);

                var nextMonthTodos = FullTodos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfNextMonth && startDate <= endOfNextMonth
                ).ToList();

                if (!nextMonthTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para o próximo mês", "Sem dados para mostrar", "Ok");
                    await GetTodosAsync();
                    FilterText = "Todas as tarefas";
                    FilteredTodos = FullTodos;
                    CurrentPage = 1;
                    UpdatePageInfo();
                    return;
                }

                FilteredTodos = nextMonthTodos;
                TotalPages = (int)Math.Ceiling((double)nextMonthTodos.Count / PageSize);
                IsPaginationVisible = TotalPages > 1;

                CurrentPage = 1;
                UpdatePagedData(nextMonthTodos);
                UpdatePageInfo();

                FilterText = "Para o próximo mês";
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterNextMonthAsync", ex.Message, "Ok");
            }
        }
    }
}
