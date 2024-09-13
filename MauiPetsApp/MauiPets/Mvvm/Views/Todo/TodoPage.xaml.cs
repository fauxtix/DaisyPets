using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using Syncfusion.Maui.Core.Carousel;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.Views.Todo
{
    public partial class TodoPage : ContentPage
    {
        private TodoViewModel _viewModel;
        private IToDoService _service;
        public TodoPage(TodoViewModel viewModel, IToDoService service)
        {
            InitializeComponent();
            _service = service;
            _viewModel = viewModel;
            BindingContext = _viewModel; // Bind the ViewModel to the page
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