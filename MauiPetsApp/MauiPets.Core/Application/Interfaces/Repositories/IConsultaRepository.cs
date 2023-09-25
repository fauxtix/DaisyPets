using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories
{
    public interface IConsultaRepository
    {
        Task DeleteAsync(int Id);
        Task<ConsultaVeterinario> FindByIdAsync(int Id);
        Task<IEnumerable<ConsultaVeterinario>> GetAllAsync();
        Task<IEnumerable<ConsultaVeterinarioVM>> GetAllConsultaVMAsync();
        Task<IEnumerable< ConsultaVeterinarioVM>> GetConsultaVMAsync(int Id);
        Task<int> InsertAsync(ConsultaVeterinario Consulta);
        Task UpdateAsync(int Id, ConsultaVeterinario Consulta);
    }
}