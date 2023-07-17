using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.Interfaces.Repositories;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Repositories
{
    public class AppSettingsRepository : IAppSettingsRepository
    {
        private readonly IDapperContext _context;
        private readonly ILogger<AppSettingsRepository> _logger;

        public AppSettingsRepository(IDapperContext context, ILogger<AppSettingsRepository> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<String> GetLanguage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CultureName FROM AppSettings ");
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<string>(sb.ToString());
            }

        }

        public async Task SetLanguage(string cultureName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("INSERT INTO AppSettings(CultureName) VALUES (@cultureName) ");
            using (var connection = _context.CreateConnection())
            {
                await connection.ExecuteAsync(sb.ToString(), new { cultureName });
            }

        }

    }
}
