using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;

namespace PropertyManagerFL.Application.Interfaces.Repositories
{
    public interface IDespesaRepository
    {
        Task<int> InsertAsync(Despesa expense);
        Task<bool> UpdateAsync(int id, Despesa expense);
        Task DeleteAsync(int id);
        Task<IEnumerable<Despesa>?> GetAllAsync();
        Task<IEnumerable<DespesaVM>?> GetAllVMAsync();
        Task<Despesa?> GetByIdAsync(int Id);
        Task<DespesaVM?> GetVMByIdAsync(int ID);
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesa_ByCategoriaDespesa(int ID);
        decimal TotalDespesas(int iTipoDespesa = 0);
        List<DespesaVM> Query_ByYear(string sAno);
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesas();
    }
}
