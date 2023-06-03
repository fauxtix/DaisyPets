using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Infrastructure.Services;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Contact controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]


    public class ContactsController : ControllerBase
    {
        private readonly IContactService _contactService;
        private readonly ILogger<ContactsController> _logger;


        /// <summary>
        /// Contact controller constructor
        /// </summary>
        /// <param name="contactService"></param>
        public ContactsController(IContactService contactService, ILogger<ContactsController> logger)
        {
            _contactService = contactService;
            _logger = logger;
        }

        /// <summary>
        /// Create new contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(ContactoVM contact)
        {
            var location = GetControllerActionNames();
            try
            {

                if (contact is null)
                {
                    return BadRequest();
                }

                var insertedId = await _contactService.InsertAsync(contact);
                var viewContact = await _contactService.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewContact.Id }, viewContact);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza contacto
        /// </summary>
        /// <param name="Id">Id do contacto</param>
        /// <param name="contact">Dados do contacto</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] ContactoVM contact)
        {
            var location = GetControllerActionNames();

            try
            {

                if (contact == null)
                {
                    string msg = "O Contacto passado como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != contact.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewContact = _contactService.GetContactVMAsync(Id);
                if (viewContact == null)
                {
                    return NotFound("Contacto não foi encontrado");
                }

                await _contactService.UpdateAsync(Id, contact);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar contacto (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Pet
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewContact = _contactService.GetContactVMAsync(Id);
                if (viewContact == null)
                {
                    return NotFound("Contacto não foi encontrado");
                }

                await _contactService.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Get contact by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var contact = await _contactService.FindByIdAsync(Id);
                if (contact is null)
                {
                    return NotFound();
                }

                return Ok(contact);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Devolve todos os contactos
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllContactsVM")]
        public async Task<IActionResult> AllContactsVM()
        {
            try
            {
                var listOfContacts = await _contactService.GetAllContactVMAsync();
                if (listOfContacts is null)
                { return NotFound(); }

                return Ok(listOfContacts);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados dos contactos não encontrados");
            }
        }

        /// <summary>
        /// Devolve contacto por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("ContactVMById/{Id:int}")]
        public async Task<IActionResult> ContactVMById(int Id)
        {
            try
            {
                var contactVM = await _contactService.GetContactVMAsync(Id);

                if (contactVM is null)
                { return NotFound(); }

                return Ok(contactVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados dos contactos não encontrados");
            }
        }

        /// <summary>
        /// Validate contact
        /// </summary>
        /// <param name="contact"></param>
        /// <returns></returns>
        [HttpPost("ValidateContacts")]
        public IActionResult ValidateContacts([FromBody] ContactoVM contact)
        {
            // Create validator instance (or inject it)
            var contactsValidator = new ContactValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = contactsValidator.Validate(contact);

            if (result.IsValid)
            {
                return Ok(contact);
            }

            var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
            return BadRequest(errorMessages);
        }

        private string GetControllerActionNames()
        {
            var controller = ControllerContext.ActionDescriptor.ControllerName;
            var action = ControllerContext.ActionDescriptor.ActionName;

            return $"{controller} - {action}";
        }

        private ObjectResult InternalError(string message)
        {
            _logger.LogError(message);
            return StatusCode(500, $"Algo de errado ocorreu ({message}). Contacte o Administrador");
        }

    }
}
