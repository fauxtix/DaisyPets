using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
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
        PetDto = new PetDto()
        {
            Chip = "",
            Chipado = 0,
            Cor = "",
            DataChip = DateTime.Today.Date.ToString("yyyy-MM-dd"),
            DataNascimento = DateTime.Today.Date.ToString("yyyy-MM-dd"),
            DoencaCronica = "",
            Esterilizado = 1,
            Genero = "M",
            Medicacao = "",
            Nome = "",
            NumeroChip = "",
            Observacoes = "",
            Padrinho = 1,
            Foto = "icon_nopet.png",
        };
        await Shell.Current.GoToAsync($"{nameof(PetAddOrEditPage)}", true,
            new Dictionary<string, object>
            {
                    {"PetDto", PetDto}
            });
    }

    [RelayCommand]
    async Task GoBack()
    {
        var petId = Id;
        if (petId > 0)
        {
            var response = await _petService.GetPetVMAsync(petId); // await http.GetAsync(devSslHelper.DevServerRootUrl + $"/api/Pets/PetVMById/{petId}");

            if (response is not null)
            {
                Pet = response;

                await Shell.Current.GoToAsync($"//{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                            {"PetVM", Pet },
                    });

            }
        }

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
                var insertedId = await _petVaccinesService.InsertAsync(SelectedVaccine);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while updating",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var vaccineDto = await _petVaccinesService.GetPetVaccinesVMAsync(insertedId);
                //IsBusy = false;

                ShowToastMessage("Contact created succesfuly");

                await Shell.Current.GoToAsync($"//{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                    {"SelectedVaccine", vaccineDto}
                    });


            }
            else // Insert (Id > 0)
            {
                var _vaccineId = SelectedVaccine.Id;
                await _petVaccinesService.UpdateAsync(_vaccineId, SelectedVaccine);

                var vaccineDto = await _petVaccinesService.GetPetVaccinesVMAsync(_vaccineId);

                await Shell.Current.GoToAsync($"//{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                    {"SelectedVaccine", vaccineDto}
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

