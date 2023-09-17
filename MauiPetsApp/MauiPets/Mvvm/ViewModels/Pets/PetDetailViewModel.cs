using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Services;

namespace MauiPets.Mvvm.ViewModels.Pets;


[QueryProperty(nameof(PetVM), "PetVM")]

public partial class PetDetailViewModel : BaseViewModel
{
    private readonly PetService _petService;
    public PetDetailViewModel(PetService petService)
    {
        _petService = petService;
    }

    
    [ObservableProperty]
    PetVM petVM;

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
        PetVM = await _petService.GetPetVMByIdAsync(petId);

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
            var petDto = await  _petService.GetPetByIdAsync(PetVM.Id);
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
}