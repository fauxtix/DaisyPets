using AutoMapper;
using MauiPets.Core.Application.Interfaces.Repositories;
using MauiPets.Core.Application.Interfaces.Services;
using MauiPets.Core.Application.ViewModels;

namespace MauiPetsApp.Infrastructure.Services
{
    public class PetPhotoService : IPetPhotoService
    {
        private readonly IPetPhotoRepository _petPhotoRepository;
        private readonly IMapper _mapper;

        public PetPhotoService(IPetPhotoRepository petPhotoRepository, IMapper mapper)
        {
            _petPhotoRepository = petPhotoRepository;
            _mapper = mapper;
        }

        public Task AddPhotoAsync(int petId, string filePath)
        {
            return _petPhotoRepository.AddPhotoAsync(petId, filePath);
        }

        public async Task DeletePhotoAsync(int photoId)
        {
            await _petPhotoRepository.DeletePhotoAsync(photoId);
        }

        public async Task<List<PetPhotoDto>> GetPhotosAsync(int petId)
        {
            var resp = await _petPhotoRepository.GetPhotosAsync(petId);

            return _mapper.Map<List<PetPhotoDto>>(resp);
        }
    }
}
