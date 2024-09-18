using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Formatting;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Vaccines;

[QueryProperty(nameof(SelectedVaccine), "SelectedVaccine")]

public partial class VaccineAddOrEditModel : VaccineBaseViewModel, IQueryAttributable
{
    public IVacinasService _vaccinesService { get; set; }
    public IPetService _petService { get; set; }
    public int SelectedVaccineId { get; set; }

    [ObservableProperty]
    public string _petPhoto;
    [ObservableProperty]
    public string _petName;


    public IConnectivity _connectivity;

    public VaccineAddOrEditModel(IVacinasService vacinnesService, IConnectivity connectivity, IPetService petService)
    {
        _vaccinesService = vacinnesService;
        _petService = petService;
        _connectivity = connectivity;
    }
    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        IsBusy = true;
        await Task.Delay(100);
        SelectedVaccine = query[nameof(SelectedVaccine)] as VacinaDto;

        IsEditing = (bool) query[nameof(IsEditing)];
        AddEditCaption = IsEditing ? "Editar vacina" : "Nova vacina";

        DataProximaToma = DataFormat.DateParse(SelectedVaccine.DataToma).AddMonths(SelectedVaccine.ProximaTomaEmMeses);

        var selectedPet = await _petService.GetPetVMAsync(SelectedVaccine.IdPet);

        PetPhoto = selectedPet.Foto;
        PetName = selectedPet.Nome;

        IsBusy = false;
    }

    [RelayCommand]
    async Task GoBack()
    {
        var petId = SelectedVaccine.IdPet;
        if (petId > 0)
        {
            var response = await _petService.GetPetVMAsync(petId); // await http.GetAsync(devSslHelper.DevServerRootUrl + $"/api/Pets/PetVMById/{petId}");

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

    [RelayCommand]
    async Task SaveVaccine()
    {
        try
        {
            //if(IsNotBusy)
            //    IsBusy = true;

            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            var errorMessages = _vaccinesService.RegistoComErros(SelectedVaccine);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }


            if (SelectedVaccine.Id == 0)
            {
                var insertedId = await _vaccinesService.InsertAsync(SelectedVaccine);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while creating",
                        $"Please contact administrator..", "OK");
                    return;
                }
                var vaccineCreated = await _vaccinesService.GetVacinaVMAsync(insertedId);
                var petVM = await _petService.GetPetVMAsync(vaccineCreated.IdPet);

                //IsBusy = false;

                ShowToastMessage("Registo criado com sucesso");

                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"PetVM", petVM}
                    });
            }
            else // Insert (Id > 0)
            {
                var _vaccineId = SelectedVaccine.Id;
                var _petId = SelectedVaccine.IdPet;
                await _vaccinesService.UpdateAsync(_vaccineId, SelectedVaccine);

                var petVM = await _petService.GetPetVMAsync(_petId);


                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"PetVM", petVM}
                    });

                //IsBusy = false;
                ShowToastMessage("Registo atualizado com sucesso");

            }
        }
        catch (Exception ex)
        {
            IsBusy = false;
            ShowToastMessage($"Error while creating Vaccine ({ex.Message})");
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
}
