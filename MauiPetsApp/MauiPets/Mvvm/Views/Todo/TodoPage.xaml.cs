using MauiPets.Mvvm.ViewModels.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;

namespace MauiPets.Mvvm.Views.Todo
{
    public partial class TodoPage : ContentPage
    {
        private IToDoService _service;
        public TodoPage(TodoViewModel viewModel, IToDoService service)
        {
            InitializeComponent();
            _service = service;
            BindingContext = viewModel;        
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