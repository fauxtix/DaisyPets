using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.PetFood
{
    public partial class PetFoodViewModel : PetFoodBaseViewModel
    {
        public ObservableCollection<RacaoDto> PetFoods { get; set; } = new();
        private readonly IRacaoService _service;
        private readonly IMapper _mapper;


        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string filterText = string.Empty;

        public PetFoodViewModel(IRacaoService Service, IMapper mapper)
        {
            _service = Service;
            _mapper = mapper;
        }

        [RelayCommand]
        private async Task GetPetFoodsAsync()
        {

            try
            {
                if (IsBusy)
                    return;

                IsBusy = true;

                await Task.Delay(100);
                var output = (await _service.GetAllAsync()).ToList();

                if (output.Count != 0)
                {
                    PetFoods.Clear();
                }

                foreach (var petFood in output)
                {
                    PetFoods.Add(petFood);
                }

                FilterText = "All Pet Food";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get pet foods: {ex.Message}");
                await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
            }
            finally
            {
                IsBusy = false;
                IsRefreshing = false;
            }
        }

    }
}
