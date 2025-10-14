using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using CommunityToolkit.Mvvm.Messaging;
using MauiPets.Core.Application.Interfaces.Services.Notifications;
using MauiPets.Core.Application.ViewModels.Messages;
using MauiPets.Mvvm.Views.Dewormers;
using MauiPets.Mvvm.Views.PetFood;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPets.Mvvm.Views.VetAppointments;
using MauiPets.Services;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;

namespace MauiPets.Mvvm.ViewModels.Pets;


[QueryProperty(nameof(PetVM), "PetVM")]

public partial class PetDetailViewModel : BaseViewModel, IQueryAttributable
{
    private readonly IPetService _petService;
    private readonly IVacinasService _petVaccinesService;
    private readonly IDesparasitanteService _petDewormersService;
    private readonly IRacaoService _petFoodService;
    private readonly IConsultaService _petVeterinaryAppointmentsService;
    private readonly INotificationsSyncService _notificationsSyncService;

    private readonly IPetFichaPdfService _petFichaPdfService;
    public PetDetailViewModel(IPetService petService,
                              IVacinasService petVaccinesService,
                              IDesparasitanteService petDewormersService,
                              IRacaoService petFoodService,
                              IConsultaService petVeterinaryAppointmentsService,
                              IPetFichaPdfService petFichaPdfService,
                              INotificationsSyncService notificationsSyncService)
    {
        _petService = petService;
        _petVaccinesService = petVaccinesService;
        _petDewormersService = petDewormersService;
        _petFoodService = petFoodService;
        _petVeterinaryAppointmentsService = petVeterinaryAppointmentsService;
        _petFichaPdfService = petFichaPdfService;
        _notificationsSyncService = notificationsSyncService;
    }


    [ObservableProperty]
    PetVM petVM;

    public ObservableCollection<VacinaVM> PetVaccinesVM { get; set; } = new();
    public ObservableCollection<DesparasitanteVM> PetDewormersVM { get; set; } = new();
    public ObservableCollection<RacaoVM> PetFoodVM { get; set; } = new();
    public ObservableCollection<ConsultaVeterinarioVM> PetConsultationsVM { get; set; } = new();

