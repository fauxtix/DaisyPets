using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.Core.Domain;
using DaisyPets.Infrastructure.Services;
using DaisyPets.WebApi.Validators;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Controller para documentos do Pet
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class DocumentController : ControllerBase
    {
        private readonly IDocumentsService _documentService;
        private readonly ILogger<DocumentController> _logger;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="documentService"></param>
        /// <param name="logger"></param>
        public DocumentController(IDocumentsService documentService, ILogger<DocumentController> logger)
        {
            _documentService = documentService;
            _logger = logger;
        }

        /// <summary>
        /// Novo documento
        /// </summary>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(DocumentoDto document)
        {
            var location = GetControllerActionNames();

            try
            {
                if (document is null)
                {
                    return BadRequest();
                }

                var validator = new DocumentoValidator();
                var result = validator.Validate(document);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _documentService.InsertDocument(document);
                var viewDocument = await _documentService.GetDocument_ById(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewDocument.Id }, viewDocument);

                return Ok(new { Id = insertedId });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Atualiza documento
        /// </summary>
        /// <param name="Id"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] DocumentoDto document)
        {
            var location = GetControllerActionNames();

            try
            {
                if (document is null)
                {
                    return BadRequest();
                }

                if (Id != document.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewDocument = _documentService.GetDocument_ById(Id);
                if (viewDocument == null)
                {
                    return NotFound("Documento não foi encontrado");
                }

                var validator = new DocumentoValidator();
                var result = validator.Validate(document);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                await _documentService.UpdateDocument(Id, document);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Apaga documento
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpDelete("{Id:int}")]
        public async Task<IActionResult> Delete(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var viewDocument = _documentService.GetDocument_ById(Id);
                if (viewDocument == null)
                {
                    return NotFound("Documento não foi encontrado");
                }

                await _documentService.DeleteDocument(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Dados do documento (extendidos)
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllDocumentsVM/{Id:int}")]
        public async Task<IActionResult> GetAllDocumentsVM(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var documentsVM = await _documentService.GetAllVM(Id);
                if (documentsVM is null)
                {
                    return NotFound();
                }

                return Ok(documentsVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Pesquisa documento por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var document = await _documentService.GetDocument_ById(Id);

                if ((document is null))
                {
                    return NotFound();
                }

                return Ok(document);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        [HttpPost("ValidateDocument")]
        public IActionResult ValidateDocument([FromBody] DocumentoDto documento)
        {
            // Create validator instance (or inject it)
            var documentsValidator = new DocumentoValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = documentsValidator.Validate(documento);

            if (result.IsValid)
            {
                return Ok(documento);
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
