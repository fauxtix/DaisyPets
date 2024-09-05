using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Models;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;

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
    public IConnectivity _connectivity;
    public IPetService _petService { get; set; }
    public ILookupTableService _lookupTablesService { get; set; }

    public PetAddOrEditViewModel(IConnectivity connectivity,
                                 IPetService petService,
                                 ILookupTableService lookupTablesService)
    {
        _connectivity = connectivity;
        _petService = petService;
        _lookupTablesService = lookupTablesService;
        SetupLookupTables();
    }

    public void ApplyQueryAttributes(IDictionary<string, object> query)
    {
        PetDto = query[nameof(PetDto)] as PetDto;

        SpecieSelectedIndex = Species.FindIndex(item => item.Id == PetDto.IdEspecie);
        BreedSelectedIndex = Breeds.FindIndex(item => item.Id == PetDto.IdRaca);
        TemperamentSelectedIndex = Temperaments.FindIndex(item => item.Id == PetDto.IdTemperamento);
        SituationSelectedIndex = Situations.FindIndex(item => item.Id == PetDto.IdSituacao);
        SizeSelectedIndex = Sizes.FindIndex(item => item.Id == PetDto.IdTamanho);

        EditCaption = query[nameof(EditCaption)] as string;
        IsEditing = (bool)query[nameof(IsEditing)];

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
    //public void RegisterPicker(Picker picker, Action<object> action)
    //{
    //    if (!pickerActions.ContainsKey(picker))
    //    {
    //        pickerActions[picker] = action;
    //        PickerSelectionBehavior.SetCommand(picker, UpdateSelectedItemCommand);
    //    }
    //}

    //public void UnregisterPicker(Picker picker)
    //{
    //    if (pickerActions.ContainsKey(picker))
    //    {
    //        pickerActions.Remove(picker);
    //        PickerSelectionBehavior.SetCommand(picker, null);
    //    }
    //}


    [RelayCommand]
    async Task SavePetData()
    {
        try
        {
            if (_connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            var errorMessages = _petService.RegistoComErros(PetDto);
            if (!string.IsNullOrEmpty(errorMessages))
            {
                await Shell.Current.DisplayAlert("Verifique entradas, p.f.",
                    $"{errorMessages}", "OK");
                return;
            }

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
            await Shell.Current.DisplayAlert("Error while 'SavePetData", ex.Message, "Ok");
        }
    }
    [RelayCommand]
    async Task GoBack()
    {
        await Shell.Current.GoToAsync("..");
    }

    private void SetupLookupTables()
    {
        GetLookupData("Especie", AppEnums.Species);
        GetLookupData("Raca", AppEnums.Breeds);
        GetLookupData("Situacao", AppEnums.Situations);
        GetLookupData("Tamanho", AppEnums.Sizes);
        GetLookupData("Temperamento", AppEnums.Temperaments);
    }
    private void GetLookupData(string tableName, AppEnums option)
    {
        if (string.IsNullOrEmpty(tableName))
            return;

        try
        {
            var result = _lookupTablesService.GetLookupTableData(tableName).Result;

            if (result is null)
            {
                return;
            }

            foreach (var item in result)
            {
                switch (option)
                {
                    case AppEnums.Species:
                        Species.Add(item);
                        break;
                    case AppEnums.Breeds:
                        Breeds.Add(item);
                        break;
                    case AppEnums.Sizes:
                        Sizes.Add(item);
                        break;
                    case AppEnums.Temperaments:
                        Temperaments.Add(item);
                        break;
                    case AppEnums.Situations:
                        Situations.Add(item);
                        break;
                }
            }

            var xxx = PetDto;
        }
        catch (Exception ex)
        {
            Shell.Current.DisplayAlert("Error while 'GetLookupData", ex.Message, "Ok");
        }
    }

    [RelayCommand]
    private async Task PickImageAsync()
    {
        var result = await MediaPicker.PickPhotoAsync();
        if (result != null)
        {
            Foto = result.FullPath;
            var originalName = result.FileName;
            var customName = "My_" + originalName; // Change this as you like
            SaveImage(result.FullPath, originalName);
        }
    }

    private void SaveImage(string path, string selectedPhotoName)
    {
        var folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "Resources", "Images");
        Directory.CreateDirectory(folder);
        var destinationPath = Path.Combine(folder, selectedPhotoName);
        File.Copy(path, destinationPath, true);
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
