using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Services
{
    public interface IDesparasitanteService
    {
        Task DeleteAsync(int Id);
        Task<DesparasitanteDto> FindByIdAsync(int Id);
        Task<IEnumerable<DesparasitanteDto>> GetAllAsync();
        Task<IEnumerable<DesparasitanteVM>?> GetAllDesparasitantesVMAsync();
        Task<IEnumerable<DesparasitanteVM>> GetDesparasitanteVMAsync(int Id);
        Task<int> InsertAsync(DesparasitanteDto desparasitante);
        Task UpdateAsync(int Id, DesparasitanteDto desparasitante);

    }
}
