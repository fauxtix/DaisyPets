using MauiPets.Core.Domain.Notifications;

namespace MauiPets.Core.Application.Interfaces.Repositories.Notifications
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync(bool includeRead = false);
        Task MarkAsReadAsync(int notificationId);
    }
}
