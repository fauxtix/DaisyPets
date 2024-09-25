using MauiPets.Mvvm.ViewModels.Logs;

namespace MauiPets.Mvvm.Views.Logs;

public partial class LogsMainPage : ContentPage
{
    public LogsMainPage(LogViewModel viewModell)
    {
        InitializeComponent();
        BindingContext = viewModell;
    }
}