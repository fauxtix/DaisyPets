using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Microsoft.Data.Sqlite;
using System.Data;

namespace MauiPetsApp.Infrastructure.Context
{
    public class DapperContext : IDapperContext
    {
        private readonly string? _connectionString;
        public DapperContext()
        {
            var dbFile =   Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB..db");
            _connectionString = $"Data Source = {dbFile}";
            //_connectionString = "Data Source = PetsDB..db";
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
