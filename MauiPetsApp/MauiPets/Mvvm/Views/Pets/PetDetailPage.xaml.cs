using MauiPets.Mvvm.ViewModels.Pets;
using Plugin.LocalNotification;

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

    protected override async void OnAppearing()
    {
        base.OnAppearing();
        VaccinesView.ItemsSource = _viewModel.PetVaccinesVM;
        DewormersView.ItemsSource = _viewModel.PetDewormersVM;
        FoodView.ItemsSource = _viewModel.PetFoodVM;
        ConsultationsView.ItemsSource = _viewModel.PetConsultationsVM;

        await SendNotification(100, "Mensagem para a Margarida", "Só mesmo para ver se funciona...", 5);
    }

    protected async Task SendNotification(int notificationId, string title, string description, int timeToWait)
    {
        if (await LocalNotificationCenter.Current.AreNotificationsEnabled() == false)
        {
            await LocalNotificationCenter.Current.RequestNotificationPermission();
        }

        var notification = new NotificationRequest
        {
            NotificationId = notificationId,
            Title = title,
            Description = description,
            ReturningData = "Dummy data", // Returning data when tapped on notification.
            Schedule =
    {
        NotifyTime = DateTime.Now.AddSeconds(timeToWait) // Used for Scheduling local notification, if not specified notification will show immediately.
    }
        };
        await LocalNotificationCenter.Current.Show(notification);
    }
}