using MauiPets.Extensions;
using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Domain.TodoManager;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;

namespace MauiPets.Mvvm.Views.Todo
{
    public partial class TodoPage : ContentPage
    {
        int pageSize = 6;
        int currentPage = 0;

        private TodoViewModel _viewModel;
        private IToDoService _service;

        public TodoPage(TodoViewModel viewModel, IToDoService service)
        {
            InitializeComponent();
            _viewModel = viewModel;
            BindingContext = _viewModel;
            _service = service;

            // Ensure that the items are loaded when the page appears
            LoadInitialData();
        }

        private async void LoadInitialData()
        {
            try
            {
                IsBusy = true;
                await LoadAllItems();
                UpdatePagingButtons();
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error", ex.Message, "Ok");
            }
            finally
            {
                IsBusy = false;
            }
        }

        private async Task LoadAllItems()
        {
            var allItems = (await _service.GetAllVMAsync()).ToList();
            _viewModel.Todos = new ObservableCollection<ToDoDto>(allItems);
            currentPage = 0;
            UpdateCollectionView();
        }

        private void UpdateCollectionView()
        {
            todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
            UpdatePagingButtons();
        }

        private IEnumerable<ToDoDto> GetItems(int startIndex, int count)
        {
            return _viewModel.Todos.Skip(startIndex).Take(count);
        }

        private void UpdatePagingButtons()
        {
            btnPrevious.IsEnabled = currentPage > 0;
            btnNext.IsEnabled = (currentPage + 1) * pageSize < _viewModel.Todos.Count;

            // Update page info label
            int totalPages = (int)Math.Ceiling((double)_viewModel.Todos.Count / pageSize);
            pageInfoLabel.Text = $"Page {currentPage + 1} of {totalPages}";
        }

        void OnFirstPageButtonClicked(object sender, EventArgs e)
        {
            currentPage = 0;
            UpdateCollectionView();
        }

        void OnPreviousPageButtonClicked(object sender, EventArgs e)
        {
            if (currentPage > 0)
            {
                currentPage--;
                UpdateCollectionView();
            }
        }

        void OnNextPageButtonClicked(object sender, EventArgs e)
        {
            if ((currentPage + 1) * pageSize < _viewModel.Todos.Count)
            {
                currentPage++;
                UpdateCollectionView();
            }
        }

        void OnLastPageButtonClicked(object sender, EventArgs e)
        {
            currentPage = (_viewModel.Todos.Count - 1) / pageSize;
            UpdateCollectionView();
        }

        private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
        {
            SearchBar searchBar = (SearchBar)sender;
            var todosFiltered = await _service.SearchTodosByText(searchBar.Text);
            _viewModel.Todos = new ObservableCollection<ToDoDto>(todosFiltered);
            currentPage = 0;
            UpdateCollectionView();
        }

        private async void Switch_Toggled(object sender, ToggledEventArgs e)
        {
            var switchControl = sender as Switch;
            var todoItem = switchControl?.BindingContext as ToDoDto;

            if (todoItem != null)
            {
                var todoId = todoItem.Id;
                todoItem.Completed = e.Value ? 1 : 0;
                todoItem.Generated = e.Value ? 0 : 1;

                await _service.UpdateAsync(todoId, todoItem);
            }
        }
    }
}
