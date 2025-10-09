using MauiPets.Mvvm.ViewModels.Pets;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetDetailPage : ContentPage
{
    private PetDetailViewModel _viewModel;
    public PetDetailPage(PetDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    //protected override async void OnAppearing()
    //{
    //    base.OnAppearing();
    //    if (BindingContext is PetAddOrEditViewModel viewModel)
    //    {
    //        await viewModel.InitializeAsync();
    //    }
    //}

}