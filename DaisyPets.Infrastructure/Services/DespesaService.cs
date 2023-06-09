using AutoMapper;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;
using FluentValidation;
using Microsoft.Extensions.Logging;
using PropertyManagerFL.Application.Interfaces.Repositories;
using PropertyManagerFL.Application.Interfaces.Services;

namespace DaisyPets.Infrastructure.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IDespesaRepository _repository;
        private readonly IValidator<DespesaDto> _validator;

        private readonly IMapper _mapper;
        private readonly ILogger<DespesaService> _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="validator"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public DespesaService(IDespesaRepository repository,
            IValidator<DespesaDto> validator,
            IMapper mapper,
            ILogger<DespesaService> logger)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<int> InsertAsync(DespesaDto expense)
        {
            var identity = _mapper.Map<Despesa>(expense);
            var insertedId = await _repository.InsertAsync(identity);
            return insertedId;
        }

        public async Task<bool> UpdateAsync(int Id, DespesaDto expense)
        {
            try
            {
                var entity = await _repository.GetByIdAsync(Id);
                if (entity == null)
                    throw new KeyNotFoundException("Despesa não encontrada");

                var mappedModel = _mapper.Map(expense, entity);

                await _repository.UpdateAsync(Id, mappedModel);
                return true;

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"Erro no update ({ex.Message})");
                return false;
            }
        }


        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<DespesaDto> GetByIdAsync(int Id)
        {
            var resp = await _repository.GetByIdAsync(Id);
            var output = _mapper.Map<DespesaDto>(resp);
            return output;
        }

        public async Task<DespesaVM> GetVMByIdAsync(int Id)
        {
            return await _repository.GetVMByIdAsync(Id);
        }

        /// <summary>
        /// Devolve todas as despesas
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DespesaDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<DespesaDto>>(resp);
            return output;
        }



        public async Task<IEnumerable<TipoDespesa>> GetTipoDespesa_ByCategoriaDespesa(int Id)
        {
            return await _repository.GetTipoDespesa_ByCategoriaDespesa(Id);
        }

        public List<DespesaVM> Query_ByYear(string year)
        {
            throw new NotImplementedException();
        }

        public decimal TotalDespesas(int tipoDespesa = 0)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<DespesaVM>> GetAllVMAsync()
        {
            try
            {
                var expensesVM = await _repository.GetAllVMAsync();
                return expensesVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }
        }
    }
}

