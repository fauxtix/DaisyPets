using MauiPets.Mvvm.ViewModels.Notifications;

namespace MauiPets.Mvvm.Views.Notifications;

public partial class NotificationsPage : ContentPage
{
    public NotificationsPage(NotificationsViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is NotificationsViewModel vm)
            await vm.LoadNotificationsAsync();
    }
}