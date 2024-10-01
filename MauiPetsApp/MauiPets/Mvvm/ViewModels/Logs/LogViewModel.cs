using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Core.Application.Interfaces.Repositories.Logs;
using MauiPets.Core.Application.ViewModels.Logs;
using MauiPets.Mvvm.Views.Logs;
using MauiPets.Mvvm.Views.Pets;
using Serilog;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Logs
{
    public partial class LogViewModel : ObservableObject
    {
        private readonly ILogRepository _logRepository;

        public ObservableCollection<LogEntry> Logs { get; } = new ObservableCollection<LogEntry>();

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPaginationVisible))]
        private string _searchText;

        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPaginationVisible))]
        [NotifyPropertyChangedFor(nameof(WeHaveLogsToDisplay))]
        private int _totalLogs;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsPaginationVisible))]
        private int _pageSize = 6;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _totalPages;

        public bool IsPaginationVisible => TotalLogs > PageSize;
        public bool WeHaveLogsToDisplay => TotalLogs > 0;

        private bool CanNavigatePrevious() => CurrentPage > 1;
        private bool CanNavigateNext() => CurrentPage < TotalPages;

        public LogViewModel(ILogRepository logRepository)
        {
            _logRepository = logRepository;
            InitializeAsync();
        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(PetsPage)}");
        }

        private async void InitializeAsync()
        {
            await LoadLogsAsync();
        }

        [RelayCommand]
        public async Task LoadLogsAsync()
        {
            CurrentPage = 1;
            await LoadPageAsync();
        }

        [RelayCommand]
        private async Task FirstPageAsync()
        {
            CurrentPage = 1;
            await LoadPageAsync();
        }

        [RelayCommand]
        private async Task LastPageAsync()
        {
            CurrentPage = TotalPages;
            await LoadPageAsync();
        }

        [RelayCommand(CanExecute = nameof(CanNavigateNext))]
        public async Task LoadNextPageAsync()
        {
            if (CurrentPage < TotalPages)
            {
                try
                {
                    CurrentPage++;
                    await LoadPageAsync();

                }
                catch (Exception ex)
                {
                    Log.Error($"LoadNextPageAsync: {ex.Message}");
                }
            }
        }

        [RelayCommand(CanExecute = nameof(CanNavigatePrevious))]
        public async Task LoadPreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                try
                {
                    CurrentPage--;
                    await LoadPageAsync();

                }
                catch (Exception ex)
                {
                    Log.Error($"LoadPreviousPageAsync: {ex.Message}");
                }
            }
        }

        private async Task LoadPageAsync()
        {
            try
            {
                IsLoading = true;
                await Task.Delay(200);

                var logs = await _logRepository.GetLogsAsync(CurrentPage, PageSize);

                if (!string.IsNullOrEmpty(SearchText))
                {
                    var filteredLogs = logs.Where(e => e.Message.Contains(SearchText, StringComparison.OrdinalIgnoreCase)).ToList();

                    Logs.Clear();
                    foreach (var log in filteredLogs)
                    {
                        Logs.Add(log);
                    }

                    TotalLogs = filteredLogs.Count;
                }
                else
                {
                    Logs.Clear();
                    foreach (var log in logs)
                    {
                        Logs.Add(log);
                    }

                    TotalLogs = await _logRepository.GetLogCountAsync();
                }

                if (TotalLogs > 0)
                {
                    TotalPages = (int)Math.Ceiling((double)TotalLogs / PageSize);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Logs",
                        "Sem registos para mostrar..", "OK");
                    await Shell.Current.GoToAsync($"//{nameof(PetsPage)}");
                }
            }
            catch (Exception ex)
            {
                Log.Error($"LoadPageAsync: {ex.Message}");
            }
            finally
            {
                IsLoading = false;
            }
        }

        [RelayCommand]
        public async Task LoadFilteredLogsAsync(string searchText)
        {
            SearchText = searchText.Trim();
            await LoadLogsAsync();
        }

        [RelayCommand]
        public async Task DeleteAllLogsAsync()
        {
            if (TotalLogs == 0)
            {
                await ShowToastMessage("Sem registos para apagar");
                return;
            }

            var confirm = await Application.Current.MainPage.DisplayAlert("Confirme, p.f.", "Quer mesmo apagar todos os registos do Log?", "Sim", "Não");
            if (confirm)
            {
                try
                {
                    IsLoading = true;
                    await Task.Delay(200);
                    await _logRepository.DeleteLogsAsync();
                    await LoadLogsAsync();
                }
                catch (Exception ex)
                {
                    Log.Error($"Error deleting logs: {ex.Message}");
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        [RelayCommand]
        private async Task ViewLogExceptionAsync(LogEntry logEntry)
        {
            var response = await _logRepository.FindByIdAsync(logEntry.Id);
            if (!string.IsNullOrEmpty(response.Exception))
            {
                if (response.Exception != null)
                    await NavigateToViewExceptionPage(response);
            }
        }

        private async Task NavigateToViewExceptionPage(LogEntry response)
        {
            await Shell.Current.GoToAsync($"{nameof(LogViewExceptionPage)}", true,
                new Dictionary<string, object>
                {
                    {"SelectedLogEntry", response},
                });
        }

        [RelayCommand]
        public async Task DeleteLogByIdAsync(LogEntry log)
        {
            if (log == null) return;

            bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apagar entrada de {log.TimeStamp}?", "Sim", "Não");
            if (okToDelete)
            {
                try
                {
                    IsLoading = true;
                    await _logRepository.DeleteAsync(log.Id);
                    await ShowToastMessage($"Log apagado com sucesso");
                    await LoadLogsAsync();
                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message);
                }
                finally
                {
                    IsLoading = false;
                }
            }
        }

        partial void OnPageSizeChanged(int value)
        {
            try
            {
                CurrentPage = 1;
                TotalPages = (int)Math.Ceiling((double)TotalLogs / value);
                _ = LoadLogsAsync();

            }
            catch (Exception ex)
            {
                Log.Error($"OnPageSizeChanged: {ex.Message}");
            }
        }

        private async Task ShowToastMessage(string text)
        {
            var toast = Toast.Make(text, ToastDuration.Short, 14);
            await toast.Show();
        }

    }
}
