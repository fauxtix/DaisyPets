using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Core.Application.Interfaces.Services
{
    public interface IGaleriaFotosService
    {
        Task<GaleriaFotosDto> FindByIdAsync(int Id);
        Task<IEnumerable<GaleriaFotosDto>> GetAllAsync();
        Task<IEnumerable<GaleriaFotosVM>> GetAllPhotosVMAsync();
        Task<GaleriaFotosVM> GetPhotoVMAsync(int Id);
        Task<int> InsertAsync(GaleriaFotosDto galeria);
        Task UpdateAsync(int Id, GaleriaFotosDto galeria);
        Task DeletePhotoAsync(int photoId);

    }
}
