using MauiPets.Mvvm.ViewModels.Contacts;

namespace MauiPets.Mvvm.Views.Contacts;

public partial class ContactDetailPage : ContentPage
{
    private ContactDetailViewModel _viewModel;

    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();
        _viewModel = viewModel;
        BindingContext = _viewModel;
    }
}