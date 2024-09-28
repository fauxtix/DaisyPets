using AutoMapper;
using FluentValidation;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Serilog;

namespace MauiPetsApp.Infrastructure.Services
{
    public class GaleriaFotosService : IGaleriaFotosService
    {

        private readonly IGaleriaFotosRepository _repository;
        private readonly IValidator<GaleriaFotosDto> _validator;
        private readonly IMapper _mapper;
        public GaleriaFotosService(IGaleriaFotosRepository repository,
                                   IValidator<GaleriaFotosDto> validator,
                                   IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
        }

        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<GaleriaFotosDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<GaleriaFotosDto>(resp);
            return output;
        }

        public async Task<IEnumerable<GaleriaFotosDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<GaleriaFotosDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<GaleriaFotosVM>> GetAllPhotosVMAsync()
        {
            try
            {
                var photosVM = await _repository.GetAllPhotosVMAsync();
                return photosVM;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<GaleriaFotosVM>();

            }
        }

        public async Task<GaleriaFotosVM> GetPhotoVMAsync(int Id)
        {
            return await _repository.GetPhotoVMAsync(Id);
        }

        public async Task<int> InsertAsync(GaleriaFotosDto galeria)
        {
            var photoGalleryIdentity = _mapper.Map<GaleriaFotos>(galeria);
            var insertedId = await _repository.InsertAsync(photoGalleryIdentity);
            return insertedId;
        }

        public async Task UpdateAsync(int Id, GaleriaFotosDto galeria)
        {
            try
            {
                var photoGalleryIdentity = await _repository.FindByIdAsync(Id);
                if (photoGalleryIdentity == null)
                    throw new KeyNotFoundException("Photo not found");

                var mappedModel = _mapper.Map(galeria, photoGalleryIdentity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Erro no update da foto");

            }
        }
    }
}
