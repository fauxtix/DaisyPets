using Dapper;
using MauiPets.Core.Application.Enums;
using MauiPets.Core.Application.Interfaces.Repositories.Notifications;
using MauiPets.Core.Application.Interfaces.Services.Notifications;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Microsoft.Extensions.Logging;

namespace MauiPetsApp.Infrastructure.Services.Notifications
{
    public class NotificationsSyncService : INotificationsSyncService
    {
        private readonly IDapperContext _db;
        private readonly ILogger<NotificationsSyncService> _logger;
        private readonly INotificationRepository _notificationRepository;

        public NotificationsSyncService(IDapperContext db, ILogger<NotificationsSyncService> logger, INotificationRepository notificationRepository)
        {
            _db = db;
            _logger = logger;
            _notificationRepository = notificationRepository;
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
                await SyncVetAppointmentsNotificationsAsync(hoje, daquiA7);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao sincronizar notificações");
            }
        }

        public async Task DeleteNotificationsForRelatedItemAsync(int relatedItemId, string notificationType)
        {
            try
            {
                using var connection = _db.CreateConnection();
                int typeId = await GetNotificationTypeIdAsync(notificationType);
                await connection.ExecuteAsync(
                    "DELETE FROM Notification WHERE RelatedItemId = @relatedItemId AND NotificationTypeId = @typeId",
                    new { relatedItemId, typeId }
                );
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao apagar notificações associadas ao item");
            }
        }

        public async Task<int> GetActiveNotificationsCountAsync()
        {
            try
            {
                using var connection = _db.CreateConnection();
                var now = DateTime.Now.Date;
                var outout = await connection.QuerySingleAsync<int>(
                    "SELECT COUNT(*) FROM Notification WHERE ScheduledFor >= @now AND IsRead = 0 AND Status = 0",
                    new { now });
                return outout;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro ao contar notificações ativas");
                return -1;
            }
        }

        private async Task SyncVaccineNotificationsAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                const string query = @"
                    SELECT V.Id, P.Nome as Nome, Marca, V.DataToma, V.ProximaTomaEmMeses
                    FROM Vacina V
                    INNER JOIN Pet P ON V.IdPet = P.Id";
                using var connection = _db.CreateConnection();

                var items = (await connection.QueryAsync(query)).ToList();

                int typeId = await GetNotificationTypeIdAsync("vaccine");
                var validVaccineIds = new List<int>();

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string nome = Convert.ToString(item.Nome);
                        string marca = Convert.ToString(item.Marca);
                        string strDataToma = Convert.ToString(item.DataToma);
                        int proximaTomaEmMeses = item.ProximaTomaEmMeses != null ? Convert.ToInt32(item.ProximaTomaEmMeses) : 0;

                        if (!DateTime.TryParse(strDataToma, out DateTime dataToma))
                            continue;

                        if (await _notificationRepository.ExistsDiscardedNotificationAsync(typeId, id))
                        {
                            continue;
                        }

                        DateTime dataProximaToma = dataToma.AddMonths(proximaTomaEmMeses);

