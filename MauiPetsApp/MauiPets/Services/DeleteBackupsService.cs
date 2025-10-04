namespace MauiPets.Services
{
    public static class DeleteBackupsService
    {
        public static async Task<int> DeleteAllBackupsAsync()
        {
            string downloadsPath = "/storage/emulated/0/Download";
            int deletedCount = 0;

            if (Directory.Exists(downloadsPath))
            {
                var backupFiles = Directory.GetFiles(downloadsPath, "PetsDB-backup-*.db");

                foreach (var file in backupFiles)
                {
                    try
                    {
                        File.Delete(file);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        // Opcional: log ex.Message
                    }
                }
            }

            await Shell.Current.DisplayAlert("Apagar Backups", $"{deletedCount} backups apagados.", "OK");
            return deletedCount;
        }
    }
}