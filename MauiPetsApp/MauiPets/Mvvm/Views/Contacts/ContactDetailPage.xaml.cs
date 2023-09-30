using MauiPets.Mvvm.ViewModels.Contacts;

namespace MauiPets.Mvvm.Views.Contacts;

public partial class ContactDetailPage : ContentPage
{

    public ContactDetailPage(ContactDetailViewModel viewModel)
    {
        InitializeComponent();
        BindingContext = viewModel;
    }
}