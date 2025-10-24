using CommunityToolkit.Maui;
using DaisyPets.WebApi.Helpers;
using FluentValidation;
using MauiPets.Core.Application.Interfaces.Repositories;
using MauiPets.Core.Application.Interfaces.Repositories.Logs;
using MauiPets.Core.Application.Interfaces.Repositories.Notifications;
using MauiPets.Core.Application.Interfaces.Services;
using MauiPets.Core.Application.Interfaces.Services.Notifications;
using MauiPets.Mvvm.ViewModels.Contacts;
using MauiPets.Mvvm.ViewModels.Dewormers;
using MauiPets.Mvvm.ViewModels.Email;
using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPets.Mvvm.ViewModels.Logs;
using MauiPets.Mvvm.ViewModels.Notifications;
using MauiPets.Mvvm.ViewModels.PetFood;
using MauiPets.Mvvm.ViewModels.Pets;
using MauiPets.Mvvm.ViewModels.Settings;
using MauiPets.Mvvm.ViewModels.Todo;
using MauiPets.Mvvm.ViewModels.Utilities;
using MauiPets.Mvvm.ViewModels.Vaccines;
using MauiPets.Mvvm.ViewModels.VetAppointments;
using MauiPets.Mvvm.Views.Contacts;
using MauiPets.Mvvm.Views.Dewormers;
using MauiPets.Mvvm.Views.Expenses;
using MauiPets.Mvvm.Views.Logs;
using MauiPets.Mvvm.Views.Notifications;
using MauiPets.Mvvm.Views.PetFood;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Mvvm.Views.Settings;
using MauiPets.Mvvm.Views.Settings.Expenses;
using MauiPets.Mvvm.Views.Todo;
using MauiPets.Mvvm.Views.Utilities;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPets.Mvvm.Views.VetAppointments;
using MauiPets.Services;
using MauiPetsApp.Application.Interfaces.Repositories;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.Interfaces.DapperContext;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Repositories.TodoManager;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.Interfaces.Services.TodoManager;
using MauiPetsApp.Core.Application.TodoManager;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.Scheduler;
using MauiPetsApp.Infrastructure;
using MauiPetsApp.Infrastructure.Context;
using MauiPetsApp.Infrastructure.Repositories;
using MauiPetsApp.Infrastructure.Repositories.Logs;
using MauiPetsApp.Infrastructure.Repositories.Notifications;
using MauiPetsApp.Infrastructure.Services;
using MauiPetsApp.Infrastructure.Services.Notifications;
using MauiPetsApp.Infrastructure.Services.ToDoManager;
using MauiPetsApp.Infrastructure.TodoManager;
using MauiPetsApp.Infrastructure.Validators;
using Microsoft.Extensions.Configuration;
using SendEmail.Views;
using Serilog;
using SkiaSharp.Views.Maui.Controls.Hosting;
using Syncfusion.Licensing;
using System.Globalization;
using System.Reflection;

namespace MauiPets
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            //DatabaseHelper.CopyDatabaseIfNeeded();

            SyncfusionLicenseProvider.RegisterLicense("NDA2ODYzOUAzMjM4MmUzMDJlMzBURGVydGp4M24zbTRiVUVIRjcwQyt6SmcweVliUWJCUHlOODZTcWtnV1IwPQ==");

            CultureInfo cultureInfo = new CultureInfo("pt-PT");
            CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
            CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;


            var builder = MauiApp.CreateBuilder();

            builder.UseMauiApp<App>().ConfigureFonts(fonts =>
            {
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
                fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
                fonts.AddFont("OpenSans-Medium.ttf", "sans-serif-medium");
                fonts.AddFont("NotoSerif-Bold.ttf", "NotoSerifBold");
                fonts.AddFont("Poppins-Bold.ttf", "PoppinsBold");
                fonts.AddFont("Poppins-SemiBold.ttf", "PoppinsSemiBold");
                fonts.AddFont("Poppins-Regular.ttf", "Poppins");
                fonts.AddFont("MaterialIconsOutlined-Regular.otf", "Material");
            })
                .UseMauiCommunityToolkit()
                .UseSkiaSharp();

            builder.Services.AddLocalization(options => options.ResourcesPath = "Resources/Languages");
            var savedCulture = Preferences.Get("AppLanguage", null);
            if (string.IsNullOrEmpty(savedCulture))
            {
                savedCulture = System.Globalization.CultureInfo.CurrentCulture.Name;
                Preferences.Set("AppLanguage", savedCulture);
            }
            var culture = new System.Globalization.CultureInfo(savedCulture);
            System.Globalization.CultureInfo.DefaultThreadCurrentCulture = culture;
            System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = culture;



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

            CopyDatabaseIfNeeded();

