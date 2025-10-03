using MauiPets.Mvvm.ViewModels.Utilities;

namespace MauiPets.Mvvm.Views.Utilities;

public partial class BackupPage : ContentPage
{
    public BackupPage(BackupViewModel vm)
    {
        InitializeComponent();
        BindingContext = vm;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is BackupViewModel vm)
        {
            vm.ClearState();
            vm.LoadLastBackupInfo();

        }
    }
}