using MauiPets.Mvvm.ViewModels.Vaccines;

namespace MauiPets.Mvvm.Views.Vaccines;

public partial class VaccineTypesPage : ContentPage
{
    public VaccineTypesPage(TipoVacinasViewModel viewmodel)
    {
        InitializeComponent();
        BindingContext = viewmodel;

    }
}