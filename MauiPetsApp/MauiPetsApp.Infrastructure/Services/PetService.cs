using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MauiPetsApp.Core.Application.Interfaces.Application;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly IMapper _mapper;
        private readonly IValidator<PetDto> _validator;

        public PetService(IPetRepository repository,
                          IMapper mapper,
                          IValidator<PetDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            try
            {
                var okToDeletePet = await _repository.CanPetBeDeleted(Id);
                if (okToDeletePet == false)
                {
                    return false;
                }

                await _repository.DeleteAsync(Id);
                return true;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return false;
            }
        }

        public async Task<PetDto> FindByIdAsync(int Id)
        {
            try
            {
                var resp = await _repository.FindByIdAsync(Id);
                var output = _mapper.Map<PetDto>(resp);
                return output;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return new();
            }
        }

        public async Task<IEnumerable<PetDto>> GetAllAsync()
        {
            try
            {
                var resp = await _repository.GetAllAsync();
                var output = _mapper.Map<IEnumerable<PetDto>>(resp);
                return output;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Enumerable.Empty<PetDto>();
            }
        }

        public async Task<IEnumerable<PetVM>> GetAllVMAsync()
        {
            try
            {
                var petsVM = await _repository.GetAllVMAsync();
                return petsVM;

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message);
                return Enumerable.Empty<PetVM>();
            }
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
                Log.Information($"Acedendo aos pesos dos animais via Service");

                var listaPesos = (await _repository.GetPesos()).ToList();
                Log.Information($"Mapeando pesos dos animais via Service, sem o AutoMapper");

                var mappedDto = new List<PesoDto>();
                foreach (var peso in listaPesos)
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
                Log.Information($"Pesos dos animais mapeado manualmente, sem recurso ao AutoMapper");

                return resp;

            }
            catch
            {
                Log.Error($"Erro ao mapear pesos dos animais com AutoMapper?");
                return Enumerable.Empty<PesoDto>();
            }
        }

        public async Task<PetVM> GetPetVMAsync(int Id)
        {
            try
            {
                return await _repository.GetPetVMAsync(Id);

            }
            catch (Exception ex)
            {

                Log.Error(ex.Message);
                return new();
            }
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
                Log.Error(ex.Message, ex);
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
                Log.Error(ex.Message, ex);
            }
        }

        /// <summary>
        /// Validação dos dados do Pet
        /// </summary>
        /// <param name="pet"></param>
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

