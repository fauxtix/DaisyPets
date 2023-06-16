using DaisyPets.Core.Application.ViewModels.Despesas;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface ITipoDespesaService
    {
        Task<int> Insert(TipoDespesaDto entity);
        Task<bool> Update(int id, TipoDespesaDto entity);
        Task<bool> Delete(int id);
        Task<TipoDespesaDto?> Get_ById(int id);
        Task<TipoDespesaVM?> GetTipoDespesaVM_ById(int id);
        Task<IEnumerable<TipoDespesaDto>?> GetAll();
        Task<IEnumerable<TipoDespesaVM>?> GetAllVM();
        string RegistoComErros(TipoDespesaDto tipoDespesa);
        Task<int> GetID_ByDescription(string Descricao);
        Task<IEnumerable<TipoDespesaDto>?> GetTipoDespesa_ByCategoria(int categoria);
        Task<bool> CanRecordBeDeleted(int id);

    }
}
