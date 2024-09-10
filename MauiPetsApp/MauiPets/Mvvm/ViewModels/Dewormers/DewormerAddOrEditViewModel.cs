using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Dewormers
{
    [QueryProperty(nameof(SelectedDewormer), "SelectedDewormer")]

    public partial class DewormerAddOrEditViewModel : DewormerBaseViewModel, IQueryAttributable
    {
        public IDesparasitanteService _service { get; set; }
        public IPetService _petService { get; set; }
        public int SelectedDewormerId { get; set; }
        public IConnectivity _connectivity;

        public DewormerAddOrEditViewModel(IDesparasitanteService service, IConnectivity connectivity, IPetService petService)
        {
            _service = service;
            _petService = petService;
            _connectivity = connectivity;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedDewormer = query[nameof(SelectedDewormer)] as DesparasitanteDto;
            var dewormerType = SelectedDewormer.Tipo;
            IsTypeInternal = dewormerType == "I";
            IsTypeExternal = dewormerType == "E";
        }

        [RelayCommand]
        async Task GoBack()
        {
            var petId = SelectedDewormer.IdPet;
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
        async Task SaveDewormer()
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

                if (SelectedDewormer.Id == 0)
                {
                    SelectedDewormer.Tipo = IsTypeInternal ? "I" : "E";

                    var insertedId = await _service.InsertAsync(SelectedDewormer);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while updating",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    var _petId = SelectedDewormer.IdPet;
                    var petVM = await _petService.GetPetVMAsync(_petId);


                    ShowToastMessage("Desparasitante criado com sucesso");

                    await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"PetVM", petVM}
                        });
                }
                else // Insert (Id > 0)
                {
                    var _vaccineId = SelectedDewormer.Id;
                    var _petId = SelectedDewormer.IdPet;
                    SelectedDewormer.Tipo = IsTypeInternal ? "I" : "E";
                    await _service.UpdateAsync(_vaccineId, SelectedDewormer);

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
}