using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Core.Application.ViewModels.Utilities;
using Microsoft.Data.Sqlite;
using Microsoft.Extensions.Logging;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Utilities
{
    public partial class BackupViewModel : ObservableObject
    {
        private readonly string dbFileName = "PetsDB.db";
        private readonly string backupFileName = "PetsDB-backup.db";
        private readonly string[] tablesToCount = {
            "Pet", "CategoriaDespesa", "ConsultaVeterinario", "Contacto", "Desparasitante", "Despesa", "Especie",
            "Esterilizacao", "Idade", "MarcaRacao", "Medicacao", "Peso", "PetsLog", "Raca", "Racao", "Situacao",
            "Tamanho", "Temperamento", "TipoContacto", "TipoDesparasitanteExterno", "TipoDesparasitanteInterno",
            "TipoDespesa", "ToDoCategories", "Todo", "Vacina"
        };

        private readonly string dbPath;

        [ObservableProperty] private DatabaseInfo currentDb;
        [ObservableProperty] private DatabaseInfo backupDb;
        [ObservableProperty] private bool showRestorePanel;

        [ObservableProperty] private ObservableCollection<KeyValuePair<string, long>> currentDbTableCounts = new();
        [ObservableProperty] private ObservableCollection<KeyValuePair<string, long>> backupDbTableCounts = new();

        [ObservableProperty] private ObservableCollection<KeyValuePair<string, long>> alteredCurrentTableCounts = new();
        [ObservableProperty] private ObservableCollection<KeyValuePair<string, long>> alteredBackupTableCounts = new();

        [ObservableProperty] private string selectedBackupName = "PetsDB-backup.db";
        [ObservableProperty] private DateTime selectedBackupDate = DateTime.MinValue;
        [ObservableProperty] private string selectedBackupPath = "";
        [ObservableProperty] private string resumoAlteracoes;

        [ObservableProperty] private bool isBusy;
        [ObservableProperty] private bool hasBackup = false;

        private readonly ILogger<BackupViewModel> _logger;
        public BackupViewModel(ILogger<BackupViewModel> logger)
        {
            _logger = logger;

            dbPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData),
                dbFileName);

            var dbInfo = GetDatabaseInfo(dbPath, tablesToCount);
            CurrentDb = dbInfo;
            CurrentDbTableCounts = new ObservableCollection<KeyValuePair<string, long>>(dbInfo.TableCounts ?? new Dictionary<string, long>());
            ShowRestorePanel = false;

            AlteredCurrentTableCounts = new();
            AlteredBackupTableCounts = new();

        }

        public void ClearState()
        {
            ShowRestorePanel = false;
            AlteredCurrentTableCounts.Clear();
            AlteredBackupTableCounts.Clear();
            CurrentDbTableCounts.Clear();
            BackupDbTableCounts.Clear();
            SelectedBackupDate = DateTime.MinValue;
            SelectedBackupPath = string.Empty;
            ResumoAlteracoes = string.Empty;
            HasBackup = File.Exists(GetBackupPath());
        }

        public void LoadLastBackupInfo()
        {
            SelectedBackupPath = Preferences.Default.Get<string>("LastBackupPath", GetBackupPath());
            SelectedBackupDate = Preferences.Default.Get<DateTime>("LastBackupDate", DateTime.MinValue);

            HasBackup = !string.IsNullOrEmpty(SelectedBackupPath) && File.Exists(SelectedBackupPath);

            if (HasBackup)
            {
                BackupDb = GetDatabaseInfo(SelectedBackupPath, tablesToCount);
                BackupDbTableCounts = new ObservableCollection<KeyValuePair<string, long>>(BackupDb.TableCounts ?? new Dictionary<string, long>());
                SelectedBackupDate = File.GetLastWriteTime(SelectedBackupPath);
            }
            else
            {
                BackupDb = null;
                BackupDbTableCounts.Clear();
                SelectedBackupDate = DateTime.MinValue;
                SelectedBackupPath = "";
            }
        }

        private string GetBackupPath()
        {
            string downloadsPath = FileSystem.Current.AppDataDirectory;
#if ANDROID
            downloadsPath = "/storage/emulated/0/Download";
#endif
#if WINDOWS
            downloadsPath = Path.Combine(
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile),
                "Downloads");
