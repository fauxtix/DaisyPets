using AutoMapper;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Domain;
using Microsoft.Extensions.Logging;

namespace MauiPetsApp.Infrastructure.Services
{
    public class TipoDespesaService : ITipoDespesaService
    {
        private readonly ITipoDespesaRepository _repository;
        private readonly ILogger<TipoDespesaService> _logger;
        private readonly IMapper _mapper;

        public TipoDespesaService(ITipoDespesaRepository repository, ILogger<TipoDespesaService> logger, IMapper mapper)
        {
            _repository = repository;
            _logger = logger;
            _mapper = mapper;
        }

        public async Task<int> Insert(TipoDespesaDto entity)
        {
            var expenseTypeToInsert = _mapper.Map<TipoDespesa>(entity);
            var insertedId = await _repository.InsereTipoDespesa(expenseTypeToInsert);
            return insertedId;
        }
        public async Task<bool> Update(int id, TipoDespesaDto entity)
        {
            var expenseToUpdate = _mapper.Map<TipoDespesa>(entity);
            return await _repository.AtualizaTipoDespesa(expenseToUpdate);
        }

        public async Task<bool> CanRecordBeDeleted(int id)
        {
            var canRecordBeDeleted = await _repository.CanRecordBeDeleted(id);
            return canRecordBeDeleted;
        }

        public async Task<bool> Delete(int id)
        {
            return await _repository.ApagaTipoDespesa(id);
        }

        public async Task<IEnumerable<TipoDespesaDto>?> GetAll()
        {
            var resp = await _repository.GetAll();
            var output = _mapper.Map<IEnumerable<TipoDespesaDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<TipoDespesaVM>?> GetAllVM()
        {
            var resp = await _repository.GetAllVM();
            var output = _mapper.Map<IEnumerable<TipoDespesaVM>>(resp);
            return output;

        }

        public async Task<TipoDespesaVM?> GetTipoDespesaVM_ById(int Id)
        {
            return await _repository.GetTipoDespesaVM_ById(Id);
        }

        public async Task<TipoDespesaDto?> Get_ById(int Id)
        {
            var entity = await _repository.GetTipoDespesa_ById(Id);
            var mappedRecord = _mapper.Map<TipoDespesaDto>(entity);
            return mappedRecord;
        }


        public string RegistoComErros(TipoDespesa tipoDespesa)
        {
            throw new NotImplementedException();
        }

        public string RegistoComErros(TipoDespesaDto tipoDespesa)
        {
            throw new NotImplementedException();
        }

        public bool TableHasData()
        {
            throw new NotImplementedException();
        }


        async Task<IEnumerable<TipoDespesaDto>?> ITipoDespesaService.GetTipoDespesa_ByCategoria(int categoria)
        {
            var list = await _repository.GetTipoDespesa_ByCategoria(categoria);
            var mappedList = _mapper.Map<IEnumerable<TipoDespesaDto>?>(list);
            return mappedList;
        }
    }
}
