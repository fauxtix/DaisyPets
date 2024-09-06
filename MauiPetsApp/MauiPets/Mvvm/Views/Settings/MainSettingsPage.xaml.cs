using MauiPets.Mvvm.ViewModels.Settings;

namespace MauiPets.Mvvm.Views.Settings;

public partial class MainSettingsPage : ContentPage
{

    public MainSettingsPage()
    {
        InitializeComponent();
    }

    private async void RacaButton_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
            new Dictionary<string, object>
            {
                    {"TableName", "Raca"},
                    {"Title", "Ra�as"},
            });

    }

    private async void Especie_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
            new Dictionary<string, object>
            {
                    {"TableName", "Especie"},
                    {"Title", "Esp�cies"},
            });
    }

    private async void Medicacao_Clicked(object sender, EventArgs e)
    {
        await Shell.Current.GoToAsync($"{nameof(SettingsManagementPage)}", true,
        new Dictionary<string, object>
        {
            {"TableName", "Medicacao"},
            {"Title", "Medica��o"},
        });

    }
}