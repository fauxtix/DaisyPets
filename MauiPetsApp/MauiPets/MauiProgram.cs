using CommunityToolkit.Maui;
using DaisyPets.WebApi.Helpers;
using FluentValidation;
using MauiPets.Core.Application.Interfaces.Repositories.Logs;
using MauiPets.Mvvm.ViewModels.Contacts;
using MauiPets.Mvvm.ViewModels.Dewormers;
using MauiPets.Mvvm.ViewModels.Email;
using MauiPets.Mvvm.ViewModels.Expenses;
using MauiPets.Mvvm.ViewModels.Logs;
using MauiPets.Mvvm.ViewModels.PetFood;
using MauiPets.Mvvm.ViewModels.Pets;
using MauiPets.Mvvm.ViewModels.Settings;
using MauiPets.Mvvm.ViewModels.Todo;
using MauiPets.Mvvm.ViewModels.Vaccines;
using MauiPets.Mvvm.ViewModels.VetAppointments;
using MauiPets.Mvvm.Views.Contacts;
using MauiPets.Mvvm.Views.Dewormers;
using MauiPets.Mvvm.Views.Expenses;
using MauiPets.Mvvm.Views.Logs;
using MauiPets.Mvvm.Views.PetFood;
using MauiPets.Mvvm.Views.Pets;
using MauiPets.Mvvm.Views.Settings;
using MauiPets.Mvvm.Views.Settings.Expenses;
using MauiPets.Mvvm.Views.Todo;
using MauiPets.Mvvm.Views.Vaccines;
using MauiPets.Mvvm.Views.VetAppointments;
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
using MauiPetsApp.Infrastructure.Repositories.Logs;
using MauiPetsApp.Infrastructure.Services;
using MauiPetsApp.Infrastructure.Services.ToDoManager;
using MauiPetsApp.Infrastructure.TodoManager;
using MauiPetsApp.Infrastructure.Validators;
using Microsoft.Extensions.Configuration;
using Plugin.LocalNotification;
using SendEmail.Views;
using Serilog;
using SkiaSharp.Views.Maui.Controls.Hosting;
using System.Globalization;
using System.Reflection;

namespace MauiPets
{
    public static class MauiProgram
    {
        public static MauiApp CreateMauiApp()
        {
            //DatabaseHelper.CopyDatabaseIfNeeded();

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
                .UseLocalNotification()
                .UseSkiaSharp();




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

            builder.Services.AddTransient<LogViewModel>();
            builder.Services.AddTransient<LogViewExceptionViewModel>();

            builder.Services.AddTransient<EmailViewModel>();

            // Views
            builder.Services.AddSingleton<PetsPage>();
            builder.Services.AddTransient<PetDetailPage>();
            builder.Services.AddTransient<PetAddOrEditPage>();

            builder.Services.AddTransient<ContactsPage>();
            builder.Services.AddTransient<ContactDetailPage>();
            builder.Services.AddTransient<AddOrEditContactPage>();
            builder.Services.AddTransient<LocationMapPage>();

            builder.Services.AddTransient<ExpensesPage>();
            builder.Services.AddTransient<ExpensesAddOrEditPage>();

            builder.Services.AddTransient<TodoPage>();
            builder.Services.AddTransient<TodoAddOrEditPage>();

            builder.Services.AddTransient<VaccineAddOrEditPage>();
            builder.Services.AddTransient<DewormerAddOrEditPage>();
            builder.Services.AddTransient<PetFoodAddOrEditPage>();
            builder.Services.AddTransient<VetAppointmentAddOrEditPage>();

            builder.Services.AddTransient<MainSettingsPage>();
            builder.Services.AddTransient<SettingsManagementPage>();
            builder.Services.AddTransient<SettingsAddOrEditPage>();
            builder.Services.AddTransient<ExpenseSettingsPage>();
            builder.Services.AddTransient<CategoriesAddOrEditPage>();
            builder.Services.AddTransient<CategoryTypesAddOrEditPage>();

            builder.Services.AddTransient<LogsMainPage>();
            builder.Services.AddTransient<LogViewExceptionPage>();

            builder.Services.AddTransient<EmailFormPage>();


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
            builder.Services.AddTransient<ILogRepository, LogRepository>();

            builder.Services.AddTransient<LocalNotificationCenter>();

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

            string destinationDatabasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "PetsDB.db");

            if (!File.Exists(destinationDatabasePath))
            {
                try
                {
                    using (var sourceStream = typeof(App).Assembly.GetManifestResourceStream("MauiPets.Database.PetsDB.db"))
                    {
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
                }
            }
            else
            {
                Log.Information($"Database {destinationDatabasePath} was already copied...");
            }
        }
    }
}