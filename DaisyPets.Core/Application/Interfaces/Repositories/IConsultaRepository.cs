using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Repositories
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