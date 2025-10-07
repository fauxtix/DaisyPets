using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPets.Services
{
    public interface IPetFichaPdfService
    {
        Task<MemoryStream> GenerateFichaPetPdfAsync(PetVM pet, IEnumerable<VacinaVM> vacinas, IEnumerable<DesparasitanteVM> desparasitantes, IEnumerable<RacaoVM> racoes, IEnumerable<ConsultaVeterinarioVM> marcacoes);
    }
}