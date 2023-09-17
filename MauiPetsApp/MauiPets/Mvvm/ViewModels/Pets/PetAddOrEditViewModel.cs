using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Helpers;
using MauiPets.Mvvm.Behaviours.Pickers;
using MauiPets.Mvvm.Models;
using MauiPets.Mvvm.Views;
using MauiPets.Services;
using System.Collections.ObjectModel;
using System.Net.Sockets;
using System.Text.Json;
using System.Windows.Input;

namespace MauiPets.Mvvm.ViewModels.Pets;

[QueryProperty(nameof(PetDto), "PetDto")]

public partial class PetAddOrEditViewModel : BaseViewModel
{
    private Dictionary<Picker, Action<object>> pickerActions = new Dictionary<Picker, Action<object>>();
    public ICommand UpdateSelectedItemCommand { get; }

    public DevHttpsConnectionHelper devSslHelper;
    public HttpClient http;
    public JsonSerializerOptions _serializerOptions;

    public ObservableCollection<LookupTableVM> Species { get; } = new();
    public ObservableCollection<LookupTableVM> Breeds { get; } = new();
    public ObservableCollection<LookupTableVM> Situations { get; } = new();
    public ObservableCollection<LookupTableVM> Sizes { get; } = new(); 
    public ObservableCollection<LookupTableVM> Temperaments { get; } = new();

    [ObservableProperty]
    private string descricao;

    private LookupTableVM selectedSpecie;
    private LookupTableVM selectedTemperament;
    private LookupTableVM selectedSituation;
    private LookupTableVM selectedSize;
    private LookupTableVM selectedBreed;

    public LookupTableVM SelectedSpecie
    {
        get => selectedSpecie;
        set => SetProperty(ref selectedSpecie, value);
    }
    public LookupTableVM SelectedTemperament
    {
        get => selectedTemperament;
        set => SetProperty(ref selectedTemperament, value);
    }
    public LookupTableVM SelectedSituation
    {
        get => selectedSituation;
        set => SetProperty(ref selectedSituation, value);
    }
    public LookupTableVM SelectedSize
    {
        get => selectedSize;
        set => SetProperty(ref selectedSize, value);
    }
    public LookupTableVM SelectedBreed
    {
        get => selectedBreed;
        set => SetProperty(ref selectedBreed, value);
    }

    private int _selectedSpecieId = 0;
    private int _selectedTemperamentd = 0;

    public int SelectedSpecieId
    {
        get => _selectedSpecieId;
        set => SetProperty(ref _selectedSpecieId, value);
    }

    public int SelectedTemperamentId
    {
        get => _selectedTemperamentd;
        set => SetProperty(ref _selectedTemperamentd, value);
    }

    public ICommand GenericPickerSelectionCommand { get; }

    IConnectivity connectivity;
    PetService petService;
    LookupTablesService lookupTablesService;


    public PetAddOrEditViewModel(IConnectivity connectivity, PetService petService, LookupTablesService lookupTablesService)
    {
        devSslHelper = new DevHttpsConnectionHelper(sslPort: 4400);
        http = devSslHelper.HttpClient;
        _serializerOptions = new JsonSerializerOptions
        {
            IncludeFields = true,
            PropertyNameCaseInsensitive = true
        };

        this.connectivity = connectivity;
        this.petService = petService;
        this.lookupTablesService = lookupTablesService;
      
        SetupLookupTables();

        UpdateSelectedItemCommand = new RelayCommand<object>(UpdateSelectedItem);
    }

    private void UpdateSelectedItem(object selectedItem)
    {
        foreach (var kvp in pickerActions)
        {
            var picker = kvp.Key;
            var action = kvp.Value;

            if (picker.SelectedItem == selectedItem)
            {
                action?.Invoke(selectedItem);
            }
        }
    }

    public void RegisterPicker(Picker picker, Action<object> action)
    {
        if (!pickerActions.ContainsKey(picker))
        {
            pickerActions[picker] = action;
            PickerSelectionBehavior.SetCommand(picker, UpdateSelectedItemCommand);
        }
    }

    public void UnregisterPicker(Picker picker)
    {
        if (pickerActions.ContainsKey(picker))
        {
            pickerActions.Remove(picker);
            PickerSelectionBehavior.SetCommand(picker, null);
        }
    }

    [ObservableProperty]
    PetDto petDto;

    private void OnGenericPickerSelectionChanged((int Id, string PropertyName) selectionInfo)
    {
        int selectedItemId = selectionInfo.Id;
        string propertyName = selectionInfo.PropertyName;

        // Determine which data model and property to update based on the PropertyName
        switch (propertyName)
        {
            case "CustomerId":
                // Update the CustomerId property in your Customer model
                // YourModel.CustomerId = selectedItemId;
                break;
            case "OrderId":
                // Update the OrderId property in your Order model
                // YourModel.OrderId = selectedItemId;
                break;
                // Handle other properties and models as needed...
        }
    }

    [RelayCommand]
    async Task SavePetData()
    {
        try
        {
            if (connectivity.NetworkAccess != NetworkAccess.Internet)
            {
                await Shell.Current.DisplayAlert("No connectivity!",
                    $"Please check internet and try again.", "OK");
                return;
            }

            var updateOk = await petService.SavePetData(PetDto);
            if (updateOk == false )
            {
                await Shell.Current.DisplayAlert("Erro ao atualizar",
                    $"Contacte administrador, pf..", "OK");
                return;

            }

            await Shell.Current.GoToAsync("..");
        }
        catch (Exception ex)
        {

            throw;
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
            var result =  lookupTablesService.GetLookupTableData(tableName).Result;

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
        }
        catch (SocketException socketEx)
        {
            //AlertTitle = "Erro ao aceder ao servidor ";
            //AlertVisibility = true;
            //WarningMessage = socketEx.Message;
            //return Enumerable.Empty<LookupTableVM>();
        }
        catch (Exception ex)
        {
            //AlertTitle = "Erro ao aceder ao servidor ";
            //AlertVisibility = true;
            //WarningMessage = ex.Message;
            //return Enumerable.Empty<LookupTableVM>();
        }

    }


}
