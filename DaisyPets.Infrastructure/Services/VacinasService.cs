using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Text;

namespace DaisyPets.Infrastructure.Services
{
    public class VacinasService : IVacinasService
    {
        private readonly IVacinasRepository _repository;
        private readonly IValidator<VacinaDto> _validator;

        private readonly IMapper _mapper;
        private readonly ILogger<VacinasService> _logger;

        public VacinasService(IVacinasRepository repository, IMapper mapper, IValidator<VacinaDto> validator, ILogger<VacinasService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }
        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<VacinaVM> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<VacinaVM>(resp);
            return output;
        }

        public async Task<IEnumerable<VacinaDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<VacinaDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<VacinaVM>> GetAllVacinaVMAsync()
        {
            try
            {
                var VacinasVM = await _repository.GetAllVacinasVMAsync();
                return VacinasVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<VacinaVM> GetVacinaVMAsync(int Id)
        {
            return await _repository.GetVacinaVMAsync(Id);
        }

        public async Task<IEnumerable<VacinaVM>> GetPetVaccinesVMAsync(int petId)
        {
            return await _repository.GetPetVaccinesVMAsync(petId);
        }


        public async Task<int> InsertAsync(VacinaDto Vacina)
        {
            var vacinaEntity = _mapper.Map<Vacina>(Vacina);
            var insertedId = await _repository.InsertAsync(vacinaEntity);
            return insertedId;
        }


        public async Task UpdateAsync(int Id, VacinaDto Vacina)
        {
            try
            {
                var VacinaEntity = await _repository.FindByIdAsync(Id);
                if (VacinaEntity == null)
                    throw new KeyNotFoundException("Vacina not found");

               var mappedModel =  _mapper.Map(Vacina, VacinaEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Erro no update do Vacinação");
                throw;
            }
        }


        /// <summary>
        /// Validação de Vacinação
        /// </summary>
        /// <param name="Vacinao"></param>
        /// <returns></returns>
        public string RegistoComErros(VacinaDto vacina)
        {
            ValidationResult results = _validator.Validate(vacina);

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
