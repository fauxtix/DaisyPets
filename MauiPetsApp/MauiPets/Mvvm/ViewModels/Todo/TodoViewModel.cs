using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using MauiPetsApp.Core.Domain.TodoManager;
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

                throw;
            }
        }


        [RelayCommand]
        private async Task GetTodosAsync()
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

                await Task.Delay(100);
                var todos = (await _service.GetAllVMAsync()).ToList();

                if (todos.Count != 0)
                {
                    Todos.Clear();
                }

                foreach (var todo in todos)
                {
                    Todos.Add(todo);
                }

                FilterText = "All Tasks";


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
        private async Task GetTodoAsync()
        {
            try
            {
                var todoId = Id;
                if (todoId > 0)
                {
                    var response = await _service.GetToDoVM_ByIdAsync(todoId); // await http.GetAsync(devSslHelper.DevServerRootUrl + $"/api/Pets/PetVMById/{petId}");

                    if (response is not null)
                    {
                        SelectedTodo = response;

                        //await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                        //    new Dictionary<string, object>
                        //    {
                        //        {"PetVM", Pet },
                        //    });

                    }
                }

            }
            catch (Exception ex)
            {

                throw;
            }
        }

        [RelayCommand]
        private async Task EditTodoAsync(ToDoDto todo)
        {
            try
            {
                IsEditing = true;
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
                            });
                    }
                }
            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [RelayCommand]
        private async Task FilterCompleted()
        {
            try
            {
                await GetTodosAsync();

                var completed = Todos.Where(c => c.Completed == 1).ToList();
                if (!completed.Any())
                    return;

                if (Todos.Count != 0)
                {
                    Todos.Clear();
                }

                foreach (var todo in completed)
                {
                    Todos.Add(todo);
                }
                FilterText = "Completed Tasks";

            }
            catch (Exception ex)
            {
                throw;
            }
        }

        [RelayCommand]
        private async Task FilterPending()
        {

            try
            {
                await GetTodosAsync();
                var pending = Todos.Where(c => c.Completed == 0).ToList();
                if (!pending.Any())
                    return;

                if (Todos.Count != 0)
                {
                    Todos.Clear();
                }

                foreach (var todo in pending)
                {
                    Todos.Add(todo);
                }
                FilterText = "Pending Tasks";

            }
            catch (Exception ex)
            {
                throw;
            }
        }


    }
}
