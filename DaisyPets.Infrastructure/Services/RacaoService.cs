using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;

namespace DaisyPets.Infrastructure.Services
{
    public class RacaoService : IRacaoService
    {
        private readonly IRacaoRepository _repository;
        private readonly IValidator<RacaoDto> _validator;

        private readonly IMapper _mapper;
        private readonly ILogger<RacaoService> _logger;

        public RacaoService(IRacaoRepository repository, IValidator<RacaoDto> validator, IMapper mapper, ILogger<RacaoService> logger)
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

        public async Task<RacaoDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<RacaoDto>(resp);
            return output;
        }

        public async Task<IEnumerable<RacaoDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<RacaoDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<RacaoVM>> GetAllRacoesVMAsync()
        {
            try
            {
                var racoesVM = await _repository.GetAllRacoesVMAsync();
                return racoesVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<RacaoVM>> GetRacaoVMAsync(int Id)
        {
            return await _repository.GetRacaoVMAsync(Id);
        }

        public async Task<int> InsertAsync(RacaoDto racao)
        {
            var racaoIdentity = _mapper.Map<Racao>(racao);
            var insertedId = await _repository.InsertAsync(racaoIdentity);
            return insertedId;
        }

        public async Task UpdateAsync(int Id, RacaoDto racao)
        {
            try
            {
                var racaoEntity = await _repository.FindByIdAsync(Id);
                if (racaoEntity == null)
                    throw new KeyNotFoundException("Ração não encontrada");

                var mappedModel = _mapper.Map(racao, racaoEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Erro no update ({ex.Message})");
                throw;
            }
        }
    }
}
