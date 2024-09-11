using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.VetAppointments
{
    [QueryProperty(nameof(SelectedAppointment), "SelectedAppointment")]

    public partial class VetAppointmentsAddOrEditViewModel : VetAppointmentsBaseViewModel, IQueryAttributable
    {
        public IConsultaService _service { get; set; }
        public IPetService _petService { get; set; }
        public IConnectivity _connectivity;
        public int SelectedAppointmentId { get; set; }

        public VetAppointmentsAddOrEditViewModel(IConsultaService service, IPetService petService, IConnectivity connectivity)
        {
            _service = service;
            _petService = petService;
            _connectivity = connectivity;
        }
        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedAppointment = query[nameof(SelectedAppointment)] as ConsultaVeterinarioDto;
        }

        [RelayCommand]
        async Task GoBack()
        {
            var petId = SelectedAppointment.IdPet;
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

        [RelayCommand]
        async Task SaveVetAppointment()
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

                if (SelectedAppointment.Id == 0)
                {

                    var insertedId = await _service.InsertAsync(SelectedAppointment);
                    if (insertedId == -1)
                    {
                        await Shell.Current.DisplayAlert("Error while updating",
                            $"Please contact administrator..", "OK");
                        return;
                    }

                    var _petId = SelectedAppointment.IdPet;
                    var petVM = await _petService.GetPetVMAsync(_petId);

                    ShowToastMessage("Consulta criada com sucesso");

                    await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"PetVM", petVM}
                        });
                }
                else // Insert (Id > 0)
                {
                    var _petApptId = SelectedAppointment.Id;
                    var _petId = SelectedAppointment.IdPet;
                    await _service.UpdateAsync(_petApptId, SelectedAppointment);

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
