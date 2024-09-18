using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using System.Collections.ObjectModel;
using System.Diagnostics;

namespace MauiPets.Mvvm.ViewModels.VetAppointments
{
    public partial class VetAppointmentsViewModel : VetAppointmentsBaseViewModel
    {
        public ObservableCollection<ConsultaVeterinarioDto> VetAppointments{ get; set; } = new();
        private readonly IConsultaService _service;
        private readonly IMapper _mapper;

        private IConnectivity _connectivity;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string filterText = string.Empty;

        public VetAppointmentsViewModel(IConsultaService service, IMapper mapper, IConnectivity connectivity)
        {
            _service = service;
            _mapper = mapper;
            _connectivity = connectivity;
        }

        [RelayCommand]
        private async Task GetVetAppointmentsAsync()
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
                var output = (await _service.GetAllAsync()).ToList();

                if (output.Count != 0)
                {
                    VetAppointments.Clear();
                }

                foreach (var appointment in output)
                {
                    VetAppointments.Add(appointment);
                }

                FilterText = "All Consultations";
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get pet appointments: {ex.Message}");
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
