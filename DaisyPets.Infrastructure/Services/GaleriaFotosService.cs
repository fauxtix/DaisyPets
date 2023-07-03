using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DaisyPets.Infrastructure.Services
{
    public class GaleriaFotosService : IGaleriaFotosService
    {

        private readonly IGaleriaFotosRepository _repository;
        private readonly IValidator<GaleriaFotosDto> _validator;
        private readonly IMapper _mapper;
        private readonly ILogger<GaleriaFotosService> _logger;

        public GaleriaFotosService(IGaleriaFotosRepository repository,
                                   IValidator<GaleriaFotosDto> validator,
                                   IMapper mapper,
                                   ILogger<GaleriaFotosService> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
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
                _logger.LogError($"Erro: {ex.Message}");
                throw;
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
                _logger.LogError(ex.Message, "Erro no update da foto");
                throw;
            }
        }
    }
}
