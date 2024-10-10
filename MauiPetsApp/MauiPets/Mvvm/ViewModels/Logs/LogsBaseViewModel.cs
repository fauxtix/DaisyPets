using CommunityToolkit.Mvvm.ComponentModel;
using MauiPets.Core.Application.ViewModels.Logs;

namespace MauiPets.Mvvm.ViewModels.Logs
{
    public partial class LogsBaseViewModel : ObservableObject
    {

        [ObservableProperty]
        private LogEntry _selectedLogEntry;
    }
}
