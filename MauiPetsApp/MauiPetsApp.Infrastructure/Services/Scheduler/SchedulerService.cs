using AutoMapper;
using MauiPetsApp.Core.Application.Interfaces.Repositories.Scheduler;
using MauiPetsApp.Core.Application.Interfaces.Services.Sceduler;
using MauiPetsApp.Core.Application.ViewModels.Scheduler;
using MauiPetsApp.Core.Domain.Scheduler;
using Serilog;

namespace MauiPetsApp.Infrastructure.Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IScheduler _repository;

        private readonly IMapper _mapper;
        public SchedulerService(IScheduler repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }
        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<AppointmentDataDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<AppointmentDataDto>(resp);
            return output;
        }

        public async Task<IEnumerable<AppointmentDataDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<AppointmentDataDto>>(resp);
            return output;
        }

        public async Task<int> InsertAsync(AppointmentDataDto appointment)
        {
            var appointmentIdentity = _mapper.Map<AppointmentData>(appointment);
            return await _repository.InsertAsync(appointmentIdentity);
        }

        public async Task UpdateAsync(int Id, AppointmentDataDto appointment)
        {
            try
            {
                var appointmentEntity = await _repository.FindByIdAsync(Id);
                if (appointmentEntity == null)
                    throw new KeyNotFoundException("Appointment not found");

                var mappedModel = _mapper.Map(appointment, appointmentEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                Log.Error(ex.Message, "Erro no update da marcação");

            }
        }
    }
}
