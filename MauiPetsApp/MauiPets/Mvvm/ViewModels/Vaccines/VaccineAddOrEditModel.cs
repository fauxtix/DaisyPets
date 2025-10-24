using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiPets.Core.Application.Interfaces.Services.Notifications;
using MauiPets.Core.Application.ViewModels.Messages;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Vaccines;

[QueryProperty(nameof(SelectedVaccine), "SelectedVaccine")]

public partial class VaccineAddOrEditModel : VaccineBaseViewModel, IQueryAttributable
{
    private readonly INotificationsSyncService _notificationsSyncService;
    public IVacinasService _vaccinesService { get; set; }
    public IPetService _petService { get; set; }
    public int SelectedVaccineId { get; set; }

    [ObservableProperty]
    private TipoVacinaDto _tipoVacinaSelecionada;


    public ObservableCollection<TipoVacinaDto> TipoVacinas { get; } = new();

    [ObservableProperty]
    public string _petPhoto;
    [ObservableProperty]
    public string _petName;


    public VaccineAddOrEditModel(IVacinasService vacinnesService, IPetService petService, INotificationsSyncService notificationsSyncService)
    {
        _vaccinesService = vacinnesService;
        _petService = petService;
        _notificationsSyncService = notificationsSyncService;
    }

    partial void OnTipoVacinaSelecionadaChanged(TipoVacinaDto value) => SelectedVaccine.IdTipoVacina = value?.Id ?? 0;

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _ = ApplyQueryAttributesAsync(query);
    }
    public async Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
    {
        IsBusy = true;
        await Task.Delay(100);
        SelectedVaccine = query[nameof(SelectedVaccine)] as VacinaDto;

        await FillVaccinesTypes_ByCurrentSpecie();

        TipoVacinaSelecionada = TipoVacinas.FirstOrDefault(tp => tp.Id == SelectedVaccine.IdTipoVacina);

        IsEditing = (bool)query[nameof(IsEditing)];
        AddEditCaption = IsEditing ? "Editar vacina" : "Nova vacina";

        UpdateNextDose();

        var selectedPet = await _petService.GetPetVMAsync(SelectedVaccine.IdPet);

        PetPhoto = selectedPet.Foto;
        PetName = selectedPet.Nome;

        IsBusy = false;
    }

    [RelayCommand]
    async Task ShowVaccinesInfo()
    {
        IsBusy = true;
        try
        {
            await Shell.Current.GoToAsync($"{nameof(VaccineTypesPage)}", true);
        }
        finally
        {
            IsBusy = false;
        }
    }


    [RelayCommand]
    async Task GoBack()
    {
        IsBusy = true;
        try
        {
            var petId = SelectedVaccine.IdPet;
            if (petId > 0)
            {
                var response = await _petService.GetPetVMAsync(petId);

                if (response is not null)
                {
                    PetVM pet = response as PetVM;

                    await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"PetVM", pet },
                        });

                }
            }

        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    async Task SaveVaccine()
    {
        try
        {
            //if(IsNotBusy)
            //    IsBusy = true;

            var errorMessages = _vaccinesService.RegistoComErros(SelectedVaccine);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }


            if (SelectedVaccine.Id == 0)
            {
                IsBusy = true;
                await Task.Delay(100);
                var insertedId = await _vaccinesService.InsertAsync(SelectedVaccine);
                if (insertedId == -1)
                {
                    IsBusy = false;
                    await Shell.Current.DisplayAlert("Error while creating",
                        $"Please contact administrator..", "OK");
                    return;
                }
                var vaccineCreated = await _vaccinesService.GetVacinaVMAsync(insertedId);
                var petVM = await _petService.GetPetVMAsync(vaccineCreated.IdPet);


                ShowToastMessage("Registo criado com sucesso");
                UpdateNextDose();
                IsBusy = false;

                await _notificationsSyncService.SyncNotificationsAsync();
                WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());

                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"PetVM", petVM}
                    });
            }
            else // Update (Id > 0)
            {
                IsBusy = true;
                await Task.Delay(100);

                var _vaccineId = SelectedVaccine.Id;
                var _petId = SelectedVaccine.IdPet;
                await _vaccinesService.UpdateAsync(_vaccineId, SelectedVaccine);

                var petVM = await _petService.GetPetVMAsync(_petId);

                await _notificationsSyncService.SyncNotificationsAsync();
                WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());

                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"PetVM", petVM}
                    });

                ShowToastMessage("Registo atualizado com sucesso");
                UpdateNextDose();
                IsBusy = false;

            }

        }
        catch (Exception ex)
        {
            IsBusy = false;
            ShowToastMessage($"Error while creating Vaccine ({ex.Message})");
        }
    }

    private async Task FillVaccinesTypes_ByCurrentSpecie()
    {
        var petId = SelectedVaccine.IdPet;
        var petSpecie = (await _petService.FindByIdAsync(petId)).IdEspecie;
        var result = await _vaccinesService.GetTipoVacinasAsync(petSpecie);
        foreach (var vaccineType in result)
        {
            TipoVacinas.Add(vaccineType);
        }
    }

    private async void ShowToastMessage(string text)
    {
        CancellationTokenSource cancellationTokenSource = new CancellationTokenSource();
        ToastDuration duration = ToastDuration.Short;
        double fontSize = 14;

        var toast = Toast.Make(text, duration, fontSize);

        await toast.Show(cancellationTokenSource.Token);
    }

    private void UpdateNextDose()
    {
        DataProximaToma = DataFormat.DateParse(SelectedVaccine.DataToma).AddMonths(SelectedVaccine.ProximaTomaEmMeses);
    }
}