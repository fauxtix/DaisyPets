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
        private async Task NavigateToTableAsync(string tableName)
        {
            if (IsBusy) return;

            IsBusy = true;
            await NavigateTo($"{nameof(SettingsManagementPage)}", new Dictionary<string, object>
            {
                { "TableName", tableName }
            });
            IsBusy = false;
        }
    }
}
