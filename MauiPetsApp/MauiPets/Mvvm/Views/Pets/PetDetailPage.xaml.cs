using CommunityToolkit.Mvvm.DependencyInjection;
using MauiPets.Converters;
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

    protected override void OnAppearing()
    {
        base.OnAppearing();
        VaccinesView.ItemsSource = _viewModel.PetVaccinesVM;
        DewormersView.ItemsSource = _viewModel.PetDewormersVM;
        FoodView.ItemsSource = _viewModel.PetFoodVM;
        ConsultationsView.ItemsSource = _viewModel.PetConsultationsVM;
    }
}