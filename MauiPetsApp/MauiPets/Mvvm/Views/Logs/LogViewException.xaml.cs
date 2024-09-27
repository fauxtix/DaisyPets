using MauiPets.Mvvm.ViewModels.Logs;

namespace MauiPets.Mvvm.Views.Logs;

public partial class LogViewException : ContentPage
{
    public LogViewException(LogViewExceptionViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}