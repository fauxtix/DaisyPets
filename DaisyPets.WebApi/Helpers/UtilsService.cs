using Microsoft.Data.Sqlite;

namespace DaisyPets.WebApi.Helpers
{
    /// <summary>
    /// Utilitários
    /// </summary>
    public static class UtilsService
    {
        static readonly IConfiguration? _configuration;
        static string _databaseName = string.Empty;

        /// <summary>
        /// Backup
        /// </summary>
        public static void BackupDatabase()
        {
            _databaseName = _configuration?.GetConnectionString("PetsConnection") ?? "";
            using (var source = new SqliteConnection("Data Source=ActiveDb.db; Version=3;"))
            using (var destination = new SqliteConnection("Data Source=BackupDb.db; Version=3;"))
            {
                source.Open();
                destination.Open();
                source.BackupDatabase(destination);
            }
        }

    }
}
