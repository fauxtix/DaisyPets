using Dapper;
using MauiPets.Core.Application.Enums;
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

        public async Task<IEnumerable<Notification>> GetAllAsync(bool showAll = false)
        {
            using var connection = _db.CreateConnection();

            var sql = @"SELECT n.Id, n.Title, n.Description, n.ScheduledFor, n.NotificationTypeId, n.IsRead, n.RelatedItemId, n.Status,
                               t.Id, t.Description
                        FROM Notification n
                        LEFT JOIN NotificationType t ON n.NotificationTypeId = t.Id";

            if (!showAll)
            {
                // pending only: active and unread
                sql += " WHERE n.Status = @Active AND n.IsRead = 0";
                sql += " ORDER BY n.ScheduledFor ASC";
            }
            else
            {
                sql += " ORDER BY n.Status ASC, n.IsRead ASC, n.ScheduledFor ASC";
            }

            var result = await connection.QueryAsync<Notification, NotificationType, Notification>(
                sql,
                (n, t) => { n.Type = t; return n; },
                new { Active = (int)NotificationStatus.Ativa },
                splitOn: "Id");

            return result;
        }

        public async Task MarkAsReadAsync(int notificationId)
        {
            using var connection = _db.CreateConnection();

            await connection.ExecuteAsync(
                "UPDATE Notification SET IsRead = 1, Status = @Status WHERE Id = @id",
                new { Status = (int)NotificationStatus.Lida, id = notificationId }
            );
        }

        public async Task DiscardAsync(int notificationId)
        {
            using var connection = _db.CreateConnection();

            await connection.ExecuteAsync(
                "UPDATE Notification SET Status = @Status WHERE Id = @id",
                new { Status = (int)NotificationStatus.Descartada, id = notificationId }
            );
        }

        public async Task<bool> ExistsDiscardedNotificationAsync(int notificationTypeId, int relatedItemId)
        {
            using var connection = _db.CreateConnection();
            var exists = await connection.QueryFirstOrDefaultAsync<int>(
                @"SELECT 1 FROM Notification 
                      WHERE NotificationTypeId = @notificationTypeId 
                        AND RelatedItemId = @relatedItemId 
                        AND Status = @Status",
                new { notificationTypeId, relatedItemId, Status = (int)NotificationStatus.Descartada });
            return exists == 1;
        }

        public async Task EnsureTablesExistAsync()
        {
            const string createNotificationType = @"
                CREATE TABLE IF NOT EXISTS ""NotificationType"" (
                    ""Id"" INTEGER PRIMARY KEY AUTOINCREMENT,
                    ""Description"" TEXT
                );";

            const string createNotification = @"
                CREATE TABLE IF NOT EXISTS ""Notification"" (
                    ""Id"" INTEGER PRIMARY KEY AUTOINCREMENT,
                    ""Title"" TEXT,
                    ""Description"" TEXT,
                    ""ScheduledFor"" DATETIME,
                    ""NotificationTypeId"" INTEGER,
                    ""IsRead"" INTEGER,
                    ""RelatedItemId"" INTEGER,
                    ""Status"" INTEGER DEFAULT 0,
                    FOREIGN KEY(""NotificationTypeId"") REFERENCES ""NotificationType""(""Id"")
                );";

            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync("PRAGMA foreign_keys = ON;");
            await connection.ExecuteAsync(createNotificationType);
            await connection.ExecuteAsync(createNotification);
        }

        public async Task PermanentDeleteAsync(int notificationId)
        {
            using var connection = _db.CreateConnection();
            await connection.ExecuteAsync(
                "DELETE FROM Notification WHERE Id = @id",
                new { id = notificationId }
            );
        }
    }
}