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
        private readonly IToDoService _service;
        private IConnectivity _connectivity;

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

        public ILookupTableService _lookupTablesService { get; set; }

        public TodoViewModel(IToDoService service, ILookupTableService lookupTablesService, IConnectivity connectivity)
        {
            _service = service;
            _lookupTablesService = lookupTablesService;
            _connectivity = connectivity;

            FillCategoryTypes();
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
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!", "Please check internet and try again.", "OK");
                    return;
                }

                if (IsBusy)
                    return;

                IsBusy = true;

                await Task.Delay(100);
                var todos = (await _service.GetAllVMAsync()).ToList();

                RefreshTodoList(todos, "Todas as tarefas");
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
                await GetTodosAsync();

                var completed = Todos.Where(c => c.Completed == 1).ToList();
                if (!completed.Any())
                {
                    await Shell.Current.DisplayAlert("Tarefas concluídas", "Sem dados para mostrar", "Ok");
                    return;
                }

                RefreshTodoList(completed, "Tarefas concluídas");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterCompleted", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task FilterPendingAsync()
        {
            try
            {
                await GetTodosAsync();
                var pending = Todos.Where(c => c.Completed == 0).ToList();
                if (!pending.Any())
                {
                    await Shell.Current.DisplayAlert("Tarefas pendentes", "Sem dados para mostrar", "Ok");
                    return;
                }

                RefreshTodoList(pending, "Tarefas pendentes");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterPending", ex.Message, "Ok");
            }
        }

        [RelayCommand]
        private async Task FilterThisWeekAsync()
        {
            try
            {
                await GetTodosAsync();
                var now = DateTime.Now;
                var startOfWeek = now.StartOfWeek(DayOfWeek.Sunday);
                var endOfWeek = startOfWeek.AddWeeks(1).AddDays(-1);

                var thisWeekTodos = Todos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfWeek && startDate <= endOfWeek
                ).ToList();

                if (!thisWeekTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para esta semana", "Sem dados para mostrar", "Ok");
                    return;
                }

                RefreshTodoList(thisWeekTodos, "Para esta semana");
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
                await GetTodosAsync();
                var now = DateTime.Now;
                var startOfWeek = now.StartOfWeek(DayOfWeek.Sunday).AddWeeks(1);
                var endOfWeek = startOfWeek.AddWeeks(1).AddDays(-1);

                var nextWeekTodos = Todos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfWeek && startDate <= endOfWeek
                ).ToList();

                if (!nextWeekTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para a próxima semana", "Sem dados para mostrar", "Ok");
                    return;
                }

                RefreshTodoList(nextWeekTodos, "Para a próxima semana");
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
                await GetTodosAsync();
                var now = DateTime.Now;
                var startOfMonth = now.StartOfMonth();
                var endOfMonth = startOfMonth.AddMonths(1).AddDays(-1);

                var thisMonthTodos = Todos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfMonth && startDate <= endOfMonth
                ).ToList();

                if (!thisMonthTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para este mês", "Sem dados para mostrar", "Ok");
                    return;
                }

                RefreshTodoList(thisMonthTodos, "Para este mês");
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
                await GetTodosAsync();
                var now = DateTime.Now;
                var startOfNextMonth = now.StartOfMonth().AddMonths(1);
                var endOfNextMonth = startOfNextMonth.AddMonths(1).AddDays(-1);

                var nextMonthTodos = Todos.Where(t =>
                    DateTime.TryParse(t.StartDate, out var startDate) &&
                    startDate >= startOfNextMonth && startDate <= endOfNextMonth
                ).ToList();

                if (!nextMonthTodos.Any())
                {
                    await Shell.Current.DisplayAlert("Para o próximo mês", "Sem dados para mostrar", "Ok");
                    return;
                }

                RefreshTodoList(nextMonthTodos, "Para o próximo mês");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'FilterNextMonthAsync", ex.Message, "Ok");
            }
        }

        private void RefreshTodoList(IEnumerable<ToDoDto> todos, string filterType)
        {
            Todos.Clear();
            foreach (var todo in todos)
            {
                Todos.Add(todo);
            }

            FilterText = filterType;
        }
    }
}
