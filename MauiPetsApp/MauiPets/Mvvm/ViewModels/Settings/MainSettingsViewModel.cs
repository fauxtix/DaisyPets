using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Settings;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    public partial class MainSettingsViewModel : MainSettingsBaseViewModel
    {
        public MainSettingsViewModel()
        {
            Title = "Configurações Principais";
        }

        [RelayCommand]
        public async Task NavigateToTableAsync(Dictionary<string, string> parameters)
        {
            if (parameters == null || !parameters.ContainsKey("TableName") || !parameters.ContainsKey("Title"))
                return;

            string tableName = parameters["TableName"];
            string title = parameters["Title"];

            await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true, new Dictionary<string, object>
            {
                { "TableName", tableName },
                { "Title", title }
            });
        }
    }
}
