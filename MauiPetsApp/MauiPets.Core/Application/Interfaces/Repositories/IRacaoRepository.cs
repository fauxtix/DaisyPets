using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
{
    public interface IRacaoRepository
    {
        Task DeleteAsync(int Id);
        Task<Racao> FindByIdAsync(int Id);
        Task<IEnumerable<Racao>> GetAllAsync();
        Task<IEnumerable<RacaoVM>> GetAllRacoesVMAsync();
        Task<IEnumerable<RacaoVM>> GetRacaoVMAsync(int Id);
        Task<int> InsertAsync(Racao racao);
        Task UpdateAsync(int Id, Racao racao);
    }
}