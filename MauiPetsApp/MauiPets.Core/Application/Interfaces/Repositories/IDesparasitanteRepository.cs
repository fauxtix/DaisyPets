using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
{
    public interface IDesparasitanteRepository
    {
        Task DeleteAsync(int Id);
        Task<Desparasitante> FindByIdAsync(int Id);
        Task<IEnumerable<Desparasitante>> GetAllAsync();
        Task<IEnumerable<DesparasitanteVM>> GetAllDesparasitantesVMAsync();
        Task<IEnumerable<DesparasitanteVM>> GetDesparasitanteVMAsync(int Id);
        Task<int> InsertAsync(Desparasitante desparasitante);
        Task UpdateAsync(int Id, Desparasitante desparasitante);
    }
}