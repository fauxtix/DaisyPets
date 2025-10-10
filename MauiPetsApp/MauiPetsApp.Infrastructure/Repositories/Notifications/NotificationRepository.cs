using Dapper;
using MauiPets.Core.Application.Interfaces.Repositories.Notifications;
using MauiPets.Core.Domain.Notifications;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;

namespace MauiPetsApp.Infrastructure.Repositories.Notifications
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly IDapperContext _db;

        public NotificationRepository(IDapperContext db)
        {
            _db = db;
        }

        public async Task<IEnumerable<Notification>> GetAllAsync(bool includeRead = false)
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT n.*, t.Id, t.Description 
                FROM Notification n
                INNER JOIN NotificationType t ON n.NotificationTypeId = t.Id";
            if (!includeRead)
                sql += " WHERE n.IsRead = 0";
            sql += " ORDER BY n.ScheduledFor ASC";

            var result = await connection.QueryAsync<Notification, NotificationType, Notification>(
                sql,
                (n, t) => { n.Type = t; return n; }
            );
            return result;
        }
        public async Task MarkAsReadAsync(int notificationId)
        {
            using var connection = _db.CreateConnection();

            await connection.ExecuteAsync("UPDATE Notification SET IsRead = 1 WHERE Id = @id", new { id = notificationId });
        }
    }
}
