using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Repositories
{
    public interface ITipoDespesaRepository
    {
        Task<int> InsereTipoDespesa(TipoDespesa novoTipoDespesa);
        Task<bool> AtualizaTipoDespesa(TipoDespesa alteraTipoDespesa);
        Task<bool> ApagaTipoDespesa(int id);
        Task<TipoDespesa?> GetTipoDespesa_ById(int id);
        Task<IEnumerable<TipoDespesa>?> GetAll();
        Task<IEnumerable<TipoDespesaVM>?> GetAllVM();
        Task<bool> CanRecordBeDeleted(int id);
        Task<TipoDespesaVM?> GetTipoDespesaVM_ById(int Id);
    }
}
