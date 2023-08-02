using DaisyPets.Web.Blazor.Services;

namespace DaisyPets.Web.Blazor;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();
    }
}