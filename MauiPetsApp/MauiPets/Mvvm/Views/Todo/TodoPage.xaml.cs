using MauiPets.Extensions;
using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Domain.TodoManager;
using System.Collections.ObjectModel;
using System.Xml.Linq;

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

        try
        {
            if (_viewModel.Todos.Count == 0)
            {
                IsBusy = true;
                LoadAllItems();
                IsBusy = false;
            }
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error while 'LoadAllItems'...", ex.Message, "Ok");
        }

        btnNext.IsEnabled = true;
        btnPrevious.IsEnabled = false;
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();

    //    try
    //    {
    //        IsBusy = true;
    //        if (_viewModel.Todos.Count == 0)
    //        {
    //            LoadAllItems();
    //        }
    //        IsBusy = false;
    //    }
    //    catch (Exception ex)
    //    {
    //        await Shell.Current.DisplayAlert("Error while 'OnAppearing", ex.Message, "Ok");
    //    }
    //    //btnNext.IsEnabled = true;
    //    //btnPrevious.IsEnabled = false;
    //}

    private async void SearchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        SearchBar searchBar = (SearchBar)sender;
        var todosFiltered = await _service.SearchTodosByText(searchBar.Text);
        var results = new ObservableCollection<ToDoDto>(todosFiltered);
        if (results.Count > 0 && !string.IsNullOrEmpty(searchBar.Text))
        {
            _viewModel.FilterText = $"Filtered by {searchBar.Text}";
        }
        else if (results.Count == 0 && !string.IsNullOrEmpty(searchBar.Text))
        {
            _viewModel.FilterText = "No records found";
        }
        else
            _viewModel.FilterText = "All Tasks";


        //_viewModel.Todos.AddRange(results);
        todoList.ItemsSource = results;
    }


    void OnFirstPageButtonClicked(object sender, EventArgs e)
    {
        currentPage = 0;
        todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
        btnPrevious.IsEnabled = false;
        btnNext.IsEnabled = true;
    }

    void OnPreviousPageButtonClicked(object sender, EventArgs e)
    {
        if (currentPage > 0)
        {
            currentPage--;
            todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
            btnNext.IsEnabled = true;
        }
        else
        {
            btnPrevious.IsEnabled = false;
        }
    }

    void OnNextPageButtonClicked(object sender, EventArgs e)
    {
        if (currentPage < (_viewModel.Todos.Count / pageSize))
        {
            currentPage++;
            //if(currentPage * pageSize == _viewModel.Todos.Count) 
            //{
            //    btnNext.IsEnabled = false;
            //}
            //else
            {
                todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
                btnPrevious.IsEnabled = true;
            }
        }
        else
        { btnNext.IsEnabled = false; }
    }

    void OnLastPageButtonClicked(object sender, EventArgs e)
    {
        currentPage = _viewModel.Todos.Count / pageSize;
        todoList.ItemsSource = GetItems(currentPage * pageSize, pageSize);
        btnNext.IsEnabled = false;
        btnPrevious.IsEnabled = true;
    }

    private IEnumerable<object> GetItems(int startIndex, int count)
    {
        IsBusy = true;
        Task.Delay(500);
        //var result = (await _service.GetAllVMAsync()).ToList().Skip(startIndex).Take(count);
        //foreach (var item in result)
        //{
        //    _viewModel.Todos.Add(item);
        //}

        var result = _viewModel.Todos.Skip(startIndex).Take(count);
        IsBusy = false;
        return result;
    }

    private async void LoadAllItems()
    {
        await Task.Delay(500);
        var result = (await _service.GetAllVMAsync()).ToList()
            .Take(pageSize);

        foreach (var item in result)
        {
            _viewModel.Todos.Add(item);
        }

        currentPage = 0;
        //todoList.ItemsSource = result;

    }

    private async void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        // Get the Switch control that triggered the event
        var switchControl = sender as Switch;

        // Get the item associated with this Switch (which is the record from the CollectionView)
        var todoItem = switchControl.BindingContext as ToDoDto;

        if (todoItem != null)
        {
            var todoId = todoItem.Id;
            // Update the Completed property in the ViewModel item
            todoItem.Completed = e.Value ? 1 : 0;
            todoItem.Generated = e.Value ? 0 : 1;

            // Call the method to update the database record
            await _service.UpdateAsync(todoId, todoItem);
        }
    }
}