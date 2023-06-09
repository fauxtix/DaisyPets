using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Controller para gestão de desparasitantes
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DesparasitanteController : ControllerBase
    {
        private readonly IDesparasitanteService _desparasitanteService;
        private readonly ILogger<DesparasitanteController> _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="desparasitanteService"></param>
        /// <param name="logger"></param>
        public DesparasitanteController(IDesparasitanteService desparasitanteService, ILogger<DesparasitanteController> logger)
        {
            _desparasitanteService = desparasitanteService;
            _logger = logger;
        }

        /// <summary>
        /// Cria novo registo para Desparasitante
        /// </summary>
        /// <param name="desparasitante"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(DesparasitanteDto desparasitante)
        {
            var location = GetControllerActionNames();
            try
            {

                if (desparasitante is null)
                {
                    return BadRequest();
                }

                var validator = new DesparasitanteValidator();
                var result = validator.Validate(desparasitante);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _desparasitanteService.InsertAsync(desparasitante);
                var viewdesparasitante = await _desparasitanteService.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewdesparasitante.Id }, viewdesparasitante);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza registo de Desparasitante
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="desparasitante"></param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] DesparasitanteDto desparasitante)
        {
            var location = GetControllerActionNames();

            try
            {

                if (desparasitante == null)
                {
                    string msg = "Desparasitante passada como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != desparasitante.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewdesparasitante = _desparasitanteService.GetDesparasitanteVMAsync(Id);
                if (viewdesparasitante == null)
                {
                    return NotFound("Desparasitante não foi encontrado");
                }

                var validator = new DesparasitanteValidator();
                var result = validator.Validate(desparasitante);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _desparasitanteService.UpdateAsync(Id, desparasitante);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar desparasitante (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Apaga registo de Desparasitante
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewdesparasitante = _desparasitanteService.GetDesparasitanteVMAsync(Id);
                if (viewdesparasitante == null)
                {
                    return NotFound("Desperasitante não foi encontrado");
                }

                await _desparasitanteService.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }



        /// <summary>
        /// Pesquisa Desparasitante por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var desparasitante = await _desparasitanteService.FindByIdAsync(Id);
                if (desparasitante is null)
                {
                    return NotFound();
                }

                return Ok(desparasitante);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Devolve todas os desparasitantes (VM - grid)
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllRacoesVM")]
        public async Task<IActionResult> AllRacoesVM()
        {
            try
            {
                var listOfRacoes = await _desparasitanteService.GetAllDesparasitantesVMAsync();
                if (listOfRacoes is null)
                { return NotFound(); }

                return Ok(listOfRacoes);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das rações não encontrados");
            }
        }

        /// <summary>
        /// Desparasitante (VM) por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("desparasitanteVMById/{Id:int}")]
        public async Task<IActionResult> desparasitanteVMById(int Id)
        {
            try
            {
                var desparasitanteVM = await _desparasitanteService.GetDesparasitanteVMAsync(Id);

                if (desparasitanteVM is null)
                { return NotFound(); }

                return Ok(desparasitanteVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das rações não encontrados");
            }
        }


        /// <summary>
        /// Devolve todas os desparasitantes de um 'Pet'
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("PetDewormers/{Id:int}")]
        public async Task<IActionResult> PetDewormers(int Id)
        {
            try
            {
                var petDewormers = await _desparasitanteService.GetDesparasitanteVMAsync(Id);

                if (petDewormers is null)
                {
                    return NotFound();
                }

                return Ok(petDewormers);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados dos desparasitantes não encontrados");
            }
        }

        /// <summary>
        /// Valida rações
        /// </summary>
        /// <param name="desparasitante"></param>
        /// <returns></returns>
        [HttpPost("ValidateDesparasitantes")]
        public IActionResult Validatedesparasitante([FromBody] DesparasitanteDto desparasitante)
        {
            var desparasitanteValidator = new DesparasitanteValidator();

            var result = desparasitanteValidator.Validate(desparasitante);

            if (result.IsValid)
            {
                return Ok(desparasitante);
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
