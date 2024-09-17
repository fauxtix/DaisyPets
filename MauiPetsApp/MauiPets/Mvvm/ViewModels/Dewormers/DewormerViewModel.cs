﻿using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Infrastructure.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MauiPets.Mvvm.ViewModels.Dewormers
{
    public partial class DewormerViewModel : DewormerBaseViewModel
    {
        public ObservableCollection<DesparasitanteDto> Dewormers { get; set; } = new();
        private readonly IDesparasitanteService _service;
        private readonly IMapper _mapper;

        private IConnectivity _connectivity;

        [ObservableProperty]
        bool isRefreshing;

        [ObservableProperty]
        string filterText = string.Empty;

        public DewormerViewModel(IDesparasitanteService dewormersService, IConnectivity connectivity, IMapper mapper)
        {
            _service = dewormersService;
            _connectivity = connectivity;
            _mapper = mapper;
        }


        [RelayCommand]
        private async Task GetDewormersAsync()
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
                var dewormers = (await _service.GetAllAsync()).ToList();

                if (dewormers.Count != 0)
                {
                    Dewormers.Clear();
                }

                foreach (var dewormer in dewormers)
                {
                    Dewormers.Add(dewormer);
                }

                FilterText = "All Dewormers";


            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Unable to get dewormers: {ex.Message}");
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