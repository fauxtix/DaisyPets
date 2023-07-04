using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Application;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Infrastructure.Services
{
    public class PetService : IPetService
    {
        private readonly IPetRepository _repository;
        private readonly IMapper _mapper;

        public PetService(IPetRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task<bool> DeleteAsync(int Id)
        {
            if(! await _repository.CanPetBeDeleted(Id))
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
                var req = await _repository.GetPesos();
                var resp = _mapper.Map<IEnumerable<PesoDto>>(req);
                return resp;

            }
            catch
            {
                return Enumerable.Empty<PesoDto>();
            }
        }

        public async Task<PetVM> GetPetVMAsync(int Id)
        {
            return await _repository.GetPetVMAsync(Id);
        }

        public async Task<int> InsertAsync(PetDto pet)
        {
            var petIdentity = _mapper.Map<Pet>(pet);
            var insertedId = await _repository.InsertAsync(petIdentity);
            return insertedId;
        }

        public async Task UpdateAsync(int Id, PetDto pet)
        {
            var petEntity = await _repository.FindByIdAsync(Id);
            if (petEntity == null)
                throw new KeyNotFoundException("Pet not found");

           var mappedModel =  _mapper.Map(pet, petEntity);

            await _repository.UpdateAsync(Id, mappedModel);
        }
    }
}
