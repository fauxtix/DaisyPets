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
        private int _totalLogs; // This now holds the total number of records

        [ObservableProperty]
        private int _pageSize = 10; // Default page size

        [ObservableProperty]
        private int _currentPage = 1; // Default starting page

        [ObservableProperty]
        private int _totalPages; // Total number of pages

        public LogViewModel(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        [RelayCommand]
        public async Task LoadLogsAsync()
        {
            CurrentPage = 1; // Reset to the first page
            await LoadPageAsync();
        }

        [RelayCommand]
        public async Task LoadNextPageAsync()
        {
            if (CurrentPage < TotalPages) // Check against TotalPages
            {
                CurrentPage++;
                await LoadPageAsync();
            }
        }

        [RelayCommand]
        public async Task LoadPreviousPageAsync()
        {
            if (CurrentPage > 1)
            {
                CurrentPage--;
                await LoadPageAsync();
            }
        }

        private async Task LoadPageAsync()
        {
            var logs = await _logRepository.GetLogsAsync(CurrentPage, PageSize);
            Logs.Clear();

            foreach (var log in logs)
            {
                Logs.Add(log);
            }

            TotalLogs = await _logRepository.GetLogCountAsync(); // Fetch total logs
            TotalPages = (int)Math.Ceiling((double)TotalLogs / PageSize); // Calculate TotalPages
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
                var logs = await _logRepository.GetFilteredLogsAsync(SearchText);
                Logs.Clear();

                foreach (var log in logs)
                {
                    Logs.Add(log);
                }
                TotalLogs = Logs.Count; // Update TotalLogs with filtered logs
                TotalPages = 1; // Only one page when filtered
            }
        }

        [RelayCommand]
        public async Task DeleteAllLogsAsync()
        {
            //if (TotalLogs == 0) return;

            // Optional: Display confirmation before deleting
            var confirm = await Application.Current.MainPage.DisplayAlert("Confirm", "Are you sure you want to delete all log entries?", "Yes", "No");
            if (confirm)
            {
                try
                {
                    Log.Information("Attempting to delete all logs.");
                    await _logRepository.DeleteLogsAsync(); // Ensure this method is correctly implemented
                    Log.Warning("All logs have been deleted successfully.");
                    await LoadLogsAsync(); // Reload logs to update UI
                }
                catch (Exception ex)
                {
                    Log.Error($"Error deleting logs: {ex.Message}"); // Log the error if any
                }
            }
        }

        // This method is called automatically when PageSize changes
        partial void OnPageSizeChanged(int value)
        {
            CurrentPage = 1; // Reset to the first page
            _ = LoadLogsAsync(); // Start loading logs without awaiting
        }
    }
}
