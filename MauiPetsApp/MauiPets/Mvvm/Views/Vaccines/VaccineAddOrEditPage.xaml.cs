using MauiPets.Mvvm.ViewModels.Vaccines;

namespace MauiPets.Mvvm.Views.Vaccines;

public partial class VaccineAddOrEditPage : ContentPage
{
    public VaccineAddOrEditPage(VaccineAddOrEditModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}