using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.TodoManager;

namespace MauiPets.Mvvm.Views.Todo;

public partial class TodoAddOrEditPage : ContentPage
{

    private TodosAddOrEditViewModel _viewModel;

    public TodoAddOrEditPage(TodosAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void Switch_Toggled(object sender, ToggledEventArgs e)
    {
        var switchControl = sender as Switch;
        var todoItem = switchControl?.BindingContext as ToDoDto;

        if (todoItem != null)
        {
            var todoId = todoItem.Id;
            todoItem.Completed = e.Value ? 1 : 0;
        }
    }

}