using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Pets;


[QueryProperty(nameof(PetVM), "PetVM")]

public partial class PetDetailViewModel : BaseViewModel
{
    private readonly IPetService _petService;
    public PetDetailViewModel(IPetService petService)
    {
        _petService = petService;
    }


    [ObservableProperty]
    PetVM petVM;

    public string Gender => Genero == "M" ? "Male" : "Female";


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
}