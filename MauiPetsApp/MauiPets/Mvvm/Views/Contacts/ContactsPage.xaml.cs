using MauiPets.Mvvm.ViewModels.Contacts;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.Views.Contacts;



public partial class ContactsPage : ContentPage
{
    private readonly IContactService _contactService;
    private ContactsViewModel _viewModel;

    public ContactsPage(ContactsViewModel viewModel, IContactService contactService)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
        _contactService = contactService;
    }

    protected override void OnAppearing()
    {
        base.OnAppearing();
        ContactsList.ItemsSource = _viewModel.ContactsVM;
    }

    private async void searchBar_TextChanged(object sender, TextChangedEventArgs e)
    {
        var searchFilter = ((SearchBar)sender).Text;
        var contactsFiltered = await _contactService.SearchContactByNamet(searchFilter);
        var results = new ObservableCollection<ContactoVM>(contactsFiltered);
        ContactsList.ItemsSource = results;
    }

    async void OnItemSelected(object sender, SelectedItemChangedEventArgs args)
    {
        ContactoVM item = args.SelectedItem as ContactoVM;
        await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}", true,
            new Dictionary<string, object>
            {
                {"ContactoVM", item}
             });

    }
}