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


    public class VacinacaoController : ControllerBase
    {
        private readonly IVacinasService _vacinacaoService;
        private readonly ILogger<VacinacaoController> _logger;


        /// <summary>
        /// Contact controller constructor
        /// </summary>
        /// <param name="vacinacaoService"></param>
        /// <param name="logger"></param>
        public VacinacaoController(IVacinasService vacinacaoService, ILogger<VacinacaoController> logger)
        {
            _vacinacaoService = vacinacaoService;
            _logger = logger;
        }

        /// <summary>
        /// Create new vaccine
        /// </summary>
        /// <param name="vacina"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(VacinaDto vacina)
        {
            var location = GetControllerActionNames();
            try
            {

                if (vacina is null)
                {
                    return BadRequest();
                }

                var insertedId = await _vacinacaoService.InsertAsync(vacina);
                var viewvacina = await _vacinacaoService.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewvacina.Id }, viewvacina);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza vacinao
        /// </summary>
        /// <param name="Id">Id do vacinao</param>
        /// <param name="vacina">Dados do vacinao</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] VacinaDto vacina)
        {
            var location = GetControllerActionNames();

            try
            {

                if (vacina == null)
                {
                    string msg = "A vacina passada como paràmetro é incorreta.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != vacina.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewContact = await _vacinacaoService.GetVacinaVMAsync(vacina.Id);
                if (viewContact == null)
                {
                    return NotFound("Vacina não foi encontrada");
                }

                await _vacinacaoService.UpdateAsync(Id, vacina);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar vacina (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Delete Vaccine
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewVacina = _vacinacaoService.GetVacinaVMAsync(Id);
                if (viewVacina == null)
                {
                    return NotFound("Vacina não foi encontrada");
                }

                await _vacinacaoService.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Get vaccine by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var contact = await _vacinacaoService.FindByIdAsync(Id);
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
        /// Devolve todos as vacinas
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllVacinasVM")]
        public async Task<IActionResult> AllVacinasVM()
        {
            try
            {
                var listOfVacinas = await _vacinacaoService.GetAllVacinaVMAsync();
                if (listOfVacinas is null)
                { return NotFound(); }

                return Ok(listOfVacinas);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das vacinas não encontrados");
            }
        }

        /// <summary>
        /// Devolve vacinaVM por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("VacinaVMById/{Id:int}")]
        public async Task<IActionResult> VacinaVMById(int Id)
        {
            try
            {
                var vacinaVM = await _vacinacaoService.GetVacinaVMAsync(Id);

                if (vacinaVM is null)
                { return NotFound(); }

                return Ok(vacinaVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados da vacina não encontrados");
            }
        }

        /// <summary>
        /// Vacinas de um pet (grid)
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("PetVaccines/{Id:int}")]
        public async Task<IActionResult> PetVaccines(int Id)
        {
            try
            {
                var petVaccines = await _vacinacaoService.GetPetVaccinesVMAsync(Id);

                if (petVaccines is null)
                { 
                    return NotFound();
                }

                return Ok(petVaccines);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das vacinas não encontrados");
            }
        }


        /// <summary>
        /// Validate contact
        /// </summary>
        /// <param name="vacina"></param>
        /// <returns></returns>
        [HttpPost("ValidateVaccine")]
        public IActionResult ValidateVaccine([FromBody] VacinaVM vacina)
        {
            // Create validator instance (or inject it)
            var vacinaValidator = new VacinaValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = vacinaValidator.Validate(vacina);

            if (result.IsValid)
            {
                return Ok(vacina);
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
