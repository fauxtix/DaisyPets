
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Configuration;
using System.Data;
using System.Reflection;

namespace MauiPetsApp.Infrastructure.Context;
public class DapperContext : IDapperContext
{
    private readonly string? dbFile;
    private readonly string? _connectionString;
    private readonly IConfiguration _configuration;
    public DapperContext(IConfiguration configuration)
    {
        _configuration = configuration;
        var platform = _configuration["AppSettings:Platform"];
        if (platform?.ToLower() == "android")
        {
            dbFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB.db");
        }
        else
        {
            dbFile = @"C:\NewProjects\DaisyPets\MauiPetsApp\MauiPets\Database\PetsDB.db"; // todo => there must be a better way...
        }

        _connectionString = $"Data Source = {dbFile}";
    }

    public IDbConnection CreateConnection() => new SqliteConnection(_connectionString);

    public void Execute(Action<IDbConnection> @event)
    {
        using (var connection = CreateConnection())
        {
            connection.Open();
            @event(connection);
        }
    }
}
