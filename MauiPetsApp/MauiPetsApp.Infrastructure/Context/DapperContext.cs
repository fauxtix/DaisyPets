
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;

namespace MauiPetsApp.Infrastructure.Context
{
    public class DapperContext : IDapperContext
    {
        public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);
        private string? dbFile;
        private readonly string? _connectionString;
        private readonly IConfiguration _configuration;
        public DapperContext(IConfiguration configuration)
        {
            _configuration = configuration;
            var platform = _configuration["AppSettings:Platform"];
            if (platform?.ToLower() == "android")
            {
                dbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB..db");
            }
            else
            {
                dbFile = @"C:\NewProjects\DaisyPets\MauiPetsApp\MauiPets\Database\PetsDB.db"; // todo => there must be a better way...
            }

            _connectionString = $"Data Source = {dbFile}";
        }

        public void Execute(Action<IDbConnection> @event)
        {
            using (var connection = CreateConnection())
            {
                connection.Open();
                @event(connection);
            }
        }
    }
}
