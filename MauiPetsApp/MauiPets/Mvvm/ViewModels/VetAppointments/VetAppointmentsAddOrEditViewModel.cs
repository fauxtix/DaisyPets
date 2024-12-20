using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
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
        public int SelectedAppointmentId { get; set; }

        [ObservableProperty]
        public string _petPhoto;
        [ObservableProperty]
        public string _petName;


        public VetAppointmentsAddOrEditViewModel(IConsultaService service, IPetService petService)
        {
            _service = service;
            _petService = petService;
        }
        public async void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            IsBusy = true;
            await Task.Delay(100);

            SelectedAppointment = query[nameof(SelectedAppointment)] as ConsultaVeterinarioDto;

            IsEditing = (bool)query[nameof(IsEditing)];
            AddEditCaption = IsEditing ? "Editar consulta" : "Nova visita";

            var selectedPet = await _petService.GetPetVMAsync(SelectedAppointment.IdPet);

            PetPhoto = selectedPet.Foto;
            PetName = selectedPet.Nome;
            IsBusy = false;
        }

        [RelayCommand]
        async Task GoBack()
        {
            IsBusy = true;
            try
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
            finally
            {
                IsBusy = false;
            }
        }

        [RelayCommand]
        async Task SaveVetAppointment()
        {
            try
            {
                //if(IsNotBusy)
                //    IsBusy = true;


                var errorMessages = _service.RegistoComErros(SelectedAppointment);
                if (!string.IsNullOrEmpty(errorMessages))
                {
                    await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                        $"{errorMessages}", "OK");
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
