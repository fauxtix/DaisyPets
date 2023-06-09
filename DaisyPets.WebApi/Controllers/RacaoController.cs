using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using DaisyPets.Infrastructure.Services;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Controller para gestão de rações
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class RacaoController : ControllerBase
    {
        private readonly IRacaoService _racaoService;
        private readonly ILogger<RacaoController> _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="racaoService"></param>
        /// <param name="logger"></param>
        public RacaoController(IRacaoService racaoService, ILogger<RacaoController> logger)
        {
            _racaoService = racaoService;
            _logger = logger;
        }

        /// <summary>
        /// Cria novo registo para Ração
        /// </summary>
        /// <param name="racao"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(RacaoDto racao)
        {
            var location = GetControllerActionNames();
            try
            {

                if (racao is null)
                {
                    return BadRequest();
                }

                var validator = new RacaoValidator();
                var result = validator.Validate(racao);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _racaoService.InsertAsync(racao);
                var viewRacao = await _racaoService.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewRacao.Id }, viewRacao);


                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza registo de ração
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="racao"></param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] RacaoDto racao)
        {
            var location = GetControllerActionNames();

            try
            {

                if (racao == null)
                {
                    string msg = "A Consulta passada como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != racao.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var validator = new RacaoValidator();
                var result = validator.Validate(racao);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var viewRacao = _racaoService.GetRacaoVMAsync(Id);
                if (viewRacao == null)
                {
                    return NotFound("Consulta não foi encontrada");
                }

                await _racaoService.UpdateAsync(Id, racao);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar ração (API)");
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Apaga registo de ração
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewRacao = _racaoService.GetRacaoVMAsync(Id);
                if (viewRacao == null)
                {
                    return NotFound("Ração não foi encontrada");
                }

                await _racaoService.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }



        /// <summary>
        /// Pesquisa ração por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var racao = await _racaoService.FindByIdAsync(Id);
                if (racao is null)
                {
                    return NotFound();
                }

                return Ok(racao);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Devolve todas as rações (VM - grid)
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllRacoesVM")]
        public async Task<IActionResult> AllRacoesVM()
        {
            try
            {
                var listOfRacoes = await _racaoService.GetAllRacoesVMAsync();
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
        /// Ração (VM) por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("RacaoVMById/{Id:int}")]
        public async Task<IActionResult> RacaoVMById(int Id)
        {
            try
            {
                var racaoVM = await _racaoService.GetRacaoVMAsync(Id);

                if (racaoVM is null)
                { return NotFound(); }

                return Ok(racaoVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das rações não encontrados");
            }
        }


        /// <summary>
        /// Devolve todas as rações de um 'Pet'
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("PetFeeds/{Id:int}")]
        public async Task<IActionResult> PetFeeds(int Id)
        {
            try
            {
                var petFeeds = await _racaoService.GetRacaoVMAsync(Id);

                if (petFeeds is null)
                {
                    return NotFound();
                }

                return Ok(petFeeds);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das rações não encontrados");
            }
        }

        /// <summary>
        /// Valida rações
        /// </summary>
        /// <param name="racao"></param>
        /// <returns></returns>
        [HttpPost("ValidateRacao")]
        public IActionResult ValidateRacao([FromBody] RacaoDto racao)
        {
            var racaoValidator = new RacaoValidator();

            var result = racaoValidator.Validate(racao);

            if (result.IsValid)
            {
                return Ok(racao);
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
