using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MauiPetsApp.Core.Application.Interfaces.Repositories;
using MauiPetsApp.Core.Application.Interfaces.Services;
using MauiPetsApp.Core.Application.ViewModels;
using MauiPetsApp.Core.Domain;
using Serilog;
using System.Text;

namespace MauiPetsApp.Infrastructure.Services
{
    public class ContactService : IContactService
    {
        private readonly IContactRepository _repository;
        private readonly IValidator<ContactoVM> _validator;

        private readonly IMapper _mapper;
        public ContactService(IContactRepository repository, IMapper mapper, IValidator<ContactoVM> validator)
        {
            _repository = repository;
            _mapper = mapper;
            _validator = validator;
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
                Log.Error($"Erro: {ex.Message}");
                return Enumerable.Empty<ContactoVM>();
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
                Log.Error(ex.Message, "Erro no update do contacto");

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
                Log.Error(ex.Message, "Erro no update do contacto");
                return Enumerable.Empty<ContactoVM>();
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
