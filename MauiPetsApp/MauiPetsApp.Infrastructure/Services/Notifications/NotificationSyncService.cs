using Dapper;
using MauiPets.Core.Application.Interfaces.Services.Notifications;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Microsoft.Extensions.Logging;

namespace MauiPetsApp.Infrastructure.Services.Notifications
{
    public class NotificationsSyncService : INotificationsSyncService
    {
        private readonly IDapperContext _db;
        private readonly ILogger<NotificationsSyncService> _logger;

        public NotificationsSyncService(IDapperContext db, ILogger<NotificationsSyncService> logger)
        {
            _db = db;
            _logger = logger;
        }

        public async Task SyncNotificationsAsync()
        {
            try
            {
                DateTime hoje = DateTime.Today;
                DateTime daquiA7 = hoje.AddDays(7);

                await SyncVaccineNotificationsAsync(hoje, daquiA7);
                await SyncDewormerNotificationsAsync(hoje, daquiA7);
                await SyncTaskNotificationsAsync(hoje, daquiA7);
                // Adiciona outros handlers conforme necessário
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao sincronizar notificações");
            }
        }

        private async Task SyncVaccineNotificationsAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                const string query = @"
                    SELECT V.Id, P.Nome as Nome, V.DataToma, V.ProximaTomaEmMeses
                    FROM Vacina V
                    INNER JOIN Pet P ON V.IdPet = P.Id";
                using var connection = _db.CreateConnection();
                var items = await connection.QueryAsync(query);

                int typeId = await GetNotificationTypeIdAsync("vaccine");

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string nome = Convert.ToString(item.Nome);
                        string strDataToma = Convert.ToString(item.DataToma);
                        int proximaTomaEmMeses = item.ProximaTomaEmMeses != null ? Convert.ToInt32(item.ProximaTomaEmMeses) : 0;

                        if (!DateTime.TryParse(strDataToma, out DateTime dataToma))
                            continue;

                        DateTime dataProximaToma = dataToma.AddMonths(proximaTomaEmMeses);

                        if (dataProximaToma >= inicio && dataProximaToma <= fim)
                        {
                            await InsertNotificationIfNotExistsAsync(
                                connection,
                                $"Vacina para {nome}",
                                $"Vacina marcada para {dataProximaToma:dd/MM/yyyy}",
                                dataProximaToma,
                                typeId,
                                id
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Erro ao processar vacina Id {item?.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em SyncVaccineNotificationsAsync");
            }
        }

        private async Task SyncDewormerNotificationsAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                const string query = @"
                    SELECT D.Id, P.Nome as Nome, D.DataProximaAplicacao
                    FROM Desparasitante D
                    INNER JOIN Pet P ON D.IdPet = P.Id";
                using var connection = _db.CreateConnection();
                var items = await connection.QueryAsync(query);

                int typeId = await GetNotificationTypeIdAsync("dewormer");

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string nome = Convert.ToString(item.Nome);
                        string strDataProximaAplicacao = Convert.ToString(item.DataProximaAplicacao);

                        if (!DateTime.TryParse(strDataProximaAplicacao, out DateTime dataProximaAplicacao))
                            continue;

                        if (dataProximaAplicacao >= inicio && dataProximaAplicacao <= fim)
                        {
                            await InsertNotificationIfNotExistsAsync(
                                connection,
                                $"Desparasitante: {nome}",
                                $"Aplicação para {dataProximaAplicacao:dd/MM/yyyy}",
                                dataProximaAplicacao,
                                typeId,
                                id
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Erro ao processar desparasitante Id {item?.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em SyncDewormerNotificationsAsync");
            }
        }

        private async Task SyncTaskNotificationsAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                const string query = @"
                    SELECT Id, Description as Nome, EndDate
                    FROM Todo";
                using var connection = _db.CreateConnection();
                var items = await connection.QueryAsync(query);

                int typeId = await GetNotificationTypeIdAsync("task");

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string nome = Convert.ToString(item.Nome);
                        string strEndDate = Convert.ToString(item.EndDate);

                        if (!DateTime.TryParse(strEndDate, out DateTime endDate))
                            continue;

                        if (endDate >= inicio && endDate <= fim)
                        {
                            await InsertNotificationIfNotExistsAsync(
                                connection,
                                $"Tarefa: {nome}",
                                $"Prazo: {endDate:dd/MM/yyyy}",
                                endDate,
                                typeId,
                                id
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Erro ao processar tarefa Id {item?.Id}");
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em SyncTaskNotificationsAsync");
            }
        }

        private async Task InsertNotificationIfNotExistsAsync(
            System.Data.IDbConnection connection,
            string title,
            string desc,
            DateTime scheduledFor,
            int typeId,
            int relatedItemId)
        {
            try
            {
                var exists = await connection.QueryFirstOrDefaultAsync<int>(
                    "SELECT 1 FROM Notification WHERE RelatedItemId = @relatedItemId AND NotificationTypeId = @typeId AND ScheduledFor = @scheduledFor",
                    new { relatedItemId, typeId, scheduledFor }
                );

                if (exists == 0)
                {
                    await connection.ExecuteAsync(
                        @"INSERT INTO Notification (Title, Description, ScheduledFor, NotificationTypeId, IsRead, RelatedItemId)
                          VALUES (@Title, @Description, @ScheduledFor, @NotificationTypeId, @IsRead, @RelatedItemId)",
                        new
                        {
                            Title = title,
                            Description = desc,
                            ScheduledFor = scheduledFor,
                            NotificationTypeId = typeId,
                            RelatedItemId = relatedItemId,
                            IsRead = false
                        }
                    );
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao inserir notificação para item {relatedItemId} ({title})");
            }
        }

        private async Task<int> GetNotificationTypeIdAsync(string description)
        {
            try
            {
                using var connection = _db.CreateConnection();
                return await connection.QuerySingleAsync<int>(
                    "SELECT Id FROM NotificationType WHERE Description = @desc",
                    new { desc = description });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao obter NotificationTypeId para '{description}'");
                return -1;
            }
        }

        public async Task<int> GetActiveNotificationsCountAsync()
        {
            try
            {
                using var connection = _db.CreateConnection();


                var now = DateTime.Now.Date;
                var outout = await connection.QuerySingleAsync<int>(
                    "SELECT COUNT(*) FROM Notification WHERE ScheduledFor >= @now AND IsRead = 0",
                    new { now });
                return outout;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao contar notificações ativas");
                return -1;
            }
        }
    }
}