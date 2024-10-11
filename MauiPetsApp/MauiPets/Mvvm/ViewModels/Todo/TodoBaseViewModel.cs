using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.TodoManager;

namespace MauiPets.Mvvm.ViewModels.Todo;

public partial class TodoBaseViewModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private string _startDate = DateTime.Now.ToShortDateString();
    [ObservableProperty]
    private string _endDate = DateTime.Now.ToShortDateString();
    [ObservableProperty]
    private int _completed;
    [ObservableProperty]
    private int _generated;
    [ObservableProperty]
    private bool _pending;
    [ObservableProperty]
    private int _categoryId;
    [ObservableProperty]
    private string _categoryDescription;

    [ObservableProperty]
    ToDoDto _selectedTodo;


    [ObservableProperty]
    [NotifyPropertyChangedFor(nameof(IsNotBusy))]
    bool isBusy;
    public bool IsNotBusy => !IsBusy;

    [ObservableProperty]
    private bool isEditing;

    [ObservableProperty]
    private string _editCaption;
}
