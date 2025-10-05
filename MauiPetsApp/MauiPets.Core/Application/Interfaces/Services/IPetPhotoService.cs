using MauiPets.Core.Application.ViewModels;

namespace MauiPets.Core.Application.Interfaces.Services
{
    public interface IPetPhotoService
    {
        Task AddPhotoAsync(int petId, string filePath);
        Task<List<PetPhotoDto>> GetPhotosAsync(int petId);
        Task DeletePhotoAsync(int photoId);
    }
}
