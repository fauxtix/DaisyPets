using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Repositories
{
    public interface IGaleriaFotosRepository
    {
        Task DeleteAsync(int Id);
        Task<GaleriaFotos> FindByIdAsync(int Id);
        Task<IEnumerable<GaleriaFotos>> GetAllAsync();
        Task<IEnumerable<GaleriaFotosVM>> GetAllPhotosVMAsync();
        Task<GaleriaFotosVM> GetPhotoVMAsync(int Id);
        Task<int> InsertAsync(GaleriaFotos galeria);
        Task UpdateAsync(int Id, GaleriaFotos galeria);
    }
}