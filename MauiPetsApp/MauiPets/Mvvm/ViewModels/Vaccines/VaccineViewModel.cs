using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.Vaccines;

public partial class VaccineViewModel : VaccineBaseViewModel
{
    public ObservableCollection<VacinaDto> Vaccines { get; set; } = new();
    private readonly IVacinasService _service;
    private readonly IMapper _mapper;


    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string filterText = string.Empty;

    public VaccineViewModel(IVacinasService vaccinesService, IMapper mapper)
    {
        _service = vaccinesService;
        _mapper = mapper;
    }

    [RelayCommand]
    private async Task GetVaccinesAsync()
    {
        try
        {
            if (IsBusy)
                return;

            IsBusy = true;

            await Task.Delay(100);
            var vaccines = (await _service.GetAllAsync()).ToList();

            if (vaccines.Count != 0)
            {
                Vaccines.Clear();
            }

            foreach (var vaccine in vaccines)
            {
                Vaccines.Add(vaccine);
            }

            FilterText = "All Vaccines";


        }
        catch (Exception ex)
        {
            Debug.WriteLine($"Unable to get vaccines: {ex.Message}");
            await Shell.Current.DisplayAlert("Error!", ex.Message, "OK");
        }
        finally
        {
            IsBusy = false;
            IsRefreshing = false;
        }
    }
}
