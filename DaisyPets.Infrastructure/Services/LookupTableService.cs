using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Application;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using Microsoft.Extensions.Logging;
using System.Reflection.Emit;

namespace DaisyPets.Infrastructure.Services
{
    public class LookupTableService : ILookupTableService
    {
        private readonly ILookupTableRepository _repoLookupTable;
        private readonly IMapper _mapper;
        private readonly ILogger<LookupTableService> _logger;

        public LookupTableService(ILookupTableRepository repoLookupTable, IMapper mapper, ILogger<LookupTableService> logger)
        {
            _repoLookupTable = repoLookupTable;
            _mapper = mapper;
            _logger = logger;
        }

        public async Task<bool> ActualizaDetalhes(LookupTableVM table)
        {
            var lookup = _mapper.Map<LookUp>(table);
            var result =   await _repoLookupTable.ActualizaDetalhes(lookup);
            return result;
        }

        public async Task<bool> CheckFKInUse(int IdFK, string fieldToCheck, string tableToCheck)
        {
            var result = await _repoLookupTable.CheckFKInUse(IdFK, fieldToCheck, tableToCheck); 
            return result;
        }

        public async Task<bool> CheckIfRecordExist(string description, string tableName)
        {
            var result = await _repoLookupTable.CheckRegistoExist(description, tableName);
            return result;
        }

        public async Task<bool> CheckRegistoExist(string Descricao, string Tabela)
        {
            var result = await _repoLookupTable.CheckRegistoExist(Descricao, Tabela);
            return result;
        }

        public async Task<int> CriaNovoRegisto(LookupTableVM table)
        {
            var mapTable = _mapper.Map<LookUp>(table);
            var result = await _repoLookupTable.CriaNovoRegisto(mapTable);
            return result;
        }

        public async Task<bool> DeleteRegisto(int iCodigo, string Tabela)
        {
           var result = await _repoLookupTable.DeleteRegisto(iCodigo, Tabela);
            return result;
        }

        public async Task<int> GetCodByDescricao(string sDescr, string sTabela)
        {
            var result = await _repoLookupTable.GetCodByDescricao(sDescr, sTabela);
            return result;
        }

        public async Task<string> GetDescription(int id, string tableName)
        {
            var result = await _repoLookupTable.GetDescription(id, tableName);
            return result;
        }

        public async Task<int> GetFirstId(string tableName)
        {
            var result = await _repoLookupTable.GetFirstId(tableName);
            return result;
        }

        public async Task<int> GetId(string Descricao, string Tabela)
        {
            var result = await _repoLookupTable.GetId(Descricao, Tabela);
            return result;
        }

        public async Task<int> GetLastInsertedId(string tableToCheck)
        {
            var result = await _repoLookupTable.GetLastInsertedId(tableToCheck);
            return result;
        }

        public async Task<IEnumerable<LookupTableVM>> GetLookupTableData(string tableName)
        {
            var result = await _repoLookupTable.GetLookupTableData(tableName);
            var mappedTable = _mapper.Map<IEnumerable< LookupTableVM>>(result);
            return mappedTable;
        }

        public async Task<LookupTableVM> GetRecordById(int id, string table)
        {
            var result = await _repoLookupTable.GetRecordById (id, table);
            var mappedTable = _mapper.Map<LookupTableVM>(result);
            return mappedTable;
        }

        public async Task<bool> TableHasData(string tableName)
        {
            var result = await _repoLookupTable.TableHasData(tableName);
            return result;
        }
    }
}
