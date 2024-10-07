using MauiPetsApp.Core.Domain.Scheduler;

namespace MauiPetsApp.Core.Application.Interfaces.Repositories.Scheduler
{
    public interface IScheduler
    {
        Task DeleteAsync(int Id);
        Task<AppointmentData> FindByIdAsync(int Id);
        Task<IEnumerable<AppointmentData>> GetAllAsync();
        Task<int> InsertAsync(AppointmentData appointment);
        Task UpdateAsync(int Id, AppointmentData appointment);
    }
}
