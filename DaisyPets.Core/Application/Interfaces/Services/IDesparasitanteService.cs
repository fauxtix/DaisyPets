using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Services
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
