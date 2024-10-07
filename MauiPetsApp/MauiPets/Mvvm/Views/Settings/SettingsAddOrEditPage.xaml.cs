using MauiPets.Mvvm.ViewModels.Settings;

namespace MauiPets.Mvvm.Views.Settings;

public partial class SettingsAddOrEditPage : ContentPage
{

    private SettingsAddOrEditViewModel _viewModel;

    public SettingsAddOrEditPage(SettingsAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}