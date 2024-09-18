using AutoMapper;
using FluentValidation;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class ConsultaService : IConsultaService
    {
        private readonly IConsultaRepository _repository;

        private readonly IMapper _mapper;
        private readonly ILogger<ConsultaService> _logger;
        private readonly IValidator<ConsultaVeterinarioDto> _validator;


        public ConsultaService(IConsultaRepository repository, IMapper mapper, ILogger<ConsultaService> logger, IValidator<ConsultaVeterinarioDto> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _logger = logger;
            _validator = validator;
        }

        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<ConsultaVeterinarioDto> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<ConsultaVeterinarioDto>(resp);
            return output;
        }

        public async Task<IEnumerable<ConsultaVeterinarioDto>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<ConsultaVeterinarioDto>>(resp);
            return output;
        }

        public async Task<IEnumerable<ConsultaVeterinarioVM>> GetAllConsultaVMAsync()
        {
            try
            {
                var contactsVM = await _repository.GetAllConsultaVMAsync();
                return contactsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<IEnumerable<ConsultaVeterinarioVM>> GetConsultaVMAsync(int Id)
        {
            return await _repository.GetConsultaVMAsync(Id);
        }

        public async Task<int> InsertAsync(ConsultaVeterinarioDto consulta)
        {
            var appointmentIdentity = _mapper.Map<ConsultaVeterinario>(consulta);
            var insertedId = await _repository.InsertAsync(appointmentIdentity);
            return insertedId;
        }

        public async Task UpdateAsync(int Id, ConsultaVeterinarioDto consulta)
        {
            try
            {
                var appointmentEntity = await _repository.FindByIdAsync(Id);
                if (appointmentEntity == null)
                    throw new KeyNotFoundException("Contact not found");

                var mappedModel = _mapper.Map(consulta, appointmentEntity);

                await _repository.UpdateAsync(Id, mappedModel);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Erro no update do contacto");
                throw;
            }
        }

        public string RegistoComErros(ConsultaVeterinarioDto appointment)
        {
            FluentValidation.Results.ValidationResult results = _validator.Validate(appointment);

            if (!results.IsValid)
            {
                StringBuilder sb = new StringBuilder();
                foreach (var failure in results.Errors)
                {
                    sb.AppendLine(failure.ErrorMessage);
                }
                return sb.ToString();
            }

            return "";
        }

    }
}
