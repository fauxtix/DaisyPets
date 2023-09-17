using MauiPets.Mvvm.ViewModels.Pets;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetsPage : ContentPage
{
	public PetsPage(PetViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
	}

}