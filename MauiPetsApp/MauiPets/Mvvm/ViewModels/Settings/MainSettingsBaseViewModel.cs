using CommunityToolkit.Mvvm.ComponentModel;

namespace MauiPets.Mvvm.ViewModels.Settings
{
    public partial class MainSettingsBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private bool isBusy;

        [ObservableProperty]
        private string title;

        public bool IsNotBusy => !IsBusy;

        protected async Task NavigateTo(string route, IDictionary<string, object> parameters = null)
        {
            if (parameters == null)
            {
                await Shell.Current.GoToAsync(route);
            }
            else
            {
                await Shell.Current.GoToAsync(route, parameters);
            }
        }
    }
}
