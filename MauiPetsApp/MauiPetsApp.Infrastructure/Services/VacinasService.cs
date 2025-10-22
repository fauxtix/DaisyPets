using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class VacinasService : IVacinasService
    {
        private readonly IVacinasRepository _repository;
        private readonly IValidator<VacinaDto> _validator;

        private readonly IMapper _mapper;
        public VacinasService(IVacinasRepository repository, IMapper mapper, IValidator<VacinaDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
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
        public async Task<VacinaDto> FindDtoByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<VacinaDto>(resp);
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
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<VacinaVM>();
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


        public async Task<int> InsertAsync(VacinaDto vaccine)
        {
            var vacinaEntity = _mapper.Map<Vacina>(vaccine);
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

                var mappedModel = _mapper.Map(Vacina, VacinaEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Erro no update da Vacinação");

            }
        }


        /// <summary>
        /// Validação de Vacina
        /// </summary>
        /// <param name="vacina"></param>
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

        public async Task<IEnumerable<TipoVacinaDto>> GetTipoVacinasAsync(int specie)
        {
            try
            {
                var resp = await _repository.GetTipoVacinasAsync(specie);
                var output = _mapper.Map<IEnumerable<TipoVacinaDto>>(resp);

                return output;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<TipoVacinaDto>();
            }

        }
    }
}
