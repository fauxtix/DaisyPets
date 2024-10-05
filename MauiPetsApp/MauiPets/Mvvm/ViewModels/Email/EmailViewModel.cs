using CommunityToolkit.Maui.Alerts;
using CommunityToolkit.Maui.Core;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using MauiPets.Core.Application.ViewModels.Email;
using MauiPets.Core.Application.ViewModels.Logs;
using MauiPets.Mvvm.Views.Logs;

namespace MauiPets.Mvvm.ViewModels.Email
{

    [QueryProperty(nameof(SelectedLogEntry), nameof(SelectedLogEntry))]

    public partial class EmailViewModel : ObservableObject, IQueryAttributable
    {

        [ObservableProperty]
        private LogEntry _selectedLogEntry;

        [ObservableProperty]
        private EmailVM _logEmail;
        public EmailViewModel()
        {
            LogEmail = new() { TO = "fauxtix.luix@hotmail.com", Subject = "Mensagem da App Daisy Pets / Logs" };
        }

        [RelayCommand]
        public async Task SendMail()
        {
            try
            {
                if (string.IsNullOrEmpty(LogEmail.Subject) ||
                   string.IsNullOrEmpty(LogEmail.Body) ||
                   string.IsNullOrEmpty(LogEmail.TO))
                {
                    await Shell.Current.DisplayAlert("Erro", "Preencha os campos requeridos, p.f.", "Ok");
                    return;
                }

                var message = new EmailMessage()
                {
                    Subject = LogEmail.Subject,
                    Body = LogEmail.Body,
                    To = new List<string>(LogEmail.TO.Split(';'))
                };

                if (LogEmail.CC.Length > 0)
                    message.Cc = new List<string>(LogEmail.CC.Split(';'));

                await Microsoft.Maui.ApplicationModel.Communication.Email.Default.ComposeAsync(message);

                var toast = Toast.Make($"Entrada no log de {SelectedLogEntry.TimeStamp} enviada com sucesso", ToastDuration.Short, 14);
                await toast.Show();
                await Shell.Current.GoToAsync($"{nameof(LogsMainPage)}", true);
            }
            catch (FeatureNotSupportedException fbsEx)
            {
                await Shell.Current.DisplayAlert("Erro", fbsEx.Message, "Ok");
            }
            catch (Exception ex)
            {
                await Shell.Current.DisplayAlert("Erro", ex.Message, "Ok");
            }
        }

        public void ApplyQueryAttributes(IDictionary<string, object> query)
        {
            SelectedLogEntry = query[nameof(SelectedLogEntry)] as LogEntry;
            LogEmail.Body = SelectedLogEntry.Message;
        }
    }
}
