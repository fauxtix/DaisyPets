using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Controller de Pets
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]


    public class PetsController : ControllerBase
    {
        private readonly IPetService _petService;
        private readonly ILogger<PetsController> _logger;

        /// <summary>
        /// Pets Controller
        /// </summary>
        /// <param name="petService"></param>
        public PetsController(IPetService petService, ILogger<PetsController> logger)
        {
            _petService = petService;
            _logger = logger;
        }

        /// <summary>
        /// Criar Pet
        /// </summary>
        /// <param name="pet">Modelo DTO</param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(PetDto pet)
        {
            var location = GetControllerActionNames();

            try
            {
                if (pet is null)
                {
                    return BadRequest();
                }

                var validator = new PetValidator();
                var result = validator.Validate(pet);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _petService.InsertAsync(pet);
                var viewPet = await _petService.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewPet.Id }, viewPet);

                return Ok(new { Id = insertedId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Atualizar Pet
        /// </summary>
        /// <param name="Id">Pet Id</param>
        /// <param name="pet">Dados do Pet</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] PetDto pet)
        {
            var location = GetControllerActionNames();

            try
            {
                if (pet is null)
                {
                    return BadRequest();
                }

                if (Id != pet.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewPet = await _petService.FindByIdAsync(Id);
                if (viewPet == null)
                {
                    return NotFound("Registo não foi encontrado");
                }

                var validator = new PetValidator();
                var result = validator.Validate(pet);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _petService.UpdateAsync(Id, pet);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
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
                var viewPet = await _petService.FindByIdAsync(Id);
                if (viewPet == null)
                {
                    return NotFound("Registo não foi encontrado");
                }

                var okToDeletePet = await _petService.DeleteAsync(Id);
                if ( okToDeletePet == false)
                {
                    return BadRequest("Pet tem registos associados. Operação cancelada");
                }

                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Get pet by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var pet = await _petService.FindByIdAsync(Id);

                if ((pet is null))
                {
                    return NotFound();
                }

                return Ok(pet);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get All Pets as a ViewModel
        /// </summary>
        /// <returns>List of pets</returns>
        [HttpGet("AllPetsVM")]
        public async Task<IActionResult> AllPetsVM()
        {
            var location = GetControllerActionNames();

            try
            {
                var petsVM = await _petService.GetAllVMAsync();
                if (petsVM is null)
                {
                    return NotFound();
                }

                return Ok(petsVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Get Pet (VM)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("PetVMById/{Id:int}")]
        public async Task<IActionResult> PetVMById(int Id)
        {
            var location = GetControllerActionNames();
            try
            {
                var petVM = await _petService.GetPetVMAsync(Id);
                if (petVM is null)
                    return NotFound();

                return Ok(petVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Lista de pesos para preencher multi column
        /// </summary>
        /// <returns></returns>
        [HttpGet("Pesos")]
        public async Task<IActionResult> GetPesosAnimais()
        {
            var location = GetControllerActionNames();

            try
            {
                var pesos = await _petService.GetPesos();
                if (pesos is null)
                    return NotFound();

                return Ok(pesos);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Descrição do animal a partir do tamanho e meses de idade
        /// </summary>
        /// <param name="tamanho"></param>
        /// <param name="meses"></param>
        /// <returns></returns>
        [HttpGet("Idade/{tamanho:int}/{meses:int}")]
        public async Task<IActionResult> GetDescriptionBySizeAndMonths(int tamanho, int meses)
        {
            var location = GetControllerActionNames();

            try
            {
                var description = await _petService.GetDescriptionBySizeAndMonths(tamanho, meses);
                if (string.IsNullOrEmpty(description))
                {
                    return NotFound();
                }
                return Ok(description);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

 
        /// <summary>
        /// Validate pet
        /// </summary>
        /// <param name="pet"></param>
        /// <returns></returns>
        [HttpPost("ValidatePets")]
        public IActionResult ValidatePets([FromBody] PetDto pet)
        {
            // Create validator instance (or inject it)
            var petsValidator = new PetValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = petsValidator.Validate(pet);

            if (result.IsValid)
            {
                return Ok(pet);
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
