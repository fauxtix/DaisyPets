using MauiPets.Core.Application.Interfaces.Repositories;
using MauiPets.Core.Application.Interfaces.Services;
using MauiPets.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Services
{
    public class PetPhotoService : IPetPhotoService
    {
        private readonly IPetPhotoRepository _petPhotoRepository;

        public PetPhotoService(IPetPhotoRepository petPhotoRepository)
        {
            _petPhotoRepository = petPhotoRepository;
        }

        public Task AddPhotoAsync(int petId, string filePath)
        {
            return _petPhotoRepository.AddPhotoAsync(petId, filePath);
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            await _petPhotoRepository.DeletePhotoAsync(photoId);
        }

        public Task<List<PetPhoto>> GetPhotosAsync(int petId)
        {
            return _petPhotoRepository.GetPhotosAsync(petId);
        }
    }
}
