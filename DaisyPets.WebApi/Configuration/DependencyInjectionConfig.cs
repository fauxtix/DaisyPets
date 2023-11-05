using DaisyPets.Core.Application.Interfaces.Application;
using DaisyPets.Core.Application.Interfaces.DapperContext;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Infrastructure.Context;
using DaisyPets.Infrastructure.Repositories;
using DaisyPets.Infrastructure.Services;
using DaisyPets.WebApi.Validators;
using FluentValidation;
using DaisyPets.Application.Interfaces.Repositories;
using DaisyPets.Application.Interfaces.Services;
using DaisyPets.Infrastructure.Repositories.Blog;
using DaisyPets.Core.Application.Interfaces.Repositories.Blog;
using DaisyPets.Core.Application.Interfaces.Services.Blog;
using DaisyPets.Infrastructure.Services.Blog;
using DaisyPets.Core.Domain.Blog;
using DaisyPets.Core.Application.Interfaces.Repositories.TodoManager;
using DaisyPets.Core.Application.Interfaces.Services.TodoManager;
using DaisyPets.Infrastructure.Repositories.TodoManager;
using DaisyPets.Infrastructure.Services.ToDoManager;
using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Application.ViewModels.Scheduler;
using DaisyPets.Core.Application.Interfaces.Services.Sceduler;
using DaisyPets.Infrastructure.Services.Scheduler;
using DaisyPets.Core.Application.Interfaces.Repositories.Scheduler;
using DaisyPets.Infrastructure.Repositories.Scheduler;
using DaisyPets.WebApi.Helpers;

namespace DaisyPets.WebApi.Configuration
/// <summary>
/// Dependency Injection
/// </summary>
{
    /// <summary>
    /// Register classes (DI)
    /// </summary>
    public static class DependencyInjectionConfig
    {
        /// <summary>
        /// Add Dependency Injection Configuration
        /// </summary>
        /// <param name="services"></param>
        public static void AddDependencyInjectionConfiguration(this IServiceCollection services)
        {
            services.AddScoped<IDapperContext, DapperContext>();

            services.AddScoped<IPetRepository, PetRepository>();
            services.AddTransient<IPetService, PetService>();

            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddTransient<IDocumentsService, DocumentsService>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddTransient<IContactService, ContactService>();

            services.AddScoped<IVacinasRepository, VacinasRepository>();
            services.AddTransient<IVacinasService, VacinasService>();

            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddTransient<IConsultaService, ConsultaService>();

            services.AddScoped<IRacaoRepository, RacaoRepository>();
            services.AddTransient<IRacaoService, RacaoService>();

            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<IDespesaService, DespesaService>();

            services.AddScoped<ITipoDespesaRepository, TipoDespesaRepository>();
            services.AddTransient<ITipoDespesaService, TipoDespesaService>();

            services.AddScoped<IGaleriaFotosRepository, GaleriaFotosRepository>();
            services.AddTransient<IGaleriaFotosService, GaleriaFotosService>();

            services.AddScoped<IDesparasitanteRepository, DesparasitanteRepository>();
            services.AddTransient<IDesparasitanteService, DesparasitanteService>();

            services.AddScoped<ILookupTableRepository, LookupTableRepository>();
            services.AddTransient<ILookupTableService, LookupTableService>();

            services.AddScoped<IAppSettingsRepository, AppSettingsRepository>();
            services.AddTransient<IAppSettingsService, AppSettingsService>();

            services.AddScoped<IBlogRepository, BlogsRepository>();
            services.AddTransient<IBlogService, BlogsService>();

            services.AddScoped<IToDoRepository, ToDoRepository>();
            services.AddTransient<IToDoService, ToDoService>();
            services.AddScoped<IScheduler, SchedulerRepository>();
            services.AddTransient<ISchedulerService, SchedulerService>();

            services.AddScoped<IValidator<PetDto>, PetValidator>(); 
            services.AddScoped<IValidator<ContactoVM>, ContactValidator>();
            services.AddScoped<IValidator<VacinaDto>, VacinaValidator>();
            services.AddScoped<IValidator<ConsultaVeterinarioDto>, ConsultaValidator>();
            services.AddScoped<IValidator<RacaoDto>, RacaoValidator>();
            services.AddScoped<IValidator<DespesaDto>,DespesaValidator>();
            services.AddScoped<IValidator<DesparasitanteDto>, DesparasitanteValidator>();
            services.AddScoped<IValidator<DocumentoDto>, DocumentoValidator>();
            services.AddScoped<IValidator<GaleriaFotosDto>, GaleriaFotosValidator>();

            services.AddScoped<IValidator<PostDto>, PostValidator>();
            services.AddScoped<IValidator<ToDoDto>, ToDoValidator>();
            services.AddScoped<IValidator<AppointmentDataDto>, Appointmentvalidator>();

            services.AddScoped<UtilsService>();
        }
    }
}