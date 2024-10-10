
using MauiPets.Mvvm.ViewModels.Pets;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetsPage : ContentPage
{
    public PetsPage(PetViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is PetViewModel viewModel)
        {
            viewModel.GetPetsCommand.Execute(null);
        }
    }


}