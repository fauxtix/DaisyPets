using MauiPets.Core.Application.ViewModels;

namespace MauiPets.Core.Application.Interfaces.Repositories
{
    public interface IPetPhotoRepository
    {
        Task AddPhotoAsync(int petId, string filePath);
        Task DeletePhotoAsync(int photoId);
        Task<List<PetPhotoDto>> GetPhotosAsync(int petId);

    }
}
