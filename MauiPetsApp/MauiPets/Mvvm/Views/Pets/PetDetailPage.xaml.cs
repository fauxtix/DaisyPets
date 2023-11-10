using MauiPets.Mvvm.ViewModels.Pets;
using Plugin.LocalNotification;
using static System.Runtime.InteropServices.JavaScript.JSType;

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

        HandleVaccinesNotifications();
    }


    protected void HandleVaccinesNotifications()
    {
        List<string> notificationMessages = new();
        var petName = "";
        foreach (var vaccine in _viewModel.PetVaccinesVM)
        {
            petName = vaccine.NomePet;
            TimeSpan difference = vaccine.DataProximaToma - DateTime.Now.Date;
            double days = difference.TotalDays;
            if (days > 0 && days < 15)
                notificationMessages.Add($"A ocorrer nos próximos 15 dias para o Pet {petName}");
            else if (days > 0 && days <= 30)
                notificationMessages.Add($"A ocorrer nos próximos 30 dias para o Pet {petName}");
        }
        if (notificationMessages.Count > 0)
        {
            foreach (var notification in notificationMessages)
            {
                SendNotification(100, $"DaisyPets - Vacinação", notification, 5);
            }
        }
    }

    protected async void SendNotification(int notificationId, string title, string description, int timeToWait)
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