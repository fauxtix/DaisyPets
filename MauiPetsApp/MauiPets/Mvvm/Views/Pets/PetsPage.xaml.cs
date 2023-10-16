using MauiPets.Mvvm.ViewModels.Pets;
using Plugin.LocalNotification;

namespace MauiPets.Mvvm.Views.Pets;

public partial class PetsPage : ContentPage
{
    private readonly INotificationService _notificationService;
    public PetsPage(PetViewModel viewModel, INotificationService notificationService)
    {
        InitializeComponent();
        _notificationService = notificationService;
        BindingContext = viewModel;
        GetMessages();
    }

    private async void GetMessages()
    {
        var deliveredNotificationList = await _notificationService.GetDeliveredNotificationList();

        //if (deliveredNotificationList.Count > 0)
        //{
        //    await DisplayAlert("Delivered Notification Count", deliveredNotificationList.Count.ToString(), "OK");
        //}

        //await Navigation.PopModalAsync();
    }

}