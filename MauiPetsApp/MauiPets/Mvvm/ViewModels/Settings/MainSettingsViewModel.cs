using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Settings;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

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
            LookupTableVM lookupTableVM = new LookupTableVM();
            await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true, new Dictionary<string, object>
                {
                    { "Title", title },
                     { "TableName", tableName },

                });
        }
    }
}
