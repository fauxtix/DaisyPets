using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Contacts
{
    public partial class ContactsViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;

        [ObservableProperty]
        private string _searchFilter;

        [ObservableProperty]
        private string _searchText = string.Empty;


        public ObservableCollection<ContactoVM> ContactsVM { get; set; } = new();
        public ObservableCollection<TipoContacto> ContactTypes { get; set; } = new();

        public ContactsViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }

        [RelayCommand]
        private async Task GetContactsAsync()
        {
            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;
                await Task.Delay(100);

                var contacts = (await _contactService.GetAllContactVMAsync()).ToList();
                if (!string.IsNullOrWhiteSpace(SearchText))
                {
                    contacts = contacts
                        .Where(e =>
                            e.Nome.Contains(SearchText, StringComparison.OrdinalIgnoreCase))
                        .ToList();
                }

                ContactsVM.Clear();

                foreach (var contact in contacts)
                {
                    ContactsVM.Add(contact);
                }

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        private async Task SearchContactsAsync(string searchText)
        {
            SearchText = searchText;
            await GetContactsAsync();
        }
    }
}
