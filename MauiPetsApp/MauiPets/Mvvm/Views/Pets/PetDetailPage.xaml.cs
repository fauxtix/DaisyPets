using MauiPets.Mvvm.ViewModels.Pets;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetDetailPage : ContentPage
{
    public PetDetailPage(PetDetailViewModel viewModel)
	{
		InitializeComponent();
		BindingContext = viewModel;
    }
}