using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPets.Mvvm.ViewModels.Vaccines;

[QueryProperty(nameof(SelectedVaccine), "SelectedVaccine")]

public partial class VaccineAddOrEditModel : VaccineBaseViewModel, IQueryAttributable
{
    public IVacinasService _vaccinesService { get; set; }
    public IPetService _petService { get; set; }
    public int SelectedVaccineId { get; set; }
    public IConnectivity _connectivity;

    public VaccineAddOrEditModel(IVacinasService vacinnesService, IConnectivity connectivity, IPetService petService)
    {
        _vaccinesService = vacinnesService;
        _petService = petService;
        _connectivity = connectivity;
    }
    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        SelectedVaccine = query[nameof(SelectedVaccine)] as VacinaDto;
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

        // await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}");
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

            if (SelectedVaccine.Id == 0)
            {
                var insertedId = await _vaccinesService.InsertAsync(SelectedVaccine);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while updating",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var _petId = SelectedVaccine.IdPet;
                var petVM = await _petService.GetPetVMAsync(_petId);

                //IsBusy = false;

                ShowToastMessage("Contact created succesfuly");

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
                ShowToastMessage("Record updated successfuly");

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
