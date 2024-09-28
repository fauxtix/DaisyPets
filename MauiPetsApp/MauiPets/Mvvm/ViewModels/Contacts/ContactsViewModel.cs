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
                if (ContactsVM.Count > 0)
                {
                    ContactsVM.Clear();
                }
                var contacts = (await _contactService.GetAllContactVMAsync()).ToList();

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
    }
}
