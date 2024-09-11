using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.PetFood
{
    [QueryProperty(nameof(SelectedPetFood), "SelectedPetFood")]

    public partial class PetFoodAddOrEditViewModel : PetFoodBaseViewModel, IQueryAttributable
    {
        public IRacaoService _service { get; set; }
        public IPetService _petService { get; set; }
        public int SelectedPetFoodId { get; set; }
        public IConnectivity _connectivity;

        public PetFoodAddOrEditViewModel(IRacaoService service, IConnectivity connectivity,IPetService petService)
        {
            _service = service;
            _petService = petService;
            _connectivity = connectivity;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedPetFood = query[nameof(SelectedPetFood)] as RacaoDto;
        }

        [RelayCommand]
        async Task GoBack()
        {
            var petId = SelectedPetFood.IdPet;
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
        async Task SavePetFood()
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

                if (SelectedPetFood.Id == 0)
                {

                    var insertedId = await _service.InsertAsync(SelectedPetFood);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while updating",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    var _petId = SelectedPetFood.IdPet;
                    var petVM = await _petService.GetPetVMAsync(_petId);


                    ShowToastMessage("Ração criada com sucesso");

                    await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"PetVM", petVM}
                        });
                }
                else // Insert (Id > 0)
                {
                    var _petFoodId = SelectedPetFood.Id;
                    var _petId = SelectedPetFood.IdPet;
                    await _service.UpdateAsync(_petFoodId, SelectedPetFood);

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
}
