using AutoMapper;
using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Repositories.Scheduler;
using DaisyPets.Core.Application.Interfaces.Services.Sceduler;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Application.ViewModels.Scheduler;
using DaisyPets.Core.Domain;
using DaisyPets.Core.Domain.Scheduler;
using Microsoft.Extensions.Logging;

namespace DaisyPets.Infrastructure.Services.Scheduler
{
    public class SchedulerService : ISchedulerService
    {
        private readonly IScheduler _repository;

        private readonly IMapper _mapper;
        private readonly ILogger<SchedulerService> _logger;

        public SchedulerService(IScheduler repository, IMapper mapper, ILogger<SchedulerService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
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

        public async Task<IEnumerable<AppointmentDataDto>> GetAllVMAsync()
        {
            try
            {
                var appointmentsVM = await _repository.GetAllVMAsync();
                return appointmentsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<AppointmentDataDto>> GetAppointmentVMAsync(int Id)
        {
            return await _repository.GetAppointmentVMAsync(Id);
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
                    throw new KeyNotFoundException("Contact not found");

                var mappedModel = _mapper.Map(appointment, appointmentEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Erro no update do contacto");
                throw;
            }
        }
    }
}
