using CommunityToolkit.Maui;
using MauiPets.Mvvm.ViewModels.Pets;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Infrastructure.Context;
using MauiPetsApp.Infrastructure.Repositories;
using MauiPetsApp.Infrastructure.Services;


namespace MauiPets
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
                .UseMauiCommunityToolkit();

#if DEBUG
            //builder.Logging.AddDebug();
#endif
            builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
            builder.Services.AddSingleton<IConnectivity>(Connectivity.Current);
            builder.Services.AddSingleton<IGeolocation>(Geolocation.Default);
            builder.Services.AddSingleton<IMap>(Map.Default);

            // ViewModels
            builder.Services.AddSingleton<PetViewModel>();
            builder.Services.AddTransient<PetDetailViewModel>();
            builder.Services.AddTransient<PetAddOrEditViewModel>();

            // Views
            builder.Services.AddSingleton<PetsPage>();
            builder.Services.AddTransient<PetDetailPage>();
            builder.Services.AddTransient<PetAddOrEditPage>();

            // Database context
            builder.Services.AddTransient<IDapperContext, DapperContext>();

            // Services
            builder.Services.AddSingleton<IPetService, PetService>();
            builder.Services.AddSingleton<ILookupTableService, LookupTableService>();

            // repositories
            builder.Services.AddSingleton<IPetRepository, PetRepository>();
            builder.Services.AddSingleton<ILookupTableRepository, LookupTableRepository>();

            return builder.Build();
        }
    }
}