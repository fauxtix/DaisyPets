using MauiPets.Core.Application.Interfaces.Services.Notifications;


namespace MauiPets;
public partial class App : Application
{
    private readonly INotificationsSyncService _syncService;

    public static IServiceProvider Services { get; private set; }

    public App(INotificationsSyncService syncService, IServiceProvider serviceProvider)
    {
        InitializeComponent();

        Services = serviceProvider;

        _syncService = syncService;

        MainPage = new AppShell();

        Task.Run(AtualizarNotificacoesEBadgeAsync);
    }

    private async Task AtualizarNotificacoesEBadgeAsync()
    {
        await _syncService.SyncNotificationsAsync();
    }
}