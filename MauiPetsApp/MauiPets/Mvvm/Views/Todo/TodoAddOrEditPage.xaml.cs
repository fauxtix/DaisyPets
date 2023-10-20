using MauiPets.Mvvm.ViewModels.Todo;

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
}