using MauiPets.Core.Domain.Notifications;

namespace MauiPets.Core.Application.Interfaces.Repositories.Notifications
{
    public interface INotificationRepository
    {
        Task<IEnumerable<Notification>> GetAllAsync();
        Task MarkAsReadAsync(int notificationId);
    }
}
