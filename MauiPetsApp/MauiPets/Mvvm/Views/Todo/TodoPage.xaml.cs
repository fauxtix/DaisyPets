using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using Microsoft.Maui.Controls;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.Views.Todo;

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
    }

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        var todosFiltered = await _service.SearchTodosByText(searchBar.Text);
        var results = new ObservableCollection<ToDoDto>(todosFiltered);
        todoList.ItemsSource = results;
        if(results.Count > 0) 
        {
            _viewModel.FilterText = $"Filtered by {searchBar.Text}";
        }
        else
        {
            _viewModel.FilterText = "No records found";
        }
    }

    protected override async void OnAppearing()
    {
        try
        {
            base.OnAppearing();
            if(_viewModel.Todos.Count == 0)
            {
                var result = (await _service.GetAllVMAsync()).ToList().Take(pageSize);
                todoList.ItemsSource = result;
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'OnAppearing", ex.Message, "Ok");
        }
    }

    void OnFirstPageButtonClicked(object sender, EventArgs e)
    {
        currentPage = 0;
        todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
    }

    void OnPreviousPageButtonClicked(object sender, EventArgs e)
    {
        if (currentPage > 0)
        {
            currentPage--;
            todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
        }
    }

    void OnNextPageButtonClicked(object sender, EventArgs e)
    {
        if (currentPage < (_viewModel.Todos.Count / pageSize))
        {
            currentPage++;
            todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
        }
    }

    void OnLastPageButtonClicked(object sender, EventArgs e)
    {
        currentPage = _viewModel.Todos.Count / pageSize;
        todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
    }

    IEnumerable<object> GetItems(int startIndex, int count)
    {
        return _viewModel.Todos.Skip(startIndex).Take(count);
    }
}