using Dapper;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using System.Text;

namespace MauiPetsApp.Infrastructure
{
    public class AppSettingsRepository : IAppSettingsRepository
    {
        private readonly IDapperContext _context;

        public AppSettingsRepository(IDapperContext context)
        {
            _context = context;
        }

        public async Task<string> GetLanguage()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("SELECT CultureName FROM AppSettings ");
            using (var connection = _context.CreateConnection())
            {
                return await connection.QuerySingleOrDefaultAsync<string>(sb.ToString()) ?? "pt-PT";
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
