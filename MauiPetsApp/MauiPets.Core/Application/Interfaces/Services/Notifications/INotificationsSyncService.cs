namespace MauiPets.Core.Application.Interfaces.Services.Notifications
{
    public interface INotificationsSyncService
    {
        Task<int> GetActiveNotificationsCountAsync();
        Task SyncNotificationsAsync();
    }
}
