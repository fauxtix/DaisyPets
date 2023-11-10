using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Mvvm.Views.Vaccines;
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
    public PetDetailViewModel(IPetService petService,
                              IVacinasService petVaccinesService,
                              IDesparasitanteService petDewormersService,
                              IRacaoService petFoodService,
                              IConsultaService petVeterinaryAppointmentsService)
    {
        _petService = petService;
        _petVaccinesService = petVaccinesService;
        _petDewormersService = petDewormersService;
        _petFoodService = petFoodService;
        _petVeterinaryAppointmentsService = petVeterinaryAppointmentsService;
    }


    [ObservableProperty]
    PetVM petVM;

    public ObservableCollection<VacinaVM> PetVaccinesVM { get; set; } = new();
    public ObservableCollection<DesparasitanteVM> PetDewormersVM { get; set; } = new();
    public ObservableCollection<RacaoVM> PetFoodVM { get; set; } = new();
    public ObservableCollection<ConsultaVeterinarioVM> PetConsultationsVM { get; set; } = new();

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
        try
        {
            var petDto = await _petService.FindByIdAsync(PetVM.Id);
            await Shell.Current.GoToAsync($"{nameof(PetAddOrEditPage)}", true,
                new Dictionary<string, object>
                {
                    {"PetDto", petDto}
                });

        }
        catch (Exception ex)
        {
            var s = ex.Message;
            throw;
        }
    }

    [RelayCommand]
    private async Task EditVaccineAsync(VacinaVM vaccine)
    {
        try
        {
//            IsEditing = true;
            var vaccineId = vaccine.Id;
            if (vaccineId > 0)
            {
                var response = await _petVaccinesService.FindDtoByIdAsync(vaccineId);
                SelectedVaccine = response;
                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(VaccineAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                                {"SelectedVaccine", response},
                        });
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'EditVaccineAsync", ex.Message, "Ok");
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
                await RefreshPetDetail();
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
                await ShowToastMessage($"Aplicação do desparasitante do dia  {dewormer.DataAplicacao} apagado com sucesso");
                await RefreshPetDetail();
            }
        }
        catch (Exception ex)
        {
            await ShowToastMessage($"Erro ao apagar desparasitante ({ex.Message})");
        }
    }


    public async void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        PetVM = query[nameof(PetVM)] as PetVM;

        var petId = PetVM.Id;

        PetVaccinesVM = new ObservableCollection<VacinaVM>(
            await _petVaccinesService.GetPetVaccinesVMAsync(petId)
        );

        PetDewormersVM = new ObservableCollection<DesparasitanteVM>(
            await _petDewormersService.GetDesparasitanteVMAsync(petId)
        );

        PetFoodVM = new ObservableCollection<RacaoVM>(
            await _petFoodService.GetRacaoVMAsync(petId)
        );

        PetConsultationsVM = new ObservableCollection<ConsultaVeterinarioVM>(
            await _petVeterinaryAppointmentsService.GetConsultaVMAsync(petId)
        );

        Gender = PetVM.Genero == "M" ? "Macho" : "Fêmea";
        GodFather = PetVM.Padrinho ? "Sim" : "Não";
        Sterilized = PetVM.Esterilizado ? "Sim" : "Não";
        Chipped = PetVM.Chipado ? "Sim" : "Não";
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