﻿using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.Vaccines
{
    public partial class VaccineBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _idPet;
        [ObservableProperty]
        private string _marca;
        [ObservableProperty]
        private string _dataToma;
        [ObservableProperty]
        private int _dataProximaTomaEmMeses;

        [ObservableProperty]
        private DateTime _dataProximaToma;
        [ObservableProperty]
        private int _diasParaProximaToma;

        [ObservableProperty]
        private string _nomePet;

        [ObservableProperty]
        VacinaDto _selectedVaccine;
        [ObservableProperty]
        VacinaVM _selectedVMVaccine;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private bool isEditing;

        private void AddVaccine()
        {
            IsEditing = false;
            SelectedVaccine = new()
            {
                DataToma = DateTime.Now.Date.ToShortDateString(),
                Marca = "",
                ProximaTomaEmMeses = 3,
                IdPet = 0
            };
        }
    }
}
