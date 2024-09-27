using CommunityToolkit.Mvvm.ComponentModel;
using MauiPets.Core.Application.ViewModels.Logs;

namespace MauiPets.Mvvm.ViewModels.Logs
{
    [QueryProperty(nameof(SelectedLogEntry), nameof(SelectedLogEntry))]


    public partial class LogViewExceptionViewModel : ObservableObject, IQueryAttributable
    {
        [ObservableProperty]
        private LogEntry _selectedLogEntry;


        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedLogEntry = query[nameof(LogEntry)] as LogEntry;
        }
    }
}
