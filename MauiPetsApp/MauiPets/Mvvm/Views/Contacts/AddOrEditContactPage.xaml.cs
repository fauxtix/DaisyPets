using MauiPets.Mvvm.ViewModels.Contacts;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

namespace MauiPets.Mvvm.Views.Contacts;

public partial class AddOrEditContactPage : ContentPage
{
    private ContactAddOrEditViewModel _viewModel;

    public AddOrEditContactPage(ContactAddOrEditViewModel viewModel)
	{
		InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;

    }

    private void SelectedIndexChanged(object sender, EventArgs e)
    {
		try
		{
            if (BindingContext is ContactAddOrEditViewModel viewModel && sender is Picker picker)
            {
                var sit = picker.SelectedItem as LookupTableVM;
                viewModel.ContactoVM.IdTipoContacto = sit.Id;
            }

        }
        catch (Exception ex)
		{
            Shell.Current.DisplayAlert("Error while 'SelectedIndexChanged", ex.Message, "Ok");
        }
    }
}