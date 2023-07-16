using Microsoft.Data.Sqlite;

namespace DaisyPets.WebApi.Helpers
{
    /// <summary>
    /// Utilitários
    /// </summary>
    public static class UtilsService
    {

        /// <summary>
        /// Backup
        /// </summary>
        public static void BackupDatabase(string _databaseName)
        {
            using (var source = new SqliteConnection(_databaseName))
            using (var destination = new SqliteConnection("Data Source=BackupDb.db"))
            {
                source.Open();
                destination.Open();
                source.BackupDatabase(destination);
            }
        }
    }
}
