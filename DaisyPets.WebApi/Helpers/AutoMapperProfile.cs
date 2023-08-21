namespace DaisyPets.WebApi.Helpers;

using AutoMapper;
using DaisyPets.Core.Application.TodoManager;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Application.ViewModels.Scheduler;
using DaisyPets.Core.Domain;
using DaisyPets.Core.Domain.Blog;
using DaisyPets.Core.Domain.Scheduler;
using DaisyPets.Core.Domain.TodoManager;

/// <summary>
/// Mapeamento de entidades e DTO's
/// </summary>
public class AutoMapperProfile : Profile
{
    /// <summary>
    /// Construtor
    /// </summary>
    public AutoMapperProfile()
    {
        CreateMap<PetDto, Pet>().ReverseMap();
        CreateMap<PesoDto, Peso>().ReverseMap();

        CreateMap<ConsultaVeterinario, ConsultaVeterinarioDto>().ReverseMap();
        CreateMap<ConsultaVeterinarioDto, ConsultaVeterinarioVM>().ReverseMap();

        CreateMap<Racao, RacaoDto>().ReverseMap();
        CreateMap<RacaoDto, RacaoVM>().ReverseMap();

        CreateMap<Documento, DocumentoDto>().ReverseMap();
        CreateMap<DocumentoDto, DocumentoVM>().ReverseMap();

        CreateMap<Despesa, DespesaDto>().ReverseMap();
        CreateMap<DespesaDto, DespesaVM>().ReverseMap();

        CreateMap<Desparasitante, DesparasitanteDto>().ReverseMap();
        CreateMap<DesparasitanteDto, DesparasitanteVM>().ReverseMap();

        CreateMap<VacinaDto, Vacina>().ReverseMap();
        CreateMap<VacinaVM, Vacina>().ReverseMap();

        CreateMap<TipoDespesaDto, TipoDespesa>().ReverseMap();
        CreateMap<TipoDespesaVM, TipoDespesa>().ReverseMap();

        CreateMap<IdadeDto, Idade>().ReverseMap();

        CreateMap<ContactoVM, Contacto>().ReverseMap();

        CreateMap<LookupTableVM, LookUp>().ReverseMap();

        CreateMap<DesparasitanteExternoDto, DesparasitanteExterno>().ReverseMap();
        CreateMap<DesparasitanteInternoDto, DesparasitanteInterno>().ReverseMap();

        CreateMap<EspecieDto, Especie>().ReverseMap();

        CreateMap<EsterilizacaoDto, Esterilizacao>().ReverseMap();

        CreateMap<GaleriaFotosDto, GaleriaFotos>().ReverseMap();

        CreateMap<IdadeDto, Idade>().ReverseMap();

        CreateMap<MarcaRacaoDto, MarcaRacao>().ReverseMap();

        CreateMap<MedicacaoDto, Medicacao>().ReverseMap();

        CreateMap<PesoDto, PesoDto>().ReverseMap();

        CreateMap<RacaDto, Raca>().ReverseMap();

        CreateMap<TemperamentoDto, Temperamento>().ReverseMap();

        CreateMap<TipoDesparasitanteExternoDto, TipoDesparasitanteExterno>().ReverseMap();
        CreateMap<TipoDesparasitanteInternoDto, TipoDesparasitanteInterno>().ReverseMap();

        // Blogs
        CreateMap<PostDto, Post>().ReverseMap();
        CreateMap<CommentDto, Comment>().ReverseMap();

        // ToDo's
        CreateMap<ToDo, ToDoDto>().ReverseMap();

        // scheduler

        CreateMap<AppointmentDataDto, AppointmentData>()
            .ForMember(x => x.StartTime,
            opt => opt.MapFrom(src => ((DateTime)src.StartTime).ToShortDateString()));
        CreateMap<AppointmentDataDto, AppointmentData>()
            .ForMember(x => x.EndTime,
            opt => opt.MapFrom(src => ((DateTime)src.EndTime).ToShortDateString()));

        CreateMap<AppointmentData, AppointmentDataDto>()
            .ForMember(x => x.StartTime,
            opt => opt.MapFrom(src => DateTime.Parse(src.StartTime)));
        CreateMap<AppointmentData, AppointmentDataDto>()
            .ForMember(x => x.EndTime,
            opt => opt.MapFrom(src => DateTime.Parse(src.EndTime)));

    }
}