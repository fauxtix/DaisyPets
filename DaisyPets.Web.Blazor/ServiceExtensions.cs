using DaisyPets.Web.Blazor.Services;
using DaisyPets.Web.Blazor.Services.SEO;

namespace DaisyPets.Web.Blazor;

public static class ServiceExtensions
{
    public static void RegisterServices(this IServiceCollection services)
    {
        services.AddScoped<ILocalStorageService, LocalStorageService>();

        services.AddSingleton<MetadataProvider>();
        services.AddScoped<MetadataTransferService>();
    }
}