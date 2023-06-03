using DaisyPets.Core.Application.Interfaces.DapperContext;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace DaisyPets.Infrastructure.Context
{
    public class DapperContext : IDapperContext
    {
        private readonly IConfiguration _configuration;
        private readonly string? _connectionString;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            _connectionString = _configuration.GetConnectionString("PetsConnection");
        }

        public void Execute(Action<IDbConnection> @event)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                @event(connection);
            }
        }
        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
    }
}
