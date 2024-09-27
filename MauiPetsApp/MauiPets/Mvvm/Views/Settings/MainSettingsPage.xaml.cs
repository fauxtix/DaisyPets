using MauiPets.Mvvm.ViewModels.Settings;

namespace MauiPets.Mvvm.Views.Settings;

public partial class MainSettingsPage : ContentPage
{

    public MainSettingsPage(MainSettingsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}