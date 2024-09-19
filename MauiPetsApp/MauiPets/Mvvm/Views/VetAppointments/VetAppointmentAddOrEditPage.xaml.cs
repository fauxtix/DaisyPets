using MauiPets.Mvvm.ViewModels.VetAppointments;

namespace MauiPets.Mvvm.Views.VetAppointments;

public partial class VetAppointmentAddOrEditPage : ContentPage
{
    public VetAppointmentAddOrEditPage(VetAppointmentsAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}