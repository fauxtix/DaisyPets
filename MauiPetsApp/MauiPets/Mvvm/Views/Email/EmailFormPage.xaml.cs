using MauiPets.Mvvm.ViewModels.Email;

namespace SendEmail.Views;

public partial class EmailFormPage : ContentPage
{
    public EmailFormPage(EmailViewModel emailViewModel)
    {
        InitializeComponent();
        BindingContext = emailViewModel;
    }
}