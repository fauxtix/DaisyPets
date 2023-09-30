using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Helpers;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace MauiPets.Mvvm.ViewModels.Pets;

public partial class PetViewModel : BaseViewModel
{
    //    public DevHttpsConnectionHelper devSslHelper;
    public HttpClient http;
    public JsonSerializerOptions _serializerOptions;

    public ObservableCollection<PetVM> Pets { get; } = new();


    private readonly IPetService _petService;
    private readonly IVacinasService _petVaccinesService;
    IConnectivity _connectivity;
    IGeolocation _geolocation;
    public PetViewModel(IConnectivity connectivity, IGeolocation geolocation, IPetService petService, IVacinasService petVaccinesService)
    {
        //devSslHelper = new DevHttpsConnectionHelper(sslPort: 4400);
        //http = devSslHelper.HttpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };
        _petService = petService;
        _connectivity = connectivity;
        _geolocation = geolocation;
        _petService = petService;
        _petVaccinesService = petVaccinesService;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    private async Task GetPetsAsync()
    {
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;

            var pets = (await _petService.GetAllVMAsync()).ToList();

            if (pets.Count != 0)
            {
                Pets.Clear();
            }

            foreach (var pet in pets)
            {
                Pets.Add(pet);
            }

        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get pets: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }

    [RelayCommand]
    private async Task GetPetAsync()
    {
        var petId = Id;
        if (petId > 0)
        {
            var response = await _petService.GetPetVMAsync(petId); // await http.GetAsync(devSslHelper.DevServerRootUrl + $"/api/Pets/PetVMById/{petId}");

            if (response is not null)
            {
                Pet = response;

                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                            {"PetVM", Pet },
                    });

            }
        }
    }

    [RelayCommand]
    private async Task DisplayPetAsync(PetVM petVM)
    {
        if (petVM is null)
        {
            return;
        }

        await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
            new Dictionary<string, object>
            {
                        {"PetVM", petVM },
             });
    }

    [RelayCommand]
    private async Task AddPetAsync()
    {
        PetDto = new()
        {
            Chip = "",
            Chipado = 0,
            Cor = "",
            DataChip = DateTime.Now.ToShortDateString(),
            DataNascimento = DateTime.Now.ToShortDateString(),
            DoencaCronica = "",
            Esterilizado = 1,
            Foto = "che.png",
            Genero = "M",
            IdPeso = 0,
            IdEspecie = 0,
            IdRaca = 0,
            IdSituacao = 0,
            IdTamanho = 0,
            IdTemperamento = 0,
            Medicacao = "",
            Nome = "",
            NumeroChip = "",
            Observacoes = "",
            Padrinho = 0
        };

        await Shell.Current.GoToAsync($"{nameof(PetAddOrEditPage)}", true,
            new Dictionary<string, object>
            {
                    {"PetDto", PetDto}
            });
    }
}

