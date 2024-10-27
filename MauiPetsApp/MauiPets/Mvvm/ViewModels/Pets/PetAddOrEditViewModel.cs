using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Models;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using Serilog;

namespace MauiPets.Mvvm.ViewModels.Pets;

[QueryProperty(nameof(PetDto), "PetDto")]

public partial class PetAddOrEditViewModel : BaseViewModel, IQueryAttributable
{

    public List<LookupTableVM> Species { get; } = new();
    public List<LookupTableVM> Breeds { get; } = new();
    public List<LookupTableVM> Situations { get; } = new();
    public List<LookupTableVM> Sizes { get; } = new();
    public List<LookupTableVM> Temperaments { get; } = new();

    [ObservableProperty]
    private string descricao;

    [ObservableProperty]
    private LookupTableVM _selectedSpecie;
    [ObservableProperty]
    private LookupTableVM _selectedTemperament;
    [ObservableProperty]
    private LookupTableVM _selectedSituation;
    [ObservableProperty]
    private LookupTableVM _selectedBreed;
    [ObservableProperty]
    private LookupTableVM _selectedSize;

    [ObservableProperty]
    private int _specieSelectedIndex;
    [ObservableProperty]
    private int _situationSelectedIndex;
    [ObservableProperty]
    private int _temperamentSelectedIndex;
    [ObservableProperty]
    private int _breedSelectedIndex;
    [ObservableProperty]
    private int _sizeSelectedIndex;

    private string selectedPickerName;
    public string SelectedPickerName
    {
        get { return selectedPickerName; }
        set { SetProperty(ref selectedPickerName, value); }
    }
    public IPetService _petService { get; set; }
    public ILookupTableService _lookupTablesService { get; set; }

    public PetAddOrEditViewModel(IPetService petService,
                                 ILookupTableService lookupTablesService)
    {
        _petService = petService;
        _lookupTablesService = lookupTablesService;
        InitializeAsync();
    }

    public async void InitializeAsync()
    {
        await SetupLookupTables();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        try
        {
            PetDto = query[nameof(PetDto)] as PetDto;

            SpecieSelectedIndex = Species.FindIndex(item => item.Id == PetDto.IdEspecie);
            BreedSelectedIndex = Breeds.FindIndex(item => item.Id == PetDto.IdRaca);
            TemperamentSelectedIndex = Temperaments.FindIndex(item => item.Id == PetDto.IdTemperamento);
            SituationSelectedIndex = Situations.FindIndex(item => item.Id == PetDto.IdSituacao);
            SizeSelectedIndex = Sizes.FindIndex(item => item.Id == PetDto.IdTamanho);

            EditCaption = query[nameof(EditCaption)] as string;
            IsEditing = (bool)query[nameof(IsEditing)];
            PetPhoto = PetDto.Foto;
        }
        catch (Exception ex)
        {
            Log.Error(ex.Message, ex);
        }
    }
    private void ExecuteUpdateSelectedItem()
    {
        switch (SelectedPickerName)
        {
            case "SpeciesPicker":
                //IdEspecie = SelectedSpecieId;
                break;
            case "TemperamentsPicker":
                //IdTemperamento = SelectedTemperamentId;
                break;
            case "SituationsPicker":
                //IdSituacao = SelectedSituationId;
                break;
        }
    }

    [RelayCommand]
    async Task SavePetData()
    {
        try
        {
            var errorMessages = _petService.RegistoComErros(PetDto);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }

            PetDto.Foto = PetPhoto;

            if (PetDto.Id == 0)
            {
                var insertedId = await _petService.InsertAsync(PetDto);
                if (insertedId == -1)
                {
                    await Shell.Current.DisplayAlert("Error while inserting",
                        $"Please contact administrator..", "OK");
                    return;
                }

                var petVM = await _petService.GetPetVMAsync(insertedId);

                ShowToastMessage("Registo criado com sucesso");

                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"PetVM", petVM }
                    });
            }
            else
            {
                await _petService.UpdateAsync(PetDto.Id, PetDto);
                var petVM = await _petService.GetPetVMAsync(PetDto.Id);
                ShowToastMessage("Registo atualizado com sucesso");
                await Shell.Current.GoToAsync($"{nameof(PetDetailPage)}", true,
                    new Dictionary<string, object>
                    {
                        {"PetVM", petVM }
                    });

            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error in 'SavePetData", ex.Message, "Ok");
        }
    }

    [RelayCommand]
    public async Task DeletePet()
    {
        var petName = PetDto.Nome;
        bool okToDelete = await Shell.Current.DisplayAlert("Confirme, por favor", $"Apaga o registo de {petName}?", "Sim", "Não");
        if (okToDelete)
        {
            var response = await _petService.DeleteAsync(PetDto.Id);
            if (!okToDelete)
            {
                await Shell.Current.DisplayAlert($"Apagar registo ({PetDto.Nome})", "Operação não permitida. Há registos associados. Verifique, p.f.", "Ok");
            }
            else
            {
                ShowToastMessage("Registo removido com sucesso");
            }
        }
    }

    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private async Task SetupLookupTables()
    {
        var lookupTasks = new List<Task>
    {
        GetLookupData("Especie", AppEnums.Species),
        GetLookupData("Raca", AppEnums.Breeds),
        GetLookupData("Situacao", AppEnums.Situations),
        GetLookupData("Tamanho", AppEnums.Sizes),
        GetLookupData("Temperamento", AppEnums.Temperaments)
    };

        await Task.WhenAll(lookupTasks);
    }
    private async Task GetLookupData(string tableName, AppEnums option)
    {
        if (string.IsNullOrEmpty(tableName))
            return;

        try
        {
            var result = await _lookupTablesService.GetLookupTableData(tableName);

            if (result is null)
                return;

            List<LookupTableVM> itemsToAdd = new List<LookupTableVM>(result);

            MainThread.BeginInvokeOnMainThread(() =>
            {
                switch (option)
                {
                    case AppEnums.Species:
                        foreach (var item in itemsToAdd)
                            Species.Add(item);
                        break;
                    case AppEnums.Breeds:
                        foreach (var item in itemsToAdd)
                            Breeds.Add(item);
                        break;
                    case AppEnums.Sizes:
                        foreach (var item in itemsToAdd)
                            Sizes.Add(item);
                        break;
                    case AppEnums.Temperaments:
                        foreach (var item in itemsToAdd)
                            Temperaments.Add(item);
                        break;
                    case AppEnums.Situations:
                        foreach (var item in itemsToAdd)
                            Situations.Add(item);
                        break;
                }
            });
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'GetLookupData", ex.Message, "Ok");
        }
    }

    [RelayCommand]
    private async Task PickImageAsync()
    {
        if (MediaPicker.Default.IsCaptureSupported)
        {
            FileResult myPhoto = await MediaPicker.Default.PickPhotoAsync();
            if (myPhoto != null)
            {
                try
                {
                    string localFilePath = Path.Combine(FileSystem.CacheDirectory, myPhoto.FileName);
                    using Stream sourceStream = await myPhoto.OpenReadAsync();
                    using FileStream localFileStream = File.OpenWrite(localFilePath);
                    await sourceStream.CopyToAsync(localFileStream);
                    PetPhoto = localFileStream.Name;

                }
                catch (Exception ex)
                {
                    Log.Error(ex.Message, ex);
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("OOPS", "Your device isn't supported!", "OK");
            }
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
