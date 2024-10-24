using MauiPets.Mvvm.ViewModels.Settings;

namespace MauiPets.Mvvm.Views.Settings;

public partial class SettingsPage : ContentPage
{
    public SettingsPage(SettingsPageViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}
