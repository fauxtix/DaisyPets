using CommunityToolkit.Maui;
using FluentValidation;
using MauiPets.Mvvm.ViewModels.Contacts;
using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPets.Mvvm.ViewModels.Pets;
using MauiPets.Mvvm.Views.Contacts;
using MauiPets.Mvvm.Views.Expenses;
using MauiPets.Mvvm.Views.Pets;
using MauiPetsApp.Application.Interfaces.Repositories;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.Scheduler;
using MauiPetsApp.Infrastructure.Context;
using MauiPetsApp.Infrastructure.Repositories;
using MauiPetsApp.Infrastructure.Services;
using MauiPetsApp.Infrastructure.Validators;
using Microsoft.Extensions.Configuration;
using System.Reflection;
using Plugin.LocalNotification;
using MauiPets.Mvvm.ViewModels.Todo;
using MauiPets.Mvvm.Views.Todo;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Infrastructure.Services.ToDoManager;
using MauiPetsApp.Core.Application.Interfaces.Repositories.TodoManager;
using MauiPetsApp.Infrastructure.Repositories.TodoManager;
using System.Globalization;

namespace MauiPets
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            //DatabaseHelper.CopyDatabaseIfNeeded();

            CultureInfo cultureInfo = new CultureInfo("pt-PT"); // For Portuguese culture
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

            var builder = MauiApp.CreateBuilder();
            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
            })
                .UseMauiCommunityToolkit()
                .UseLocalNotification();


#if ANDROID
            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MauiPets.appsettings.Android.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(config);
#elif WINDOWS
            using var stream = Assembly.GetExecutingAssembly()
                .GetManifestResourceStream("MauiPets.appsettings.Windows.json");
            var config = new ConfigurationBuilder().AddJsonStream(stream).Build();
            builder.Configuration.AddConfiguration(config);
#endif

            string destinationDatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB.db");

            // Check if the destination file already exists
            if (!File.Exists(destinationDatabasePath))
            {
                // Open and copy the embedded database file to the destination
                using (var sourceStream = typeof(App).Assembly.GetManifestResourceStream("MauiPets.Database.PetsDB.db"))
                {
                    using (var destinationStream = File.Create(destinationDatabasePath))
                    {
                        sourceStream.CopyTo(destinationStream);
                    }
                }
            }
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

            builder.Services.AddTransient<ContactsViewModel>();
            builder.Services.AddTransient<ContactDetailViewModel>();
            builder.Services.AddTransient<ContactAddOrEditViewModel>();

            builder.Services.AddTransient<TodoViewModel>();
            builder.Services.AddTransient<TodosAddOrEditViewModel>();

            builder.Services.AddTransient<ExpensesViewModel>();
            builder.Services.AddTransient<ExpenseAddOrEditViewModel>();

            // Views
            builder.Services.AddSingleton<PetsPage>();
            builder.Services.AddTransient<PetDetailPage>();
            builder.Services.AddTransient<PetAddOrEditPage>();

            builder.Services.AddTransient<ContactsPage>();
            builder.Services.AddTransient<ContactDetailPage>();
            builder.Services.AddTransient<AddOrEditContactPage>();

            builder.Services.AddTransient<ExpensesPage>();
            builder.Services.AddTransient<ExpensesAddOrEditPage>();

            builder.Services.AddTransient<TodoPage>();
            builder.Services.AddTransient<TodoAddOrEditPage>();

            // Database context

            builder.Services.AddTransient<IDapperContext, DapperContext>();

            // Validators
            builder.Services.AddScoped<IValidator<PetDto>, PetValidator>();
            builder.Services.AddScoped<IValidator<ContactoVM>, ContactValidator>();
            builder.Services.AddScoped<IValidator<VacinaDto>, VacinaValidator>();
            builder.Services.AddScoped<IValidator<ConsultaVeterinarioDto>, ConsultaValidator>();
            builder.Services.AddScoped<IValidator<RacaoDto>, RacaoValidator>();
            builder.Services.AddScoped<IValidator<DespesaDto>, DespesaValidator>();
            builder.Services.AddScoped<IValidator<DesparasitanteDto>, DesparasitanteValidator>();
            builder.Services.AddScoped<IValidator<DocumentoDto>, DocumentoValidator>();
            builder.Services.AddScoped<IValidator<GaleriaFotosDto>, GaleriaFotosValidator>();

            builder.Services.AddScoped<IValidator<PostDto>, PostValidator>();
            builder.Services.AddScoped<IValidator<ToDoDto>, ToDoValidator>();
            builder.Services.AddScoped<IValidator<AppointmentDataDto>, Appointmentvalidator>();
            builder.Services.AddScoped<IValidator<ContactoVM>, ContactValidator>();


            // Services
            builder.Services.AddTransient<IPetService, PetService>();
            builder.Services.AddTransient<IDespesaService, DespesaService>();
            builder.Services.AddTransient<IVacinasService, VacinasService>();
            builder.Services.AddTransient<IDesparasitanteService, DesparasitanteService>();
            builder.Services.AddTransient<IRacaoService, RacaoService>();
            builder.Services.AddTransient<IConsultaService, ConsultaService>();
            builder.Services.AddTransient<IContactService, ContactService>();
            builder.Services.AddTransient<IToDoService, ToDoService>();

            builder.Services.AddTransient<ILookupTableService, LookupTableService>();
            builder.Services.AddTransient<ITipoDespesaService, TipoDespesaService>();

            // repositories
            builder.Services.AddTransient<IPetRepository, PetRepository>();
            builder.Services.AddTransient<IDespesaRepository, DespesaRepository>();
            builder.Services.AddTransient<IVacinasRepository, VacinasRepository>();
            builder.Services.AddTransient<IDesparasitanteRepository, DesparasitanteRepository>();
            builder.Services.AddTransient<IRacaoRepository, RacaoRepository>();
            builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
            builder.Services.AddTransient<IContactRepository, ContactRepository>();
            builder.Services.AddTransient<IToDoRepository, ToDoRepository>();

            builder.Services.AddTransient<ILookupTableRepository, LookupTableRepository>();
            builder.Services.AddTransient<ITipoDespesaRepository, TipoDespesaRepository>();

            builder.Services.AddTransient<LocalNotificationCenter>();

            return builder.Build();
        }
    }
}