                        if (dataProximaToma >= inicio && dataProximaToma <= fim)
                        {
                            validVaccineIds.Add(id);

                            await InsertOrUpdateNotificationAsync(
                                connection,
                                $"Pet: {nome}",
                                $"Marca {marca}",
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

                if (validVaccineIds.Count > 0)
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification 
                          WHERE NotificationTypeId = @typeId 
                            AND RelatedItemId NOT IN @validVaccineIds",
                        new { typeId, validVaccineIds });
                }
                else
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification WHERE NotificationTypeId = @typeId",
                        new { typeId });
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
                    SELECT D.Id, D.Marca, P.Nome as Nome, D.DataAplicacao, D.DataProximaAplicacao
                    FROM Desparasitante D
                    INNER JOIN Pet P ON D.IdPet = P.Id";
                using var connection = _db.CreateConnection();
                var items = (await connection.QueryAsync(query)).ToList();

                int typeId = await GetNotificationTypeIdAsync("dewormer");
                var validDewormerIds = new List<int>();

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string nome = Convert.ToString(item.Nome);
                        string marca = Convert.ToString(item.Marca);
                        string strDataProximaAplicacao = Convert.ToString(item.DataProximaAplicacao);
                        string strDataAplicacao = Convert.ToString(item.DataAplicacao);

                        if (!DateTime.TryParse(strDataProximaAplicacao, out DateTime dataProximaAplicacao))
                            continue;

                        if (await _notificationRepository.ExistsDiscardedNotificationAsync(typeId, id))
                        {
                            continue;
                        }

                        if (dataProximaAplicacao >= inicio && dataProximaAplicacao <= fim)
                        {
                            validDewormerIds.Add(id);

                            await InsertOrUpdateNotificationAsync(
                                connection,
                                $"Pet: {nome}",
                                $"Marca: {marca}",
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

                if (validDewormerIds.Count > 0)
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification 
                          WHERE NotificationTypeId = @typeId 
                            AND RelatedItemId NOT IN @validDewormerIds",
                        new { typeId, validDewormerIds });
                }
                else
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification WHERE NotificationTypeId = @typeId",
                        new { typeId });
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
                    SELECT T.Id, T.Description, P.Nome as Nome, StartDate
                    FROM Todo T
                    INNER JOIN Pet P ON T.IdPet = P.Id
                    WHERE T.Completed = 0";
                using var connection = _db.CreateConnection();
                var items = (await connection.QueryAsync(query)).ToList();

                int typeId = await GetNotificationTypeIdAsync("task");
                var validTaskIds = new List<int>();

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string description = Convert.ToString(item.Description);
                        string nomePet = Convert.ToString(item.Nome);
                        string strStartDate = Convert.ToString(item.StartDate);

                        if (!DateTime.TryParse(strStartDate, out DateTime startDate))
                            continue;

                        if (await _notificationRepository.ExistsDiscardedNotificationAsync(typeId, id))
                        {
                            continue;
                        }

                        if (startDate >= inicio && startDate <= fim)
                        {
                            validTaskIds.Add(id);

                            await InsertOrUpdateNotificationAsync(
                                connection,
                                $"Pet: {nomePet}",
                                $"Tarefa: {description}",
                                startDate,
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

                if (validTaskIds.Count > 0)
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification 
                          WHERE NotificationTypeId = @typeId 
                            AND RelatedItemId NOT IN @validTaskIds",
                        new { typeId, validTaskIds });
                }
                else
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification WHERE NotificationTypeId = @typeId",
                        new { typeId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em SyncTaskNotificationsAsync");
            }
        }
        private async Task SyncVetAppointmentsNotificationsAsync(DateTime inicio, DateTime fim)
        {
            try
            {
                const string query = @"
                    SELECT C.Id, C.Motivo, P.Nome as Nome, C.DataConsulta as StartDate
                    FROM ConsultaVeterinario C Inner Join Pet P on C.IdPet = P.Id";
                using var connection = _db.CreateConnection();
                var items = (await connection.QueryAsync(query)).ToList();

                int typeId = await GetNotificationTypeIdAsync("vet_appointment");
                var validAppointmentIds = new List<int>();

                foreach (var item in items)
                {
                    try
                    {
                        int id = Convert.ToInt32(item.Id);
                        string motivo = item.Motivo;
                        string nome = Convert.ToString(item.Nome);
                        string strStartDate = Convert.ToString(item.StartDate);

                        if (!DateTime.TryParse(strStartDate, out DateTime startDate))
                            continue;

                        if (await _notificationRepository.ExistsDiscardedNotificationAsync(typeId, id))
                        {
                            continue;
                        }

                        if (startDate >= inicio && startDate <= fim)
                        {
                            validAppointmentIds.Add(id);

                            await InsertOrUpdateNotificationAsync(
                                connection,
                                $"Pet: {nome}",
                                $"Motivo: {motivo}",
                                startDate,
                                typeId,
                                id
                            );
                        }
                    }
                    catch (Exception ex)
                    {
                        _logger.LogError(ex, $"Erro ao processar consulta Id {item?.Id}");
                    }
                }

                if (validAppointmentIds.Count > 0)
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification 
                          WHERE NotificationTypeId = @typeId 
                            AND RelatedItemId NOT IN @validAppointmentIds",
                        new { typeId, validAppointmentIds });
                }
                else
                {
                    await connection.ExecuteAsync(
                        @"DELETE FROM Notification WHERE NotificationTypeId = @typeId",
                        new { typeId });
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Erro em SyncVetAppointmentsNotificationsAsync");
            }
        }

        private async Task InsertOrUpdateNotificationAsync(
            System.Data.IDbConnection connection,
            string title,
            string desc,
            DateTime scheduledFor,
            int typeId,
            int relatedItemId)
        {
            try
            {

                var existing = await connection.QueryFirstOrDefaultAsync(
                    "SELECT Id, ScheduledFor, Status, Title, Description FROM Notification WHERE RelatedItemId = @relatedItemId AND NotificationTypeId = @typeId",
                    new { relatedItemId, typeId }
                );

                if (existing == null)
                {
                    await connection.ExecuteAsync(
                        @"INSERT INTO Notification (Title, Description, ScheduledFor, NotificationTypeId, IsRead, RelatedItemId, Status)
                            VALUES (@Title, @Description, @ScheduledFor, @NotificationTypeId, @IsRead, @RelatedItemId, @Status)",
                        new
                        {
                            Title = title,
                            Description = desc,
                            ScheduledFor = scheduledFor,
                            NotificationTypeId = typeId,
                            RelatedItemId = relatedItemId,
                            IsRead = false,
                            Status = (int)NotificationStatus.Ativa
                        }
                    );
                }
                else
                {
                    int currentStatus = 0;
                    var statusObj = ((IDictionary<string, object>)existing)["Status"];
                    if (statusObj is int s) currentStatus = s;
                    else int.TryParse(statusObj?.ToString(), out currentStatus);

                    // Se estiver descartada, NÃO atualizar nem reativar
                    if (currentStatus == (int)NotificationStatus.Descartada)
                        return;

                    DateTime existingDate;
                    var scheduledForObj = ((IDictionary<string, object>)existing)["ScheduledFor"];
                    if (scheduledForObj is DateTime dt)
                        existingDate = dt;
                    else if (!DateTime.TryParse(scheduledForObj?.ToString(), out existingDate))
                        existingDate = DateTime.MinValue;

                    string? existingTitle = ((IDictionary<string, object>)existing)["Title"] as string;
                    string? existingDescription = ((IDictionary<string, object>)existing)["Description"] as string;

                    if (existingTitle != title || existingDescription != desc || existingDate != scheduledFor)
                    {
                        var idObj = ((IDictionary<string, object>)existing)["Id"];
                        int existingId = Convert.ToInt32(idObj);

                        await connection.ExecuteAsync(
                            @"UPDATE Notification 
                                  SET ScheduledFor = @ScheduledFor, Title = @Title, Description = @Description, IsRead = 0, Status = @Status
                                  WHERE Id = @Id",
                            new
                            {
                                ScheduledFor = scheduledFor,
                                Title = title,
                                Description = desc,
                                Id = existingId,
                                Status = (int)NotificationStatus.Ativa
                            }
                        );
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"Erro ao inserir/atualizar notificação para item {relatedItemId} ({title})");
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
                // 1. Verifica se já existe descartada
                var discardedExists = await connection.QueryFirstOrDefaultAsync<int>(
                    @"SELECT 1 FROM Notification 
                      WHERE RelatedItemId = @relatedItemId 
                        AND NotificationTypeId = @typeId 
                        AND ScheduledFor = @scheduledFor 
                        AND Status = @Status",
                    new { relatedItemId, typeId, scheduledFor, Status = (int)NotificationStatus.Descartada }
                );

                if (discardedExists == 1)
                    return; // NÃO criar: já foi descartada

                // 2. Verifica se já existe notificação para o mesmo RelatedItemId, TypeId e data (independentemente do status)
                var exists = await connection.QueryFirstOrDefaultAsync<int>(
                    @"SELECT 1 FROM Notification 
                      WHERE RelatedItemId = @relatedItemId 
                        AND NotificationTypeId = @typeId 
                        AND ScheduledFor = @scheduledFor",
                    new { relatedItemId, typeId, scheduledFor }
                );

                if (exists == 0)
                {
                    await connection.ExecuteAsync(
                        @"INSERT INTO Notification (Title, Description, ScheduledFor, NotificationTypeId, IsRead, RelatedItemId, Status)
                          VALUES (@Title, @Description, @ScheduledFor, @NotificationTypeId, @IsRead, @RelatedItemId, @Status)",
                        new
                        {
                            Title = title,
                            Description = desc,
                            ScheduledFor = scheduledFor,
                            NotificationTypeId = typeId,
                            RelatedItemId = relatedItemId,
                            IsRead = false,
                            Status = (int)NotificationStatus.Ativa
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
    }
}