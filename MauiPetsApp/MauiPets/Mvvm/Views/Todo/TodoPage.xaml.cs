using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;

namespace MauiPets.Mvvm.Views.Todo;

public partial class TodoPage : ContentPage
{
    private TodoViewModel _viewModel;

    public TodoPage(TodoViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = viewModel;
    }

    //protected override void OnAppearing()
    //{
    //    try
    //    {
    //        base.OnAppearing();
    //        //todoList.ItemsSource = _viewModel.Todos;

    //    }
    //    catch (Exception ex)
    //    {
    //        throw;
    //    }
    //}

}