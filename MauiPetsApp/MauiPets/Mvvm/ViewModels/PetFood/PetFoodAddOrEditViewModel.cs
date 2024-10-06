using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
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
        [ObservableProperty]
        public string _petPhoto;
        [ObservableProperty]
        public string _petName;

        public PetFoodAddOrEditViewModel(IRacaoService service, IPetService petService)
        {
            _service = service;
            _petService = petService;
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedPetFood = query[nameof(SelectedPetFood)] as RacaoDto;
            IsEditing = (bool)query[nameof(IsEditing)];

            EditCaption = IsEditing ? "Editar ração" : "Nova ração";
            var selectedPet = await _petService.GetPetVMAsync(SelectedPetFood.IdPet);

            PetPhoto = selectedPet.Foto;
            PetName = selectedPet.Nome;

            selectedPet.Foto = selectedPet.Foto; ;
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
                var errorMessages = _service.RegistoComErros(SelectedPetFood);
                if (!string.IsNullOrEmpty(errorMessages))
                {
                    await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                        $"{errorMessages}", "OK");
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
