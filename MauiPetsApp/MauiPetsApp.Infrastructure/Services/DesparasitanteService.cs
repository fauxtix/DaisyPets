using AutoMapper;
using FluentValidation;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class DesparasitanteService : IDesparasitanteService
    {
        private readonly IDesparasitanteRepository _repository;
        private readonly IValidator<DesparasitanteDto> _validator;

        private readonly IMapper _mapper;
        public DesparasitanteService(IDesparasitanteRepository repository,
            IValidator<DesparasitanteDto> validator,
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

        public async Task<DesparasitanteDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<DesparasitanteDto>(resp);
            return output;
        }

        public async Task<IEnumerable<DesparasitanteDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<DesparasitanteDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<DesparasitanteVM>?> GetAllDesparasitantesVMAsync()
        {
            try
            {
                var output = await _repository.GetAllDesparasitantesVMAsync();
                return output;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return null;
            }
        }

        public async Task<IEnumerable<DesparasitanteVM>> GetDesparasitanteVMAsync(int Id)
        {
            return await _repository.GetDesparasitanteVMAsync(Id);
        }

        public async Task<int> InsertAsync(DesparasitanteDto desparasitante)
        {
            try
            {
                var identity = _mapper.Map<Desparasitante>(desparasitante);
                var insertedId = await _repository.InsertAsync(identity);
                return insertedId;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro ao inserir registo: {ex.Message}");
                return -1;
            }
        }

        public async Task UpdateAsync(int Id, DesparasitanteDto desparasitante)
        {
            try
            {
                var entity = await _repository.FindByIdAsync(Id);
                if (entity == null)
                    throw new KeyNotFoundException("Desparasitante não encontrado");

                var mappedModel = _mapper.Map(desparasitante, entity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, $"Erro no update ({ex.Message})");

            }
        }

        /// <summary>
        /// Validação de Vacina
        /// </summary>
        /// <param name="vacina"></param>
        /// <returns></returns>
        public string RegistoComErros(DesparasitanteDto desparasitante)
        {
            FluentValidation.Results.ValidationResult results = _validator.Validate(desparasitante);

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
