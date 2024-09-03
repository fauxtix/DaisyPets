using CommunityToolkit.Maui.Alerts;
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
        EditCaption = query[nameof(EditCaption)] as string;
        IsEditing = (bool)query[nameof(IsEditing)];
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

                //var todoDto = await _todosService.GetToDoVM_ByIdAsync(insertedId);
                //IsBusy = false;

                await ShowToastMessage("Item created succesfuly");
                await Shell.Current.GoToAsync($"//{nameof(TodoPage)}", true);
            }
            else // Insert (Id > 0)
            {
                var _todoId = SelectedTodo.Id;
                await _todosService.UpdateAsync(_todoId, SelectedTodo);
                await Shell.Current.GoToAsync($"//{nameof(TodoPage)}", true);
                await ShowToastMessage("Record updated successfuly");

            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await ShowToastMessage($"Error while creating Item ({ex.Message})");
        }
    }

    [RelayCommand]
    async Task DeleteTodo()
    {
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            if (SelectedTodo.Id > 0)
            {
                bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o registo?", "Sim", "Não");
                if (okToDelete)
                {
                    try
                    {
                        await _todosService.DeleteAsync(SelectedTodo.Id);
                        await ShowToastMessage($"Registo  apagado com sucesso");
                        await Shell.Current.GoToAsync($"//{nameof(TodoPage)}", true);

                    }
                    catch (Exception ex)
                    {
                        await ShowToastMessage($"Erro ao apagar registo ({ex.Message})");
                        await Shell.Current.GoToAsync($"//{nameof(TodoPage)}", true);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            await ShowToastMessage($"Error while creating Todo ({ex.Message})");
        }
    }


    private async Task ShowToastMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Long;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

}
