using Dapper;
using MauiPets.Core.Application.Interfaces.Repositories.Logs;
using MauiPets.Core.Application.ViewModels.Logs;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Serilog;

namespace MauiPetsApp.Infrastructure.Repositories.Logs
{
    public class LogRepository : ILogRepository
    {
        private readonly IDapperContext _context;

        public LogRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<LogEntry>> GetLogsAsync(int page, int pageSize)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var offset = (page - 1) * pageSize;
                var sql = "SELECT * FROM PetsLogs ORDER BY date(TimeStamp) DESC LIMIT @PageSize OFFSET @Offset";
                var output = await connection.QueryAsync<LogEntry>(sql, new { PageSize = pageSize, Offset = offset });
                return output;

            }
            catch (Exception ex)
            {
                Log.Error($"An error occurred: {ex.Message}");
                return Enumerable.Empty<LogEntry>();
            }
        }

        public async Task<int> GetLogCountAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT COUNT(*) FROM PetsLogs";
                var output = await connection.QuerySingleOrDefaultAsync<int>(sql);
                return output;

            }
            catch (Exception ex)
            {
                Log.Error($"GetLogCountAsync: {ex.Message}");
                return 0;
            }
        }

        public async Task<IEnumerable<LogEntry>> GetFilteredLogsAsync(string searchText)
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "SELECT * FROM PetsLogs WHERE Message LIKE @SearchText ORDER BY date(TimeStamp) DESC";

                return await connection.QueryAsync<LogEntry>(sql, new { SearchText = $"%{searchText}%" });

            }
            catch (Exception ex)
            {
                Log.Error($"GetFilteredLogsAsync: {ex.Message}");
                return Enumerable.Empty<LogEntry>();

            }
        }

        public async Task DeleteLogsAsync()
        {
            try
            {
                using var connection = _context.CreateConnection();
                var sql = "DELETE FROM PetsLogs";
                await connection.ExecuteAsync(sql);

            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao apagar Logs {ex.Message}");
                throw;
            }
        }
    }
}