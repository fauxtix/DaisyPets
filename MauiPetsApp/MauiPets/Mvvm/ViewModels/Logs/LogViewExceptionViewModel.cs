using MauiPets.Core.Application.ViewModels.Logs;

namespace MauiPets.Mvvm.ViewModels.Logs
{
    [QueryProperty(nameof(SelectedLogEntry), nameof(SelectedLogEntry))]


    public partial class LogViewExceptionViewModel : LogsBaseViewModel, IQueryAttributable
    {

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedLogEntry = query[nameof(SelectedLogEntry)] as LogEntry;
        }
    }
}
