using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Helpers;
using MauiPets.Mvvm.Views;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Services;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Text.Json;

namespace MauiPets.Mvvm.ViewModels.Pets;

public partial class PetViewModel : BaseViewModel 
{
    public DevHttpsConnectionHelper devSslHelper;
    public HttpClient http;
    public JsonSerializerOptions _serializerOptions;

    public ObservableCollection<PetVM> Pets { get; } = new();


    PetService petService;
    IConnectivity connectivity;
    IGeolocation geolocation;
    public PetViewModel(PetService petService, IConnectivity connectivity, IGeolocation geolocation)
    {
        devSslHelper = new DevHttpsConnectionHelper(sslPort: 4400);
        http = devSslHelper.HttpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };
        this.petService = petService;
        this.connectivity = connectivity;
        this.geolocation = geolocation;
    }

    [ObservableProperty]
    bool isRefreshing;

    [RelayCommand]
    private async Task GetPetsAsync()
    {
        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            if (IsBusy)
                return;

            IsBusy = true;

            var pets = await petService.GetPetsAsync();

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
        if (Id is not null)
        {
            var petId = Convert.ToInt32(Id);
            if (petId > 0)
            {
                var response = await http.GetAsync(devSslHelper.DevServerRootUrl + $"/api/Pets/PetVMById/{petId}");

                if (response.IsSuccessStatusCode)
                {
                    using (var responseStream =
                            await response.Content.ReadAsStreamAsync())
                    {
                        var data = await JsonSerializer
                         .DeserializeAsync<PetVM>(responseStream, _serializerOptions);
                        Pet = data;
                    }

                    await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                        new Dictionary<string, object>
                        {
                            {"SelectedPet", Pet }
                        });

                }
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
                        {"PetVM", petVM }
             });
    }

}

