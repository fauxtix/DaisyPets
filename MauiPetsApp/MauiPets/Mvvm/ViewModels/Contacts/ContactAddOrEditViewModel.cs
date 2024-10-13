using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Contacts;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;


namespace MauiPets.Mvvm.ViewModels.Contacts;

[QueryProperty(nameof(ContactoVM), "ContactoVM")]

public partial class ContactAddOrEditViewModel : BaseViewModel, IQueryAttributable
{
    public List<LookupTableVM> ContactTypes { get; } = new();

    [ObservableProperty]
    private LookupTableVM _selectedContactType;

    [ObservableProperty]
    private int _contactTypeSelected;
    [ObservableProperty]
    private int _contactTypeSelectedIndex;

    public IContactService _contactsService { get; set; }

    public ILookupTableService _lookupTablesService { get; set; }

    [ObservableProperty]
    private string descricao;

    public BackButtonBehavior BackButtonBehavior { get; set; }

    public int SelectedContactId { get; set; }
    public ContactAddOrEditViewModel(IContactService contactsService,
                                     ILookupTableService lookupTablesService)
    {
        _contactsService = contactsService;
        _lookupTablesService = lookupTablesService;

        FillContactTypes();

        BackButtonBehavior = new BackButtonBehavior { IsVisible = false };
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        EditCaption = query[nameof(EditCaption)] as string;
        IsEditing = (bool)query[nameof(IsEditing)];

        if (ContactoVM is null)
        {
            ContactTypeSelectedIndex = 0;
        }
        else
        {
            ContactoVM = query[nameof(ContactoVM)] as ContactoVM;
            ContactTypeSelectedIndex = ContactTypes.FindIndex(item => item.Id == ContactoVM.IdTipoContacto);
        }
    }



    [RelayCommand]
    async Task SaveContactData()
    {
        try
        {

            var morada = ContactoVM.Morada;
            var localidade = ContactoVM.Localidade;
            var (latitude, longitude) = await GetCoordinatesFromAddress(morada, localidade);

            ContactoVM.Latitude = latitude;
            ContactoVM.Longitude = longitude;

            if (ContactoVM.Id == 0)
            {
                var insertedId = await _contactsService.InsertAsync(ContactoVM);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while updating contact",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var contactoVM = await _contactsService.GetContactVMAsync(insertedId);

                await ShowToastMessage("Contacto criado com sucesso");

                await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"ContactoVM", contactoVM }
                    });


            }
            else // Insert (Id > 0)
            {
                var _contactId = ContactoVM.Id;
                await _contactsService.UpdateAsync(_contactId, ContactoVM);

                var contactoVM = await _contactsService.GetContactVMAsync(_contactId);

                await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"ContactoVM", contactoVM }
                    });

                await ShowToastMessage("Contacto atualizado com sucesso");

            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Error while creating Contact ({ex.Message})");
        }
    }

    [RelayCommand]
    private void UpdateContactType(int contactId)
    {
        // Verificar se a raça selecionada é diferente da anterior
        if (contactId != SelectedContactId)
        {
            SelectedContactId = contactId;

            ContactoVM.IdTipoContacto = SelectedContactId;
        }
    }

    [RelayCommand]
    async Task GoBack()
    {
        if (ContactoVM.Id > 0)
        {
            await Shell.Current.GoToAsync($"{nameof(ContactDetailPage)}", true,
                new Dictionary<string, object>
                {
                {"ContactoVM", ContactoVM},
                });
        }
        else
        {
            await Shell.Current.GoToAsync($"//{nameof(ContactsPage)}", true);
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

    private void FillContactTypes()
    {
        var result = _lookupTablesService.GetLookupTableData("TipoContacto").Result;
        foreach (var contactType in result)
        {
            ContactTypes.Add(contactType);
        }
    }

    private async Task<(double latitude, double longitude)> GetCoordinatesFromAddress(string morada, string localidade)
    {
        string address = $"{morada}, {localidade}";

        try
        {
            var locations = await Geocoding.Default.GetLocationsAsync(address);

            var location = locations?.FirstOrDefault();

            if (location != null)
            {
                return (location.Latitude, location.Longitude);
            }
        }
        catch (Exception ex)
        {
            // Trate exceções, como falha ao acessar o serviço de geocodificação
            Console.WriteLine($"Erro ao obter coordenadas: {ex.Message}");
        }

        // Retorna 0,0 caso não seja possível obter a localização
        return (0.0, 0.0);
    }
}
