using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
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
        await Shell.Current.GoToAsync("..");
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
    }
}