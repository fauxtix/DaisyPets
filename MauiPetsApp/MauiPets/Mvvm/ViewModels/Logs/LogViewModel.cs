using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Core.Application.Interfaces.Repositories.Logs;
using MauiPets.Core.Application.ViewModels.Logs;
using Serilog;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Logs
{
    public partial class LogViewModel : ObservableObject
    {
        private readonly ILogRepository _logRepository;

        public ObservableCollection<LogEntry> Logs { get; } = new ObservableCollection<LogEntry>();

        [ObservableProperty]
        private string _searchText;
        [ObservableProperty]
        private bool _isLoading;

        [ObservableProperty]
        private int _totalLogs;

        [ObservableProperty]
        private int _pageSize = 10;

        [ObservableProperty]
        private int _currentPage = 1;

        [ObservableProperty]
        private int _totalPages;

        private bool CanNavigatePrevious() => CurrentPage > 1;
        private bool CanNavigateNext() => CurrentPage < TotalPages;

        public LogViewModel(ILogRepository logRepository)
        {
            _logRepository = logRepository;
            InitializeAsync();
        }

        private async void InitializeAsync()
        {
            await LoadLogsAsync();
        }

        [RelayCommand]
        public async Task LoadLogsAsync()
        {
            await Task.Delay(100);
            IsLoading = true;
            CurrentPage = 1;
            await LoadPageAsync();
            IsLoading = false;
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

                var logs = await _logRepository.GetLogsAsync(CurrentPage, PageSize);
                Logs.Clear();

                foreach (var log in logs)
                {
                    Logs.Add(log);
                }

                TotalLogs = await _logRepository.GetLogCountAsync();
                TotalPages = (int)Math.Ceiling((double)TotalLogs / PageSize);
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
        public async Task LoadFilteredLogsAsync()
        {
            if (string.IsNullOrWhiteSpace(SearchText))
            {
                await LoadLogsAsync();
            }
            else
            {
                try
                {
                    IsLoading = true;
                    var logs = await _logRepository.GetFilteredLogsAsync(SearchText);
                    Logs.Clear();

                    foreach (var log in logs)
                    {
                        Logs.Add(log);
                    }
                    TotalLogs = Logs.Count;
                    TotalPages = 1;

                }
                catch (Exception ex)
                {
                    Log.Error($"LoadFilteredLogsAsync: {ex.Message}");
                }
                finally
                {
                    IsLoading = false;
                }

            }
        }

        [RelayCommand]
        public async Task DeleteAllLogsAsync()
        {

            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to delete all log entries?", "Yes", "No");
            if (confirm)
            {
                try
                {
                    IsLoading = true;
                    await _logRepository.DeleteLogsAsync();
                    Log.Warning("All logs have been deleted successfully.");
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
    }
}
