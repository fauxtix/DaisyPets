using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;

namespace DaisyPets.Core.Application.Interfaces.Services
{
    public interface IConsultaService
    {
        Task DeleteAsync(int Id);
        Task<ConsultaVeterinarioDto> FindByIdAsync(int Id);
        Task<IEnumerable<ConsultaVeterinarioDto>> GetAllAsync();
        Task<IEnumerable<ConsultaVeterinarioVM>> GetAllConsultaVMAsync();
        Task<IEnumerable<ConsultaVeterinarioVM>> GetConsultaVMAsync(int Id);
        Task<int> InsertAsync(ConsultaVeterinarioDto Consulta);
        Task UpdateAsync(int Id, ConsultaVeterinarioDto Consulta);

    }
}
