using MauiPetsApp.Core.Application.ViewModels;

namespace MauiPetsApp.Core.Application.Interfaces.Application
{
    public interface ILookupTableRepository
    {
        Task<bool> ActualizaDetalhes(LookUp table);
        Task<bool> CheckFKInUse(int sourceFk, string fieldToCheck, string tableToCheck);
        Task<bool> CheckRegistoExist(string Descricao, string Tabela);
        Task<int> CriaNovoRegisto(LookUp tableRecord);
        Task<bool> DeleteRegisto(int iCodigo, string Tabela);
        Task<IEnumerable<LookUp>> GenericGetAll(string sTabela);
        Task<int> GetCodByDescricao(string sDescr, string sTabela);
        Task<IEnumerable<LookUp>> GetDataFromTabela(string sTabela, string sDescricao = "");
        Task<string> GetDescricao(int Codigo, string Tabela);
        Task<IEnumerable<LookUp>> GetDescricaoByDescricao(string sDescricao, string sTabela);
        Task<string> GetDescription(int id, string tableName);
        Task<int> GetId(string Descricao, string Tabela);
        Task<int> GetLastInsertedId(string tableToCheck);
        Task<IEnumerable<LookUp>> GetLookupTableData(string tableName);
        Task<LookUp> GetRecordById(int id, string table);
        Task<int> GetFirstId(string tableName);
        Task<bool> TableHasData(string tableName);
    }
}
