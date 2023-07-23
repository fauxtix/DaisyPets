using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Application.ViewModels.LookupTables;
using DaisyPets.Core.Domain;

namespace DaisyPets.Application.Interfaces.Repositories
{
    public interface IDespesaRepository
    {
        Task<int> InsertAsync(Despesa expense);
        Task<bool> UpdateAsync(int id, Despesa expense);
        Task DeleteAsync(int id);
        Task<IEnumerable<Despesa>?> GetAllAsync();
        Task<IEnumerable<DespesaVM>?> GetAllVMAsync();
        Task<Despesa?> GetByIdAsync(int Id);
        Task<DespesaVM?> GetVMByIdAsync(int Id);
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesa_ByCategoriaDespesa(int Id);
        Task<LookupTableVM> GetDescricaoCategoriaDespesa(int Id);
        decimal TotalDespesas(int iTipoDespesa = 0);
        List<DespesaVM> Query_ByYear(string sAno);
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesas();
    }
}
