using MauiPets.Core.Domain.Notifications;

namespace MauiPets.Core.Application.Interfaces.Repositories.Notifications
{
    public interface INotificationRepository
    {
        Task DiscardAsync(int notificationId); // Renomeado (era DeleteAsync)
        Task<bool> ExistsDiscardedNotificationAsync(int notificationTypeId, int relatedItemId);
        Task<IEnumerable<Notification>> GetAllAsync(bool includeRead = false);
        Task MarkAsReadAsync(int notificationId);
        Task EnsureTablesExistAsync();
        Task PermanentDeleteAsync(int notificationId);
    }
}