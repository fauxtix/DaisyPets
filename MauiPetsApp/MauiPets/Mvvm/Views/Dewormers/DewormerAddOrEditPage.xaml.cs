using MauiPets.Mvvm.ViewModels.Dewormers;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using Syncfusion.Maui.Core.Carousel;
using System.IO.Pipes;

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