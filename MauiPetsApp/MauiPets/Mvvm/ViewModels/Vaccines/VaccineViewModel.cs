using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPets.Mvvm.ViewModels.Vaccines;
public partial class VaccineViewModel : VaccineBaseViewModel
{
    public ObservableCollection<VacinaDto> Vaccines { get; set; } = new();
    private readonly IVacinasService _service;
    private IConnectivity _connectivity;

    [ObservableProperty]
    bool isRefreshing;

    [ObservableProperty]
    string filterText = string.Empty;

    public VaccineViewModel(IVacinasService vaccinesService, IConnectivity connectivity)
    {
        _service = vaccinesService;
        _connectivity = connectivity;
    }

    [RelayCommand]
    private async Task GetVaccinesAsync()
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


    [RelayCommand]
    private async Task EditTodoAsync(VacinaDto vaccine)
    {
        try
        {
            IsEditing = true;
            var vaccineId = vaccine.Id;
            if (vaccineId> 0)
            {
                var response = await _service.FindByIdAsync(vaccineId);

                if (response is not null)
                {

                    await Shell.Current.GoToAsync($"{nameof(VaccineAddOrEditPage)}", true,
                        new Dictionary<string, object>
                        {
                                {"SelectedVaccine", response},
                        });
                }
            }
        }
        catch (Exception ex)
        {
            await Shell.Current.DisplayAlert("Error while 'EditVaccineAsync", ex.Message, "Ok");
        }
    }
}
