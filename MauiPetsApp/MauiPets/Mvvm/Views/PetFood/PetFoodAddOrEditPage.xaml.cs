using MauiPets.Mvvm.ViewModels.PetFood;

namespace MauiPets.Mvvm.Views.PetFood;

public partial class PetFoodAddOrEditPage : ContentPage
{

    public PetFoodAddOrEditPage(PetFoodAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}