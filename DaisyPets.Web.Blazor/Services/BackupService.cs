using Microsoft.AspNetCore.Razor.TagHelpers;
using Microsoft.Data.Sqlite;

namespace DaisyPets.Web.Blazor.Services
{
    public class BackupService
    {
        private readonly ILogger<BackupService> _logger;

        public BackupService(ILogger<BackupService> logger)
        {
            _logger = logger;
        }
        public  async Task BackupDatabase(string bkFile = "PetsDBBak.db")
        {
            try
            {
                if (File.Exists(bkFile))
                    File.Delete(bkFile);

                using (var source = new SqliteConnection($"Data Source = PetsDB.db"))
                using (var target = new SqliteConnection($"Data Source = {bkFile};"))
                {
                    await source.OpenAsync();
                    await target.OpenAsync();
                    source.BackupDatabase(target);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);

            }
        }
    }
}
