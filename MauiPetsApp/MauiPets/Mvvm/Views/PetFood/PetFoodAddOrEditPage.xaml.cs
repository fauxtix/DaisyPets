using MauiPets.Mvvm.ViewModels.PetFood;
using Syncfusion.Maui.Core.Carousel;

namespace MauiPets.Mvvm.Views.PetFood;

public partial class PetFoodAddOrEditPage : ContentPage
{
    private readonly PetFoodAddOrEditViewModel _viewModel;

    public PetFoodAddOrEditPage(PetFoodAddOrEditViewModel viewModel)
    {
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}