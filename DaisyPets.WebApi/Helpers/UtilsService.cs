using Microsoft.Data.Sqlite;

namespace DaisyPets.WebApi.Helpers
{
    /// <summary>
    /// Utilitários
    /// </summary>
    public class UtilsService
    {

        /// <summary>
        /// Backup
        /// </summary>
        public void BackupDatabase(string _database)
        {
            using (var source = new SqliteConnection(_database))
            using (var destination = new SqliteConnection("Data Source=BackupDb.db"))
            {
                source.Open();
                destination.Open();
                source.BackupDatabase(destination);
            }
        }
    }
}
