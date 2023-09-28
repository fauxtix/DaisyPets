using AutoMapper;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.Logging;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IValidator<ContactoVM> _validator;

        private readonly IMapper _mapper;
        private readonly ILogger<ContactService> _logger;

        public ContactService(IContactRepository repository, IMapper mapper, IValidator<ContactoVM> validator, ILogger<ContactService> logger)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
            _logger = logger;
        }
        public async Task DeleteAsync(int Id)
        {
            await _repository.DeleteAsync(Id);
        }

        public async Task<ContactoVM> FindByIdAsync(int Id)
        {
            var resp = await _repository.FindByIdAsync(Id);
            var output = _mapper.Map<ContactoVM>(resp);
            return output;
        }

        public async Task<IEnumerable<ContactoVM>> GetAllAsync()
        {
            var resp = await _repository.GetAllAsync();
            var output = _mapper.Map<IEnumerable<ContactoVM>>(resp);
            return output;
        }

        public async Task<IEnumerable<ContactoVM>> GetAllContactVMAsync()
        {
            try
            {
                var contactsVM = await _repository.GetAllContactVMAsync();
                return contactsVM;

            }
            catch (Exception ex)
            {
                _logger.LogError($"Erro: {ex.Message}");
                throw;
            }
        }

        public async Task<ContactoVM> GetContactVMAsync(int Id)
        {
            return await _repository.GetContactVMAsync(Id);
        }

        public async Task<int> InsertAsync(ContactoVM contact)
        {
            var contactIdentity = _mapper.Map<Contacto>(contact);
            var insertedId = await _repository.InsertAsync(contactIdentity);
            return insertedId;
        }


        public async Task UpdateAsync(int Id, ContactoVM contact)
        {
            try
            {
                var contactEntity = await _repository.FindByIdAsync(Id);
                if (contactEntity == null)
                    throw new KeyNotFoundException("Contact not found");

                _mapper.Map(contact, contactEntity);

                await _repository.UpdateAsync(Id, contactEntity);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Erro ni update do contacto");
                throw;
            }
        }

        public async Task<IEnumerable<ContactoVM>> SearchContactByNamet(string filter)
        {
            try
            {
                return await _repository.SearchContactByNamet(filter);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "Erro ni update do contacto");
                return null;
            }
        }

        /// <summary>
        /// Validação de contacto
        /// </summary>
        /// <param name="contacto"></param>
        /// <returns></returns>
        public string RegistoComErros(ContactoVM contacto)
        {
            ValidationResult results = _validator.Validate(contacto);

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
