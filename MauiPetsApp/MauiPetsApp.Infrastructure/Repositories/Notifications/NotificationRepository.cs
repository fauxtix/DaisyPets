// Já tens tudo muito próximo do ideal! Aqui estão os pontos exatos a validar/ajustar:

// 1. GetAllAsync(bool includeRead = false)
// - Já filtra por Status = 0 (Ativa). Mantém assim se só quiseres mostrar ativas.
// - Se quiseres mostrar todas (ativas e lidas), remove "WHERE n.Status = 0" e filtra no código conforme necessário.

// 2. MarkAsReadAsync(int notificationId)
// - Correto: Atualiza IsRead e Status para Lida.

// 3. DeleteAsync(int notificationId)
// - Correto: Em vez de apagar, marca como Descartada.
// - (Opcional: muda o nome do método para DiscardAsync para refletir melhor a ação. O nome DELETE pode induzir em erro.)

// 4. ExistsDiscardedNotificationAsync
// - Perfeito: Verifica se já existe uma notificação descartada para aquele evento de origem.

// ----
// Fica assim:

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

            var sql = @"SELECT n.*, t.Id, t.Description 
                FROM Notification n
                INNER JOIN NotificationType t ON n.NotificationTypeId = t.Id";

            if (!showAll)
                sql += " WHERE n.Status = 0"; // Só “Ativas”
            else
                sql += " WHERE n.Status = 1"; // Só “Lidas” quando em EmptyView

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

            await connection.ExecuteAsync(
                "UPDATE Notification SET IsRead = 1, Status = @Status WHERE Id = @id",
                new { Status = (int)NotificationStatus.Lida, id = notificationId }
            );
        }

        // (Sugestão: muda nome para DiscardAsync para maior clareza)
        public async Task DiscardAsync(int notificationId)
        {
            try
            {
                using var connection = _db.CreateConnection();

                await connection.ExecuteAsync(
                    "UPDATE Notification SET Status = @Status WHERE Id = @id",
                    new { Status = (int)NotificationStatus.Descartada, id = notificationId }
                );
            }
            catch (Exception)
            {
                throw;
            }
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
    }
}