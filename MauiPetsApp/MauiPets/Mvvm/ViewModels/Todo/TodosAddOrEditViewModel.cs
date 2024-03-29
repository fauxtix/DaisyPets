﻿using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.ViewModels.Todo;

[QueryProperty(nameof(SelectedTodo), "SelectedTodo")]

public partial class TodosAddOrEditViewModel : TodoBaseViewModel, IQueryAttributable
{

    public IToDoService _todosService { get; set; }
    public ILookupTableService _lookupTablesService { get; set; }

    public List<LookupTableVM> TodoCategories;
    [ObservableProperty]
    private LookupTableVM _selectedContactType;

    [ObservableProperty]
    private int _selectedCategoryType;
    [ObservableProperty]
    private int _todoCategorySelectedIndex;

    [ObservableProperty]
    private string descricao;
    public int SelectedTodoId { get; set; }


    public IConnectivity _connectivity;


    public TodosAddOrEditViewModel(IToDoService todosService, ILookupTableService lookupTablesService, IConnectivity connectivity)
    {
        _todosService = todosService;
        _connectivity = connectivity;
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync($"//{nameof(TodoPage)}");
    }



    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        if (SelectedTodo is null)
        {
            TodoCategorySelectedIndex = 0;
        }

        SelectedTodo = query[nameof(SelectedTodo)] as ToDoDto;
//        TodoCategorySelectedIndex = TodoCategories.FindIndex(item => item.Id == SelectedTodo.CategoryId);
    }


    [RelayCommand]
    async Task SaveTodo()
    {
        try
        {
            //if(IsNotBusy)
            //    IsBusy = true;

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            if (SelectedTodo.Id == 0)
            {
                var insertedId = await _todosService.InsertAsync(SelectedTodo);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while updating",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var todoDto = await _todosService.GetToDoVM_ByIdAsync(insertedId);
                //IsBusy = false;

                ShowToastMessage("Contact created succesfuly");

                await Shell.Current.GoToAsync($"//{nameof(TodoPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"SelectedTodo", todoDto}
                    });


            }
            else // Insert (Id > 0)
            {
                var _todoId = SelectedTodo.Id;
                await _todosService.UpdateAsync(_todoId, SelectedTodo);

                var todoDto = await _todosService.GetToDoVM_ByIdAsync(_todoId);

                await Shell.Current.GoToAsync($"//{nameof(TodoPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"SelectedTodo", todoDto}
                    });

                //IsBusy = false;
                ShowToastMessage("Record updated successfuly");

            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            ShowToastMessage($"Error while creating Contact ({ex.Message})");
        }
    }

    private static async void ShowToastMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

}
