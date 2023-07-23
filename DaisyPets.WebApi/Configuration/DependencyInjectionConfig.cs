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
            services.AddScoped<IPetService, PetService>();

            services.AddScoped<IDocumentsRepository, DocumentsRepository>();
            services.AddScoped<IDocumentsService, DocumentsService>();

            services.AddScoped<IContactRepository, ContactRepository>();
            services.AddScoped<IContactService, ContactService>();

            services.AddScoped<IVacinasRepository, VacinasRepository>();
            services.AddScoped<IVacinasService, VacinasService>();

            services.AddScoped<IConsultaRepository, ConsultaRepository>();
            services.AddScoped<IConsultaService, ConsultaService>();

            services.AddScoped<IRacaoRepository, RacaoRepository>();
            services.AddScoped<IRacaoService, RacaoService>();

            services.AddScoped<IDespesaRepository, DespesaRepository>();
            services.AddScoped<IDespesaService, DespesaService>();

            services.AddScoped<ITipoDespesaRepository, TipoDespesaRepository>();
            services.AddScoped<ITipoDespesaService, TipoDespesaService>();

            services.AddScoped<IGaleriaFotosRepository, GaleriaFotosRepository>();
            services.AddScoped<IGaleriaFotosService, GaleriaFotosService>();

            services.AddScoped<IDesparasitanteRepository, DesparasitanteRepository>();
            services.AddScoped<IDesparasitanteService, DesparasitanteService>();

            services.AddScoped<ILookupTableRepository, LookupTableRepository>();
            services.AddScoped<ILookupTableService, LookupTableService>();

            services.AddScoped<IAppSettingsRepository, AppSettingsRepository>();
            services.AddScoped<IAppSettingsService, AppSettingsService>();

            services.AddScoped<IValidator<PetDto>, PetValidator>(); 
            services.AddScoped<IValidator<ContactoVM>, ContactValidator>();
            services.AddScoped<IValidator<VacinaDto>, VacinaValidator>();
            services.AddScoped<IValidator<ConsultaVeterinarioDto>, ConsultaValidator>();
            services.AddScoped<IValidator<RacaoDto>, RacaoValidator>();
            services.AddScoped<IValidator<DespesaDto>,DespesaValidator>();
            services.AddScoped<IValidator<DesparasitanteDto>, DesparasitanteValidator>();
            services.AddScoped<IValidator<DocumentoDto>, DocumentoValidator>();
            services.AddScoped<IValidator<GaleriaFotosDto>, GaleriaFotosValidator>();
        }
    }
}