#endif
            return Path.Combine(downloadsPath, backupFileName);
        }

        private DatabaseInfo GetDatabaseInfo(string path, string[] tables)
        {
            var info = new DatabaseInfo
            {
                Path = path,
                TableCounts = new Dictionary<string, long>()
            };

            info.SizeInBytes = File.Exists(path) ? new FileInfo(path).Length : 0;
            info.LastModified = File.Exists(path) ? File.GetLastWriteTime(path) : DateTime.MinValue;
            info.IsAccessible = false;

            if (File.Exists(path))
            {
                try
                {
                    using var conn = new SqliteConnection($"Data Source={path}");
                    conn.Open();
                    info.IsAccessible = true;
                    foreach (var table in tables)
                    {
                        using var cmd = conn.CreateCommand();
                        cmd.CommandText = $"SELECT COUNT(*) FROM {table}";
                        info.TableCounts[table] = (long)cmd.ExecuteScalar();
                    }
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error accessing database");
                    info.IsAccessible = false;
                }
            }

            return info;
        }

        private void AtualizaTabelasAlteradas()
        {
            AlteredCurrentTableCounts.Clear();
            AlteredBackupTableCounts.Clear();

            var allKeys = tablesToCount;
            foreach (var key in allKeys)
            {
                long currCount = CurrentDbTableCounts.FirstOrDefault(x => x.Key == key).Value;
                long backupCount = BackupDbTableCounts.FirstOrDefault(x => x.Key == key).Value;
                if (currCount != backupCount)
                {
                    AlteredCurrentTableCounts.Add(new KeyValuePair<string, long>(key, currCount));
                    AlteredBackupTableCounts.Add(new KeyValuePair<string, long>(key, backupCount));
                }
            }

            if (BackupDb?.Path != null)
            {
                SelectedBackupDate = File.Exists(BackupDb.Path) ? File.GetLastWriteTime(BackupDb.Path) : DateTime.MinValue;
                SelectedBackupPath = BackupDb.Path;
            }
            else
            {
                SelectedBackupDate = DateTime.MinValue;
                SelectedBackupPath = "";
            }

            if (AlteredCurrentTableCounts.Count > 0)
            {
                var lines = AlteredCurrentTableCounts.Select((kvp, idx) =>
                {
                    var backup = AlteredBackupTableCounts.ElementAtOrDefault(idx);
                    if (backup.Key == kvp.Key)
                    {
                        var diff = kvp.Value - backup.Value;
                        var sign = diff > 0 ? "+" : "";
                        return $"{kvp.Key}: {backup.Value} → {kvp.Value} ({sign}{diff})";
                    }
                    else
                    {
                        return $"{kvp.Key}: {kvp.Value} (alterado)";
                    }
                });
                var alteredTables = AlteredCurrentTableCounts.Count;
                if (alteredTables == 1)
                    ResumoAlteracoes = $"Foi detetada 1 tabela alterada:\n\n" +
                    string.Join("\n\n", lines);
                else
                    ResumoAlteracoes = $"Foram detetadas {AlteredCurrentTableCounts.Count} tabelas alteradas:\n" +
                    string.Join("\n\n", lines);
            }
            else
            {
                ResumoAlteracoes = "Nenhuma tabela foi alterada.";
            }
        }

        [RelayCommand]
        public async Task BackupDatabaseAsync()
        {
            IsBusy = true;
            try
            {
                bool ok = await Shell.Current.DisplayAlert(
                    "Confirmar Backup",
                    "Deseja realmente criar um backup da base de dados?",
                    "Sim", "Não");
                if (!ok)
                    return;

                var destPath = GetBackupPath();

                if (File.Exists(destPath))
                    File.Delete(destPath);

                File.Copy(dbPath, destPath, overwrite: true);

                Preferences.Default.Set("LastBackupPath", destPath);
                Preferences.Default.Set("LastBackupDate", File.GetLastWriteTime(destPath));

                SelectedBackupPath = destPath;
                SelectedBackupDate = File.GetLastWriteTime(destPath);

                await Shell.Current.DisplayAlert("Backup", $"Backup criado em:\n{destPath}", "OK");

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        public async Task RestoreDatabaseAsync()
        {
            IsBusy = true;
            try
            {
                LoadLastBackupInfo();

                if (!HasBackup)
                {
                    await Shell.Current.DisplayAlert(
                        "Sem backup",
                        "Não existe nenhum ficheiro de backup para restaurar.",
                        "OK");
                    ShowRestorePanel = false;
                    return;
                }

                CurrentDb = GetDatabaseInfo(dbPath, tablesToCount);
                CurrentDbTableCounts = new ObservableCollection<KeyValuePair<string, long>>(CurrentDb.TableCounts ?? new Dictionary<string, long>());

                AtualizaTabelasAlteradas();

                bool registosIguais = AlteredCurrentTableCounts.Count == 0;
                if (registosIguais)
                {
                    await Shell.Current.DisplayAlert(
                        "Sem alterações",
                        "O backup tem o mesmo número de registos em todas as tabelas que a base de dados atual. Não há necessidade de restaurar.",
                        "OK");
                    ShowRestorePanel = false;
                    return;
                }

                ShowRestorePanel = true;
            }
            finally
            {
                IsBusy = false;
            }
        }
        [RelayCommand]
        public async Task ConfirmRestoreAsync()
        {
            IsBusy = true;
            try
            {
                var backupPath = GetBackupPath();

                bool ok = await Shell.Current.DisplayAlert(
                    "Confirmar Restore",
                    "A operação irá substituir a base de dados atual pelo backup. Deseja continuar?",
                    "Sim", "Não");
                if (!ok)
                    return;

                File.Copy(backupPath, dbPath, overwrite: true);
                CurrentDb = GetDatabaseInfo(dbPath, tablesToCount);
                CurrentDbTableCounts = new ObservableCollection<KeyValuePair<string, long>>(CurrentDb.TableCounts ?? new Dictionary<string, long>());
                AtualizaTabelasAlteradas();
                ShowRestorePanel = false;
                await Shell.Current.DisplayAlert("Restore", "Base de dados restaurada com sucesso!", "OK");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("//PetsPage");
        }
    }
}