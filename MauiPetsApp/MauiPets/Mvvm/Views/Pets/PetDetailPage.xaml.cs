using MauiPets.Mvvm.ViewModels.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetDetailPage : ContentPage
{
    private IPetService _petService;
    public PetDetailPage(PetDetailViewModel viewModel, IPetService petService)
    {
        InitializeComponent();
        BindingContext = viewModel;
        _petService = petService;
    }

}