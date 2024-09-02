using AutoMapper;
using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Contacts;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPets.Mvvm.ViewModels.Contacts
{
    [QueryProperty(nameof(ContactoVM), "ContactoVM")]


    public partial class ContactDetailViewModel : BaseViewModel, IQueryAttributable
    {
        private readonly IContactService _contactService;
        private readonly IMapper _mapper;
        public BackButtonBehavior BackButtonBehavior { get; set; }


        public ContactDetailViewModel(IContactService contactService, IMapper mapper)
        {
            _contactService = contactService;
            BackButtonBehavior = new BackButtonBehavior { IsVisible = false };
            _mapper = mapper;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {

            ContactoVM = query[nameof(ContactoVM)] as ContactoVM;
            OnPropertyChanged(nameof(ContactoVM));

        }

        [RelayCommand]
        async Task GoBack()
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}");
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
                    {"ContactoVM", _contact}
                    });

            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
        }

        [RelayCommand]
        private async Task DeleteContactAsync()
        {
            if (ContactoVM is null)
            {
                return;
            }
            try
            {
                var _contact = await _contactService.GetContactVMAsync(ContactoVM.Id);
                string contactName = _contact.Nome;
                bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o contacto {contactName}?", "Sim", "Não");
                if(okToDelete)
                {
                    await _contactService.DeleteAsync(_contact.Id);
                    await ShowToastMessage($"Contacto {contactName} apagado com sucesso");
                    await Shell.Current.GoToAsync("///.///ContactsPage", true);
                }
            }
            catch (Exception ex)
            {
                await ShowToastMessage($"Erro ao apagar Contacto! ({ex.Message})");
                await Shell.Current.GoToAsync($"{nameof(ContactsPage)}", true);
            }
        }

        private async Task ShowToastMessage(string text)
        {
            CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
            ToastDuration duration = ToastDuration.Short;
            double fontSize = 14;

            var toast = Toast.Make(text, duration, fontSize);

            await toast.Show(cancellationTokenSource.Token);
        }


    }
}
