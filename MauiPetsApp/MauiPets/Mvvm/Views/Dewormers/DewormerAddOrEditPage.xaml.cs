using MauiPets.Mvvm.ViewModels.Dewormers;

namespace MauiPets.Mvvm.Views.Dewormers;

public partial class DewormerAddOrEditPage : ContentPage
{
    private readonly DewormerAddOrEditViewModel _viewModel;
	public DewormerAddOrEditPage(DewormerAddOrEditViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }

    private void TransactionDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        _viewModel.DataAplicacao = e.NewDate;
        _viewModel.DataProximaAplicacao = e.NewDate.AddMonths(3);
    }

    private void NextApplicationDate_DateSelected(object sender, DateChangedEventArgs e)
    {
        _viewModel.DataProximaAplicacao = e.NewDate;
    }
}