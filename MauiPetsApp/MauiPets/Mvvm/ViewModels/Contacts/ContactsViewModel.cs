using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Models;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.Contacts
{
    public partial class ContactsViewModel : BaseViewModel
    {
        private readonly IContactService _contactService;
        private IConnectivity _connectivity;
        private IGeolocation _geolocation;

        [ObservableProperty]
        private string _searchFilter;

        public ObservableCollection<ContactoVM> ContactsVM { get; set; } = new();
        public ObservableCollection<TipoContacto> ContactTypes { get; set; } = new();

        public ContactsViewModel(IContactService contactService, IConnectivity connectivity, IGeolocation geolocation)
        {
            _contactService = contactService;
            _connectivity = connectivity;
            _geolocation = geolocation;
        }

        [RelayCommand]
        private async Task GetContactsAsync()
        {
            try
            {
                if (_connectivity.NetworkAccess != NetworkAccess.Internet)
                {
                    await Shell.Current.DisplayAlert("No connectivity!",
                        $"Please check internet and try again.", "OK");
                    return;
                }
                if (IsBusy)
                    return;

                IsBusy = true;
                if (ContactsVM.Count > 0)
                {
                    ContactsVM.Clear();
                }
                var contacts = (await _contactService.GetAllContactVMAsync()).ToList();

                // doesn't work as expected, followed microsoft example
                
                //foreach (var contact in contacts)
                //{
                //    var contactTypeName = contact.DescricaoTipoContacto;
                //    ContactGroups.Add(
                //        new ContactGroup(contactTypeName, new List<ContactoVM>
                //        {
                //            new ContactoVM
                //            {
                //                Nome = contact.Nome,
                //                 DescricaoTipoContacto = contact.DescricaoTipoContacto,
                //                Localidade = contact.Localidade,
                //                eMail = contact.eMail,
                //                Morada = contact.Morada,
                //                Movel = contact.Movel,
                //                Id = contact.Id,
                //                IdTipoContacto = contact.IdTipoContacto,
                //                Notas = contact.Notas,
                //            }
                //        })
                //    );
                //}

                foreach (var contact in contacts)
                {
                    ContactsVM.Add(contact);
                }

            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get contacts: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
            }
        }
    }
}
