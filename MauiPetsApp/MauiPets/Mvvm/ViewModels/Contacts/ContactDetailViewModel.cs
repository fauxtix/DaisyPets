using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Contacts;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Contacts
{
    [QueryProperty(nameof(ContactoVM), "ContactoVM")]

    public partial class ContactDetailViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IContactService _contactService;

        public ContactDetailViewModel(IContactService contactService)
        {
            _contactService = contactService;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            ContactoVM = query[nameof(ContactoVM)] as ContactoVM;

        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync("..");
        }

        [RelayCommand]
        private async Task AddOrEditContactAsync()
        {
            if (ContactoVM is null)
            {
                return;
            }
            try
            {
                var _contact = await _contactService.GetContactVMAsync(ContactoVM.Id);
                await Shell.Current.GoToAsync($"{nameof(AddOrEditContactPage)}", true,
                    new Dictionary<string, object>
                    {
                    {"ContactVM", _contact}
                    });

            }
            catch (Exception ex)
            {
                var s = ex.Message;
                throw;
            }
        }

    }
}
