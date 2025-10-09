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

        // Não é possível await no construtor, mas podes iniciar assim:
        Task.Run(AtualizarNotificacoesEBadgeAsync);
    }

    private async Task AtualizarNotificacoesEBadgeAsync()
    {
        await _syncService.SyncNotificationsAsync();
    }
}