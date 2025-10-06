using MauiPetsApp.Core.Domain;

namespace MauiPets.Core.Application.Interfaces.Repositories
{
    public interface IPetPhotoRepository
    {
        Task AddPhotoAsync(int petId, string filePath);
        Task DeletePhotoAsync(int photoId);
        Task<List<PetPhoto>> GetPhotosAsync(int petId);

    }
}
