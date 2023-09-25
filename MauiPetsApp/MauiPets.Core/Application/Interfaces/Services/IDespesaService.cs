using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Application.ViewModels.LookupTables;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Application.Interfaces.Services
{
    public interface IDespesaService
    {
        Task<int> InsertAsync(DespesaDto expense);
        Task<bool> UpdateAsync(int Id, DespesaDto expense);
        Task DeleteAsync(int id);
        Task<IEnumerable<DespesaDto>?> GetAllAsync();
        Task<IEnumerable<DespesaVM>?> GetAllVMAsync();
        Task<DespesaDto> GetByIdAsync(int ID);
        Task<DespesaVM?> GetVMByIdAsync(int ID);
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesa_ByCategoriaDespesa(int Id);
        decimal TotalDespesas(int tipoDespesa = 0);
        List<DespesaVM> Query_ByYear(string year);
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesas();
        Task<LookupTableVM> GetDescricaoCategoriaDespesa(int Id);
    }
}
