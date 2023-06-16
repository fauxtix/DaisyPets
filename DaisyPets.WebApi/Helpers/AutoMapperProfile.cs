namespace DaisyPets.WebApi.Helpers;

using AutoMapper;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Domain;

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
    }
}