using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly IMapper _mapper;
        private readonly ILogger<PetService> _logger;
        private readonly IValidator<PetDto> _validator;


        public PetService(IPetRepository repository,
                          IMapper mapper,
                          ILogger<PetService> logger,
                          IValidator<PetDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            var okToDeletePet = await _repository.CanPetBeDeleted(Id);
            if (okToDeletePet == false)
            {
                return false;
            }

            await _repository.DeleteAsync(Id);
            return true;
        }

        public async Task<PetDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<PetDto>(resp);
            return output;
        }

        public async Task<IEnumerable<PetDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<PetDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<PetVM>> GetAllVMAsync()
        {
            var petsVM = await _repository.GetAllVMAsync();
            return petsVM;
        }

        public async Task<string> GetDescriptionBySizeAndMonths(int IdTamanho, int meses)
        {
            try
            {
                var descriptionBySizeAndMonths = await _repository.GetDescriptionBySizeAndMonths(IdTamanho, meses);
                return descriptionBySizeAndMonths;

            }
            catch
            {
                return "";
            }
        }

        public async Task<IEnumerable<PesoDto>> GetPesos()
        {
            try
            {
                _logger.LogInformation($"Acedendo aos pesos dos animais via Service");

                var listaPesos = (await _repository.GetPesos()).ToList();
                _logger.LogInformation($"Mapeando pesos dos animais via Service, sem o AutoMapper");

                var mappedDto= new List<PesoDto>();
                foreach ( var peso in listaPesos)
                {
                    mappedDto.Add(new PesoDto()
                    {
                         Id = peso.Id,
                          A = peso.A,
                          De = peso.De,
                          Descricao = peso.Descricao,
                    });
                }

                var resp = mappedDto; 
                //var auto = _mapper.Map<IEnumerable<PesoDto>>(listaPesos);
                _logger.LogInformation($"Pesos dos animais mapeado manualmente, sem recurso ao AutoMapper");

                return resp;

            }
            catch
            {
                _logger.LogError($"Erro ao mapear pesos dos animais com AutoMapper?");
                return Enumerable.Empty<PesoDto>();
            }
        }

        public async Task<PetVM> GetPetVMAsync(int Id)
        {
            return await _repository.GetPetVMAsync(Id);
        }

        public async Task<int> InsertAsync(PetDto pet)
        {
            try
            {
                var petIdentity = _mapper.Map<Pet>(pet);
                var insertedId = await _repository.InsertAsync(petIdentity);
                return insertedId;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return -1;
            }
        }

        public async Task UpdateAsync(int Id, PetDto pet)
        {
            try
            {
                var petEntity = await _repository.FindByIdAsync(Id);
                if (petEntity == null)
                    throw new KeyNotFoundException("Pet not found");

                var mappedModel = _mapper.Map(pet, petEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validação de contacto
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        public string RegistoComErros(PetDto pet)
        {
            ValidationResult results = _validator.Validate(pet);

            if (!results.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in results.Errors)
                {
                    sb.AppendLine(failure.ErrorMessage);
                }
                return sb.ToString();
            }

            return "";
        }
    }
}

