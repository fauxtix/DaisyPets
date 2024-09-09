using MauiPets.Mvvm.ViewModels.Settings;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.Views.Settings;

public partial class SettingsManagementPage : ContentPage
{
    public ILookupTableService _service { get; }

    public SettingsManagementPage(SettingsViewModel viewModel, ILookupTableService service )
	{
		InitializeComponent();
        BindingContext = viewModel;
        _service = service;
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

    private void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        //var searchFilter = ((SearchBar)sender).Text;
        //var contactsFiltered = 
        //var results = new ObservableCollection<LookupTableVM>(contactsFiltered);
        //lookupTablesView.ItemsSource = results;

    }
}