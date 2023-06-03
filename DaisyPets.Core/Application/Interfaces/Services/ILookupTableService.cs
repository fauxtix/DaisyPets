using DaisyPets.Core.Application.ViewModels.LookupTables;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface ILookupTableService
    {
        Task<bool> CheckIfRecordExist(string description, string tableName);
        Task<string> GetDescription(int id, string tableName);
        Task<IEnumerable<LookupTableVM>> GetLookupTableData(string tableName);

        Task<bool> ActualizaDetalhes(LookupTableVM table);
        Task<bool> CheckRegistoExist(string Descricao, string Tabela);
        Task<int> CriaNovoRegisto(LookupTableVM table);
        Task<bool> DeleteRegisto(int iCodigo, string Tabela);

        Task<int> GetCodByDescricao(string sDescr, string sTabela);
        Task<int> GetId(string Descricao, string Tabela);

        Task<bool> CheckFKInUse(int IdFK, string fieldToCheck, string tableToCheck);
        Task<int> GetLastInsertedId(string tableToCheck);
        Task<LookupTableVM> GetRecordById(int id, string table);
        Task<int> GetFirstId(string tableName);
        Task<bool> TableHasData(string tableName);
    }
}
