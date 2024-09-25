using MauiPets.Core.Application.ViewModels.Logs;

namespace MauiPets.Core.Application.Interfaces.Repositories.Logs
{
    public interface ILogRepository
    {
        Task<IEnumerable<LogEntry>> GetLogsAsync(int page, int pageSize);
        Task<int> GetLogCountAsync();
        Task<IEnumerable<LogEntry>> GetFilteredLogsAsync(string searchText);
        Task DeleteLogsAsync();
    }
}