#if DEBUG
            //builder.Logging.AddDebug();
#endif


            builder.Services.AddAutoMapper(typeof(AutoMapperProfile));


            // ViewModels
            builder.Services.AddSingleton<PetViewModel>();
            builder.Services.AddTransient<PetDetailViewModel>();
            builder.Services.AddTransient<PetAddOrEditViewModel>();
            builder.Services.AddTransient<PetGalleryViewModel>();

            builder.Services.AddTransient<ContactsViewModel>();
            builder.Services.AddTransient<ContactDetailViewModel>();
            builder.Services.AddTransient<ContactAddOrEditViewModel>();

            builder.Services.AddTransient<TodoViewModel>();
            builder.Services.AddTransient<TodosAddOrEditViewModel>();

            builder.Services.AddTransient<ExpensesViewModel>();
            builder.Services.AddTransient<ExpenseAddOrEditViewModel>();
            builder.Services.AddTransient<GroupedExpensesPage>();
            builder.Services.AddTransient<GroupedExpensesViewModel>();

            builder.Services.AddTransient<VaccineViewModel>();
            builder.Services.AddTransient<VaccineAddOrEditModel>();
            builder.Services.AddTransient<TipoVacinasViewModel>();

            builder.Services.AddTransient<DewormerViewModel>();
            builder.Services.AddTransient<DewormerAddOrEditViewModel>();

            builder.Services.AddTransient<PetFoodViewModel>();
            builder.Services.AddTransient<PetFoodAddOrEditViewModel>();

            builder.Services.AddTransient<VetAppointmentsViewModel>();
            builder.Services.AddTransient<VetAppointmentsAddOrEditViewModel>();

            builder.Services.AddTransient<SettingsViewModel>();
            builder.Services.AddTransient<SettingsAddOrEditViewModel>();
            builder.Services.AddTransient<MainSettingsBaseViewModel>();
            builder.Services.AddTransient<MainSettingsViewModel>();
            builder.Services.AddTransient<ExpensesSettingsViewModel>();
            builder.Services.AddTransient<ExpenseTypesSettingsViewModel>();
            builder.Services.AddTransient<SettingsPageViewModel>();

            builder.Services.AddTransient<LogViewModel>();
            builder.Services.AddTransient<LogViewExceptionViewModel>();

            builder.Services.AddTransient<EmailViewModel>();

            builder.Services.AddTransient<BackupViewModel>();

            builder.Services.AddTransient<NotificationsViewModel>();



            // Views
            builder.Services.AddSingleton<PetsPage>();
            builder.Services.AddTransient<PetDetailPage>();
            builder.Services.AddTransient<PetAddOrEditPage>();
            builder.Services.AddTransient<PetGalleryPage>();

            builder.Services.AddTransient<ContactsPage>();
            builder.Services.AddTransient<ContactDetailPage>();
            builder.Services.AddTransient<AddOrEditContactPage>();
            builder.Services.AddTransient<LocationMapPage>();

            builder.Services.AddTransient<ExpensesPage>();
            builder.Services.AddTransient<ExpensesAddOrEditPage>();

            builder.Services.AddTransient<TodoPage>();
            builder.Services.AddTransient<TodoAddOrEditPage>();

            builder.Services.AddTransient<VaccineAddOrEditPage>();
            builder.Services.AddTransient<VaccineTypesPage>();

            builder.Services.AddTransient<DewormerAddOrEditPage>();
            builder.Services.AddTransient<PetFoodAddOrEditPage>();
            builder.Services.AddTransient<VetAppointmentAddOrEditPage>();

            builder.Services.AddTransient<SettingsPage>();
            builder.Services.AddTransient<MainSettingsPage>();
            builder.Services.AddTransient<SettingsManagementPage>();
            builder.Services.AddTransient<SettingsAddOrEditPage>();
            builder.Services.AddTransient<ExpenseSettingsPage>();
            builder.Services.AddTransient<CategoriesAddOrEditPage>();
            builder.Services.AddTransient<CategoryTypesAddOrEditPage>();

            builder.Services.AddTransient<LogsMainPage>();
            builder.Services.AddTransient<LogViewExceptionPage>();

            builder.Services.AddTransient<EmailFormPage>();
            builder.Services.AddTransient<BackupPage>();

            builder.Services.AddTransient<NotificationsPage>();


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
            builder.Services.AddScoped<IValidator<VacinaDto>, VacinaValidator>();

            // Services
            builder.Services.AddTransient<IPetService, PetService>();
            builder.Services.AddTransient<IDespesaService, DespesaService>();
            builder.Services.AddTransient<IVacinasService, VacinasService>();
            builder.Services.AddTransient<IDesparasitanteService, DesparasitanteService>();
            builder.Services.AddTransient<IRacaoService, RacaoService>();
            builder.Services.AddTransient<IConsultaService, ConsultaService>();
            builder.Services.AddTransient<IContactService, ContactService>();
            builder.Services.AddTransient<IToDoService, ToDoService>();
            builder.Services.AddTransient<IPetPhotoService, PetPhotoService>();

            builder.Services.AddTransient<ILookupTableService, LookupTableService>();
            builder.Services.AddTransient<ITipoDespesaService, TipoDespesaService>();

            builder.Services.AddSingleton<INotificationsSyncService, NotificationsSyncService>();


            // Local services
            builder.Services.AddSingleton<IPetFichaPdfService, PetFichaPdfService>();

            // repositories
            builder.Services.AddTransient<IPetRepository, PetRepository>();
            builder.Services.AddTransient<IPetPhotoRepository, PetPhotoRepository>();
            builder.Services.AddTransient<IDespesaRepository, DespesaRepository>();
            builder.Services.AddTransient<IVacinasRepository, VacinasRepository>();
            builder.Services.AddTransient<IDesparasitanteRepository, DesparasitanteRepository>();
            builder.Services.AddTransient<IRacaoRepository, RacaoRepository>();
            builder.Services.AddTransient<IConsultaRepository, ConsultaRepository>();
            builder.Services.AddTransient<IContactRepository, ContactRepository>();
            builder.Services.AddTransient<IToDoRepository, ToDoRepository>();

            builder.Services.AddTransient<ILookupTableRepository, LookupTableRepository>();
            builder.Services.AddTransient<ITipoDespesaRepository, TipoDespesaRepository>();
            builder.Services.AddTransient<ILogRepository, LogRepository>();

            builder.Services.AddSingleton<INotificationRepository, NotificationRepository>();


            SetupSerilog(builder);

            builder.Logging.AddSerilog(dispose: true);

            return builder.Build();
        }

        private static void SetupSerilog(MauiAppBuilder builder)
        {
            var serviceProvider = builder.Services.BuildServiceProvider();
            var dapperContext = serviceProvider.GetRequiredService<IDapperContext>();

            Log.Logger = new LoggerConfiguration()
                .MinimumLevel.Information()
                .WriteTo.Debug()
                .WriteTo.Sink(new SQLiteSink(dapperContext, null))
                .CreateLogger();
        }

        private static void CopyDatabaseIfNeeded()
        {
            var forceCopy = false;
            string destinationDatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB.db");

            if (!File.Exists(destinationDatabasePath) || forceCopy)
            {
                try
                {
                    using (var sourceStream = typeof(App).Assembly.GetManifestResourceStream("MauiPets.Database.PetsDB.db"))
                    {
                        if (sourceStream == null)
                        {
                            Log.Error("Embedded database resource 'MauiPets.Database.PetsDB.db' not found.");
                            return;
                        }

                        using (var destinationStream = File.Create(destinationDatabasePath))
                        {
                            sourceStream.CopyTo(destinationStream);
                        }
                    }

                    Log.Error("First run...", "Database copied to physical device");
                }
                catch (Exception ex)
                {
                    Shell.Current.DisplayAlert("Error while 'CopyDatabaseIfNeeded", ex.Message, "Ok");
                    Log.Error(ex.Message);
                    try
                    {
                        if (File.Exists(destinationDatabasePath))
                            File.Delete(destinationDatabasePath);
                    }
                    catch (Exception cleanupEx)
                    {
                        Log.Error(cleanupEx, cleanupEx.Message);
                    }
                }
            }
            else
            {
                Log.Information($"Database {destinationDatabasePath} was already copied...");
            }
        }
    }
}