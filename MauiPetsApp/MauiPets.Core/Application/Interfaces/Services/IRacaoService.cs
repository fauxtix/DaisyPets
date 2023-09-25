using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Services
{
    public interface IRacaoService
    {
        Task DeleteAsync(int Id);
        Task<RacaoDto> FindByIdAsync(int Id);
        Task<IEnumerable<RacaoDto>> GetAllAsync();
        Task<IEnumerable<RacaoVM>> GetAllRacoesVMAsync();
        Task<IEnumerable<RacaoVM>> GetRacaoVMAsync(int Id);
        Task<int> InsertAsync(RacaoDto racao);
        Task UpdateAsync(int Id, RacaoDto racao);


    }
}
