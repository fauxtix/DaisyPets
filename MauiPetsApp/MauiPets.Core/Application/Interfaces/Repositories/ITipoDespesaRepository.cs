using MauiPetsApp.Core.Application.ViewModels.Despesas;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
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
        Task<IEnumerable<TipoDespesa>?> GetTipoDespesa_ByCategoria(int categoria);

    }
}
