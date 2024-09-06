using MauiPets.Mvvm.ViewModels.Settings;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.Views.Settings;

public partial class SettingsManagementPage : ContentPage
{
	public SettingsManagementPage(SettingsViewModel viewModel)
	{
		InitializeComponent();
        BindingContext = viewModel;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        if (BindingContext is SettingsViewModel viewModel)
        {
            viewModel.GetLookupDataCommand.Execute(null);
            
        }
    }

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        if (args.SelectedItem is LookupTableVM item)
        {
            await Shell.Current.GoToAsync($"{nameof(SettingsAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                        {"LookupRecordSelected", item },
                        {"EdtCaption","Editar"},
                        {"IsEditing", true },
                });
        }
    }


}