namespace MauiPets.Helpers
{
    public class DatabaseHelper
    {
        public static async void CopyDatabaseIfNeeded()
        {
            await CopyFileToAppDataDirectory("PetsDB.db");

            string databaseName = "PetsDB.db";
            string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string destinationPath = Path.Combine(localAppDataPath, databaseName);

            if (!File.Exists(destinationPath))
            {
                try
                {
                    // Get the source database path
                    string sourceDatabasePath = GetSourceDatabasePath();

                    if (!string.IsNullOrEmpty(sourceDatabasePath))
                    {
                        // Copy the database to the local application data folder
                        File.Copy(sourceDatabasePath, destinationPath);
                    }
                }
                catch (Exception ex)
                {
                    // Handle the exception (e.g., log, show an error message)
                    Console.WriteLine("Error copying database: " + ex.Message);
                }
            }
        }

        private static string GetSourceDatabasePath()
        {
            // The source database path on the disk drive
            string sourceDatabasePath = Path.Combine(FileSystem.Current.AppDataDirectory, "Database", "PetsDb.db");

            if (File.Exists(sourceDatabasePath))
            {
                return sourceDatabasePath;
            }
            else
            {
                // Handle the case where the source database does not exist
                Console.WriteLine("Source database file does not exist.");
                return string.Empty;
            }
        }
        public static async Task CopyFileToAppDataDirectory(string filename)
        {
            // Open the source file
            try
            {

                using Stream inputStream = await FileSystem.Current.OpenAppPackageFileAsync(filename);

                // Create an output filename
                string targetFile = Path.Combine(FileSystem.Current.AppDataDirectory, filename);

                // Copy the file to the AppDataDirectory
                using FileStream outputStream = File.Create(targetFile);
                await inputStream.CopyToAsync(outputStream);

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error while 'CopyFileToAppDataDirectory", ex.Message, "Ok");
            }
        }
    }
}


