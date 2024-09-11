using MauiPets.Mvvm.ViewModels.VetAppointments;

namespace MauiPets.Mvvm.Views.VetAppointments;

public partial class VetAppointmentAddOrEditPage : ContentPage
{
    private readonly VetAppointmentsAddOrEditViewModel _viewModel;

    public VetAppointmentAddOrEditPage(VetAppointmentsAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}