using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Todo;
using MauiPetsApp.Core.Application.TodoManager;

namespace MauiPets.Mvvm.ViewModels.Todo;

public partial class TodoBaseViewModel : ObservableObject
{
    [ObservableProperty]
    private int _id;
    [ObservableProperty]
    private string _description;
    [ObservableProperty]
    private string _startDate;
    [ObservableProperty]
    private string _endDate;
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


    //private DateTime _todoStartDate;
    //private DateTime _todoEndDate;


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
