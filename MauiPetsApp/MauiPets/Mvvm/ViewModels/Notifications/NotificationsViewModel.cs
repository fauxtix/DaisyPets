using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Core.Application.Interfaces.Repositories.Notifications;
using MauiPets.Core.Domain.Notifications;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Notifications
{
    public partial class NotificationsViewModel : ObservableObject
    {
        private readonly INotificationRepository _notificationRepository;

        public ObservableCollection<Notification> Notifications { get; } = new();

        [ObservableProperty]
        private bool isBusy;
        //[ObservableProperty]
        //private bool title;
        //[ObservableProperty]
        //private bool description;
        //[ObservableProperty]
        //private bool scheduledFor;



        public NotificationsViewModel(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;
        }

        [RelayCommand]
        public async Task LoadNotificationsAsync()
        {
            if (IsBusy) return;
            try
            {
                IsBusy = true;
                Notifications.Clear();
                var notifications = await _notificationRepository.GetAllAsync();
                foreach (var notification in notifications)
                    Notifications.Add(notification);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task MarkAsReadAsync(Notification notification)
        {
            if (notification.IsRead) return;
            await _notificationRepository.MarkAsReadAsync(notification.Id);
            await LoadNotificationsAsync();
        }
    }
}