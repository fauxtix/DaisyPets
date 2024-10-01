using MauiPets.Mvvm.ViewModels.Logs;

namespace MauiPets.Mvvm.Views.Logs;

public partial class LogViewExceptionPage : ContentPage
{
    public LogViewExceptionPage(LogViewExceptionViewModel model)
    {
        InitializeComponent();
        BindingContext = model;
    }
}