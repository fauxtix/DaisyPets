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

        // simple filter flag used by ShowAll
        private bool _showAll = false;

        public bool HasNotifications => Notifications.Count > 0;

        public NotificationsViewModel(INotificationRepository notificationRepository)
        {
            _notificationRepository = notificationRepository;

            Notifications.CollectionChanged += (s, e) =>
            {
                OnPropertyChanged(nameof(HasNotifications));
            };
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
            if (notification == null) return;
            if (notification.IsRead) return;

            if (notification.ScheduledFor.Date > DateTime.Now.Date)
            {
                bool ok = await Shell.Current.DisplayAlert("Confirme, por favor", "Data no futuro", "Ok", "Cancelar");
                if (!ok) return;
            }

            await _notificationRepository.MarkAsReadAsync(notification.Id);
            await LoadNotificationsAsync();
            WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());
        }

        [RelayCommand]
        public async Task DiscardNotificationAsync(Notification notification)
        {
            if (notification == null) return;

            // Always perform soft-discard. do not permanently delete.
            string message = notification.IsRead
                ? "Confirma que quer descartar esta notificação?"
                : "Deseja descartar esta notificação?";

            bool okToDiscard = await Shell.Current.DisplayAlert(
                "Confirmar",
                message,
                "Ok",
                "Cancelar");

            if (!okToDiscard) return;

            await _notificationRepository.DiscardAsync(notification.Id);

            // if viewing All, reload so discarded status is visible; otherwise remove from pending view
            if (_showAll)
                await LoadNotificationsAsync();
            else
                Notifications.Remove(notification);

            WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());
        }

        // Keep a simple command to show all (used by EmptyView button).
        [RelayCommand]
        public async Task ShowAllNotificationsAsync()
        {
            if (IsBusy) return;
            _showAll = true;
            await LoadNotificationsAsync();
        }
    }
}
