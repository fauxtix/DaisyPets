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

            //var rawSql = """
            //PRAGMA foreign_keys = ON;

            //CREATE TABLE IF NOT EXISTS TipoVacinas (
            //    Id INTEGER PRIMARY KEY AUTOINCREMENT,
            //    Id_Especie INTEGER NOT NULL,
            //    Categoria TEXT NOT NULL,
            //    Vacina TEXT NOT NULL,
            //    Prevencao TEXT NOT NULL,
            //    Notas TEXT,
            //    FOREIGN KEY (Id_Especie) REFERENCES Especie (Id)
            //);

            //INSERT INTO TipoVacinas (Id_Especie, Categoria, Vacina, Prevencao, Notas) VALUES 
            //(1, 'Essencial', 'Polivalente (L4)', 'Parvovirose, esgana, hepatite infeciosa, adenovirose, leptospirose', 'Fortemente recomendada para todos os cães.'),
            //(1, 'Essencial', 'Raiva', 'Raiva', 'Obrigatória por lei em Portugal.'),
            //(1, 'Não Essencial', 'Leishmaniose', 'Leishmaniose', 'Aconselhada para cães que vivam ou viajem para zonas de risco.'),
            //(1, 'Não Essencial', 'Tosse do Canil', 'Traqueobronquite infeciosa (tosse do canil)', 'Aconselhada para cães com contacto frequente com outros cães.');

            //INSERT INTO TipoVacinas (Id_Especie, Categoria, Vacina, Prevencao, Notas) VALUES 
            //(2, 'Essencial', 'Trivalente Felina (V3 ou V4)', 'Panleucopénia felina, herpesvírus felino (rinotraqueíte) e calicivírus felino', 'Recomendada para todos os gatos, mesmo os que não saem de casa.'),
            //(2, 'Essencial', 'Leucemia Felina (FeLV)', 'Leucemia felina', 'Essencial para gatos com acesso ao exterior.'),
            //(2, 'Essencial', 'Raiva', 'Raiva', 'Embora não obrigatória em Portugal para gatos, é considerada essencial para gatos com acesso ao exterior.'),
            //(2, 'Não Essencial', 'Clamidiose', 'Chlamydophila felis', 'Aconselhada em situações específicas.');
            //""";

            //var testResult1 = await connection.ExecuteAsync(rawSql);

            //var testResult2 = await connection.QueryAsync<TipoVacina>("SELECT * FROM TipoVacinas");

            var testResult = await connection.QueryAsync<Notification>("SELECT * FROM Notification");

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

        /// <summary>
        /// Creates NotificationType and Notification tables if they do not exist.
        /// Safe to call at startup; uses IF NOT EXISTS semantics.
        /// </summary>
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

            // Ensure foreign keys are enforced for this connection
            await connection.ExecuteAsync("PRAGMA foreign_keys = ON;");

            // Execute create statements (order matters: create NotificationType first)
            await connection.ExecuteAsync(createNotificationType);
            await connection.ExecuteAsync(createNotification);
        }
    }
}