    [ObservableProperty]
    string faixaEtaria;

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync($"//{nameof(PetsPage)}");
    }

    [RelayCommand]
    async Task RefreshPetDetail()
    {
        int petId = PetVM.Id;
        IsRefreshing = true;
        PetVM = await _petService.GetPetVMAsync(petId);
        IsRefreshing = false;
    }

    [RelayCommand]
    private async Task AddOrEditPetAsync()
    {
        if (PetVM is null)
        {
            return;
        }

        IsBusy = true;

        IsEditing = true;
        EditCaption = "Edição de registo";

        try
        {
            var petDto = await _petService.FindByIdAsync(PetVM.Id);
            await Shell.Current.GoToAsync($"{nameof(PetAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"PetDto", petDto},
                    {"EditCaption", EditCaption},
                    {"IsEditing", IsEditing },

                });

        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao editar Pet ({ex.Message})");
        }
        finally
        {
            IsBusy = false;
        }
    }


    [RelayCommand]
    private async Task AddVaccine()
    {
        IsEditing = false;
        SelectedVaccine = new()
        {
            DataToma = DateTime.Now.Date.ToShortDateString(),
            Marca = "",
            ProximaTomaEmMeses = 3,
            IdPet = PetVM.Id
        };

        await Shell.Current.GoToAsync($"{nameof(VaccineAddOrEditPage)}", true,
            new Dictionary<string, object>
            {
                {"SelectedVaccine", SelectedVaccine},
                {"IsEditing", IsEditing},
            });
    }


    [RelayCommand]
    private async Task EditVaccineAsync(VacinaVM vaccine)
    {
        IsBusy = true;
        try
        {
            var vaccineId = vaccine.Id;
            if (vaccineId > 0)
            {
                IsEditing = true;
                var response = await _petVaccinesService.FindDtoByIdAsync(vaccineId);
                SelectedVaccine = response;
                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(VaccineAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"SelectedVaccine", response},
                            {"IsEditing", IsEditing},
                        });
                }
                else
                {
                    await Shell.Current.DisplayAlert("Vacina não encontrada", $"ID#: {vaccineId}", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Task.CompletedTask;
            await Shell.Current.DisplayAlert("Error while 'EditVaccineAsync", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddDewormer()
    {
        IsEditing = false;
        IsBusy = true;
        try
        {
            SelectedDewormer = new()
            {
                DataAplicacao = DateTime.Now.Date.ToShortDateString(),
                DataProximaAplicacao = DateTime.Now.Date.AddMonths(3).ToShortDateString(),
                Tipo = "",
                Marca = "",
                IdPet = PetVM.Id
            };

            await Shell.Current.GoToAsync($"{nameof(DewormerAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                {"SelectedDewormer", SelectedDewormer},
                {"IsEditing", IsEditing},
                });

        }
        catch (Exception ex)
        {
            await Task.CompletedTask;
            await Shell.Current.DisplayAlert("Error while 'AddDewormer", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task EditDewormerAsync(DesparasitanteVM dewormer)
    {
        IsEditing = true;
        IsBusy = true;

        try
        {

            var dewormerId = dewormer.Id;
            if (dewormerId > 0)
            {
                var response = await _petDewormersService.FindByIdAsync(dewormerId);
                SelectedDewormer = response;
                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(DewormerAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"SelectedDewormer", response},
                            {"IsEditing", IsEditing},
                        });
                }
                else
                {
                    await Shell.Current.DisplayAlert("Desparasitante não encontrado", $"ID#: {dewormerId}", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'EditDewormerAsync", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddPetFood()
    {
        IsEditing = false;
        IsBusy = true;
        try
        {
            SelectedPetFood = new()
            {
                IdPet = PetVM.Id,
                Marca = "",
                DataCompra = DateTime.Now.Date.ToShortDateString(),
                QuantidadeDiaria = 1
            };

            await Shell.Current.GoToAsync($"{nameof(PetFoodAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                {"SelectedPetFood", SelectedPetFood},
                {"IsEditing", IsEditing},
                });

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'AddPetFood", ex.Message, "Ok");
        }

        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task AddVetAppointment()
    {
        IsEditing = false;
        IsBusy = true;

        try
        {
            SelectedAppointment = new()
            {
                IdPet = PetVM.Id,
                DataConsulta = DateTime.Now.Date.ToShortDateString(),
                Motivo = string.Empty,
                Diagnostico = string.Empty,
                Tratamento = string.Empty,
                Notas = string.Empty,
            };

            await Shell.Current.GoToAsync($"{nameof(VetAppointmentAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                {"SelectedAppointment", SelectedAppointment},
                {"IsEditing", IsEditing},
                });

        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'AddVetAppointment", ex.Message, "Ok");
        }

        finally
        {
            IsBusy = false;
        }
    }


    [RelayCommand]
    private async Task EditPetFoodAsync(RacaoVM petFood)
    {
        IsEditing = true;
        IsBusy = true;

        try
        {

            var petFoodId = petFood.Id;
            if (petFoodId > 0)
            {
                var response = await _petFoodService.FindByIdAsync(petFoodId);
                SelectedPetFood = response;
                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(PetFoodAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"SelectedPetFood", SelectedPetFood},
                            {"IsEditing", IsEditing},
                        });
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ração não encontrada", $"ID#: {petFoodId}", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'EditPetFoodAsync", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }

    [RelayCommand]
    private async Task EditVetAppointmentAsync(ConsultaVeterinarioVM petAppointment)
    {
        IsBusy = true;
        IsEditing = true;
        try
        {
            var petApptId = petAppointment.Id;
            if (petApptId > 0)
            {
                var response = await _petVeterinaryAppointmentsService.FindByIdAsync(petApptId);
                SelectedAppointment = response;
                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(VetAppointmentAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"SelectedAppointment", SelectedAppointment},
                            {"IsEditing", IsEditing},
                        });
                }
                else
                {
                    await Shell.Current.DisplayAlert("Consulta não encontrada", $"ID#: {petApptId}", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'EditAddVetAppointmentAsync", ex.Message, "Ok");
        }
        finally
        {
            IsBusy = false;
        }
    }



    [RelayCommand]
    private async Task DeletePetAsync()
    {
        if (PetDto is null)
        {
            return;
        }
        try
        {
            var _petRecord = await _petService.GetPetVMAsync(PetVM.Id);
            string petName = _petRecord.Nome;
            bool deletionConfirmed = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o Pet {petName}?", "Sim", "Não");
            if (deletionConfirmed)
            {
                var okToDelete = await _petService.DeleteAsync(_petRecord.Id);
                if (okToDelete)
                {
                    await ShowToastMessage($"Pet {petName} apagado com sucesso");
                    await Shell.Current.GoToAsync($"//{nameof(PetsPage)}", true);
                }
                else
                {
                    await Shell.Current.DisplayAlert("Apagar Pet", $"{petName} não pode ser apagado, uma vez que tem histórico associado.", "Ok");
                }
            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar Pet! ({ex.Message})");
            await Shell.Current.GoToAsync($"{nameof(PetsPage)}", true);
        }
    }

    [RelayCommand]
    private async Task DeleteVacinaAsync(VacinaVM vaccine)
    {
        if (vaccine is null)
        {
            return;
        }
        try
        {
            bool deletionConfirmed = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga a vacina de {vaccine.DataToma} ({vaccine.Marca})?", "Sim", "Não");
            if (deletionConfirmed)
            {
                await _petVaccinesService.DeleteAsync(vaccine.Id);

                await ShowToastMessage($"Vacina do dia  {vaccine.DataToma} apagada com sucesso");

                await _notificationsSyncService.DeleteNotificationsForRelatedItemAsync(vaccine.Id, "vaccine");
                WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());

                PetVaccinesVM.Remove(vaccine);
            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar Vacina ({ex.Message})");
        }
    }

    [RelayCommand]
    private async Task DeleteDesparasitanteAsync(DesparasitanteVM dewormer)
    {
        if (dewormer is null)
        {
            return;
        }
        try
        {
            bool deletionConfirmed = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o registo do desparasitante de {dewormer.DataAplicacao} ({dewormer.Marca})?", "Sim", "Não");
            if (deletionConfirmed)
            {
                await _petDewormersService.DeleteAsync(dewormer.Id);
                await ShowToastMessage($"Aplicação do desparasitante no dia  {dewormer.DataAplicacao} apagado com sucesso");
                PetDewormersVM.Remove(dewormer);

                await _notificationsSyncService.DeleteNotificationsForRelatedItemAsync(dewormer.Id, "dewormer");
                WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());

            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar desparasitante ({ex.Message})");
        }
    }

    [RelayCommand]
    private async Task DeletePetFoodAsync(RacaoVM petFood)
    {
        if (petFood is null)
        {
            return;
        }
        try
        {
            bool deletionConfirmed = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o registo da Ração de {petFood.DataCompra} ({petFood.Marca})?", "Sim", "Não");
            if (deletionConfirmed)
            {
                await _petFoodService.DeleteAsync(petFood.Id);
                await ShowToastMessage($"Ração comprada em  {petFood.DataCompra} apagada com sucesso");
                PetFoodVM.Remove(petFood);
            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar desparasitante ({ex.Message})");
        }
    }


    [RelayCommand]
    private async Task DeletePetAppointment(ConsultaVeterinarioVM petAppt)
    {
        if (petAppt is null)
        {
            return;
        }
        try
        {
            bool deletionConfirmed = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o registo da Consulya de {petAppt.DataConsulta}?", "Sim", "Não");
            if (deletionConfirmed)
            {
                await _petVeterinaryAppointmentsService.DeleteAsync(petAppt.Id);

                await _notificationsSyncService.DeleteNotificationsForRelatedItemAsync(petAppt.Id, "vet_appointment");
                WeakReferenceMessenger.Default.Send(new UpdateUnreadNotificationsMessage());

                await ShowToastMessage($"Consulta de  {petAppt.DataConsulta} apagada com sucesso");
                PetConsultationsVM.Remove(petAppt);
            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar consulta ({ex.Message})");
        }
    }

    [RelayCommand]
    public async Task ExportarFichaPetPdfAsync()
    {
        if (PetVM is null)
            return;

        try
        {
            IsBusy = true;
            var pdfStream = await _petFichaPdfService.GenerateFichaPetPdfAsync(
                PetVM,
                PetVaccinesVM?.Any() == true ? PetVaccinesVM : Enumerable.Empty<VacinaVM>(),
                PetDewormersVM?.Any() == true ? PetDewormersVM : Enumerable.Empty<DesparasitanteVM>(),
                PetFoodVM?.Any() == true ? PetFoodVM : Enumerable.Empty<RacaoVM>(),
                PetConsultationsVM?.Any() == true ? PetConsultationsVM : Enumerable.Empty<ConsultaVeterinarioVM>()
    );

            var filePath = Path.Combine(FileSystem.CacheDirectory, $"Ficha_{PetVM.Nome}.pdf");
            using (var file = File.Create(filePath))
            {
                pdfStream.Seek(0, SeekOrigin.Begin);
                await pdfStream.CopyToAsync(file);
            }

            await Share.RequestAsync(new ShareFileRequest
            {
                Title = $"Ficha de {PetVM.Nome}",
                File = new ShareFile(filePath)
            });

        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro na partilha de Pdf ({ex.Message})");
        }
        finally
        {
            IsBusy = false;
        }
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        _ = ApplyQueryAttributesAsync(query);
    }


    public async Task ApplyQueryAttributesAsync(IDictionary<string, object> query)
    {
        PetVM = query[nameof(PetVM)] as PetVM;

        var petId = PetVM.Id;

        var vaccinesTask = _petVaccinesService.GetPetVaccinesVMAsync(petId);
        var dewormersTask = _petDewormersService.GetDesparasitanteVMAsync(petId);
        var foodItemsTask = _petFoodService.GetRacaoVMAsync(petId);
        var consultationsTask = _petVeterinaryAppointmentsService.GetConsultaVMAsync(petId);

        await Task.WhenAll(vaccinesTask, dewormersTask, foodItemsTask, consultationsTask);

        // Clear and repopulate existing collections
        PetVaccinesVM.Clear();
        foreach (var vaccine in await vaccinesTask)
        {
            PetVaccinesVM.Add(vaccine);
        }

        PetDewormersVM.Clear();
        foreach (var dewormer in await dewormersTask)
        {
            PetDewormersVM.Add(dewormer);
        }

        PetFoodVM.Clear();
        foreach (var food in await foodItemsTask)
        {
            PetFoodVM.Add(food);
        }

        PetConsultationsVM.Clear();
        foreach (var consultation in await consultationsTask)
        {
            PetConsultationsVM.Add(consultation);
        }

        Gender = PetVM.Genero == "M" ? "Macho" : "Fêmea";
        GodFather = PetVM.Padrinho ? "Sim" : "Não";
        Sterilized = PetVM.Esterilizado ? "Sim" : "Não";
        Chipped = PetVM.Chipado ? "Sim" : "Não";

        var dNascimento = DateTime.Parse(PetVM.DataNascimento);
        DateOnly startDate = DateOnly.FromDateTime(dNascimento);
        DateOnly endDate = DateOnly.FromDateTime(DateTime.Today);
        int meses = Extensions.DateTimeExtensions.NumberOfMonths(startDate, endDate);
        var tamanho = PetVM.IdTamanho;
        FaixaEtaria = await _petService.GetDescriptionBySizeAndMonths(tamanho, meses);

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