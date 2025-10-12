using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiPets.Core.Application.Interfaces.Repositories.Notifications;
using MauiPets.Core.Application.ViewModels.Messages;
using MauiPets.Core.Domain.Notifications;
using MauiPets.Mvvm.Views.Pets;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Notifications
{
    public partial class NotificationsViewModel : ObservableObject
    {
        private readonly INotificationRepository _notificationRepository;

        public ObservableCollection<Notification> Notifications { get; } = new();

        [ObservableProperty]
        private bool isBusy;

        private bool _showAll = false;
        public bool HasNotifications => Notifications.Count > 0;

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
                var notifications = await _notificationRepository.GetAllAsync(_showAll);
                foreach (var notification in notifications)
                    Notifications.Add(notification);
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(PetsPage)}");
        }


        [RelayCommand]
        public async Task MarkAsReadAsync(Notification notification)
        {
            if (notification.IsRead) return;

            if (notification.ScheduledFor.Date > DateTime.Now.Date)
            {
                bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", "Data no futuro", "Ok", "Cancelar");
                if (okToDelete)
                {
                    await _notificationRepository.MarkAsReadAsync(notification.Id);
                }
                else
                {
                    return;
                }
            }
            else
            {
                await _notificationRepository.MarkAsReadAsync(notification.Id);
            }

            await LoadNotificationsAsync();
            WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());
        }

        [RelayCommand]
        public async Task DiscardNotificationAsync(Notification notification)
        {
            if (notification == null) return;

            bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", "Descartar notificação", "Ok", "Cancelar");
            if (okToDelete)
            {
                await _notificationRepository.DiscardAsync(notification.Id);
                Notifications.Remove(notification);
                WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());
            }
        }

        [RelayCommand]
        public async Task ShowAllNotificationsAsync()
        {
            if (IsBusy) return;
            _showAll = true;
            await LoadNotificationsAsync();
        }
    }
}