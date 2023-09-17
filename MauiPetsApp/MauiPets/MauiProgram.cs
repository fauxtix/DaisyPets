using MauiPets.Mvvm.ViewModels.Pets;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Services;
using AutoMapper;
using Microsoft.Extensions.Logging;

namespace MauiPets
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder
                .UseMauiApp<App>()
                .ConfigureFonts(fonts =>
                {
                    fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                    fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                });

#if DEBUG
            builder.Logging.AddDebug();
#endif
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
            builder.Services.AddSingleton<IMap>(Map.Default);

            builder.Services.AddSingleton<PetService>();
            builder.Services.AddSingleton<LookupTablesService>();

            builder.Services.AddSingleton<PetViewModel>();
            builder.Services.AddSingleton<PetsPage>();

            builder.Services.AddTransient<PetDetailViewModel>();
            builder.Services.AddTransient<PetDetailPage>();

            builder.Services.AddTransient<PetAddOrEditViewModel>();
            builder.Services.AddTransient<PetAddOrEditPage>();

            return builder.Build();
        }
    }
}