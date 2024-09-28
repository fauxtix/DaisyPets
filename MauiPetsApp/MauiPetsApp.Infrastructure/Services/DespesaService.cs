using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MauiPetsApp.Application.Interfaces.Repositories;
using MauiPetsApp.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using MauiPetsApp.Core.Domain;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class DespesaService : IDespesaService
    {
        private readonly IDespesaRepository _repository;
        private readonly IValidator<DespesaDto> _validator;

        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="repository"></param>
        /// <param name="validator"></param>
        /// <param name="mapper"></param>
        /// <param name="logger"></param>
        public DespesaService(IDespesaRepository repository,
            IValidator<DespesaDto> validator,
            IMapper mapper)
        {
            _repository = repository;
            _validator = validator;
            _mapper = mapper;
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
                Log.Error(ex.Message, $"Erro no update ({ex.Message})");
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

        public async Task<DespesaVM?> GetVMByIdAsync(int Id)
        {
            return await _repository.GetVMByIdAsync(Id);
        }

        /// <summary>
        /// Devolve todas as despesas
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        public async Task<IEnumerable<DespesaDto>?> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<DespesaDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<TipoDespesa>?> GetTipoDespesa_ByCategoriaDespesa(int Id)
        {
            return await _repository.GetTipoDespesa_ByCategoriaDespesa(Id);
        }

        public async Task<LookupTableVM> GetDescricaoCategoriaDespesa(int Id)
        {
            return await _repository.GetDescricaoCategoriaDespesa(Id);
        }


        public async Task<IEnumerable<TipoDespesa>?> GetTipoDespesas()
        {
            return await _repository.GetTipoDespesas();
        }

        public List<DespesaVM> Query_ByYear(string year)
        {
            throw new NotImplementedException();
        }

        public decimal TotalDespesas(int tipoDespesa = 0)
        {
            throw new NotImplementedException();
        }


        public async Task<IEnumerable<DespesaVM>?> GetAllVMAsync()
        {
            try
            {
                var expensesVM = await _repository.GetAllVMAsync();
                return expensesVM;

            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<DespesaVM>();
            }
        }

        public async Task<IEnumerable<DespesaVM>?> GetExpensesByYearAsync(int year)
        {
            try
            {
                var expensesVM = await _repository.GetExpensesByYearAsync(year);
                return expensesVM;
            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<DespesaVM>();
            }
        }

        public async Task<IEnumerable<DespesaVM>?> GetExpensesByMonthAsync(int year, int month)
        {
            try
            {
                var expensesVM = await _repository.GetExpensesByMonthAsync(year, month);
                return expensesVM;
            }
            catch (Exception ex)
            {
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<DespesaVM>();
            }
        }

        /// <summary>
        /// Validação de despesa
        /// </summary>
        /// <param name="despesa"></param>
        /// <returns></returns>
        public string RegistoComErros(DespesaDto expense)
        {
            ValidationResult results = _validator.Validate(expense);

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

