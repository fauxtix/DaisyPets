using CommunityToolkit.Mvvm.ComponentModel;
using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Mvvm.ViewModels.VetAppointments
{
    public partial class VetAppointmentsBaseViewModel : ObservableObject
    {
        [ObservableProperty]
        private int _id;
        [ObservableProperty]
        private int _idPet;
        [ObservableProperty]
        private DateTime _dataConsulta;
        [ObservableProperty]
        private string _motivo;
        [ObservableProperty]
        private string _diagnostico;
        [ObservableProperty]
        private string _tratamento;
        [ObservableProperty]
        private string _notas;

        [ObservableProperty]
        private string _nomePet;

        [ObservableProperty]
        ConsultaVeterinarioDto _selectedAppointment;
        [ObservableProperty]
        ConsultaVeterinarioVM _selectedAppointmentVM;

        [ObservableProperty]
        [NotifyPropertyChangedFor(nameof(IsNotBusy))]
        bool isBusy;
        public bool IsNotBusy => !IsBusy;

        [ObservableProperty]
        private bool isEditing;

        [ObservableProperty]
        private string _addEditCaption;
    }
}
