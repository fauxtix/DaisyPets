using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Repositories
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