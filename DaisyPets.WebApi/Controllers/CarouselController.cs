using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.Core.Application.ViewModels;
using DaisyPets.WebApi.Attributes;
using DaisyPets.WebApi.Validators;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Photo gallery controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]


    public class CarouselController : ControllerBase
    {
        private readonly IGaleriaFotosService _photosService;
        private readonly ILogger<CarouselController> _logger;


        /// <summary>
        /// Photo gallery controller constructor
        /// </summary>
        /// <param name="photostService"></param>
        public CarouselController(IGaleriaFotosService photostService, ILogger<CarouselController> logger)
        {
            _photosService = photostService;
            _logger = logger;
        }

        /// <summary>
        /// Create new record
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> Insert(GaleriaFotosDto photo)
        {
            var location = GetControllerActionNames();
            try
            {

                if (photo is null)
                {
                    return BadRequest();
                }

                var _photos = await _photosService.GetAllPhotosVMAsync();
                var photoExist = _photos.Where(p=>p.Imagem == photo.Imagem).Any();
                if(photoExist)
                {
                    return BadRequest("Foto já existe. Operação cancelada.");
                }

                var validator = new GaleriaFotosValidator();
                var result = validator.Validate(photo);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }

                var insertedId = await _photosService.InsertAsync(photo);
                var viewPhoto = await _photosService.FindByIdAsync(insertedId);
                var actionReturned = CreatedAtAction(nameof(Get), new { id = viewPhoto.Id }, viewPhoto);

                return Ok(new { Id = insertedId });

            }
            catch (Exception e)
            {
                _logger.LogError(e.ToString());
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        /// <summary>
        /// Update record
        /// </summary>
        /// <param name="Id">Id da Foto</param>
        /// <param name="photo">Dados da Foto</param>
        /// <returns></returns>
        [HttpPut("{Id:int}")]
        public async Task<IActionResult> Update(int Id, [FromBody] GaleriaFotosDto photo)
        {
            var location = GetControllerActionNames();

            try
            {

                if (photo == null)
                {
                    string msg = "A Foto passada como paràmetro é incorreto.";
                    _logger.LogWarning(msg);
                    return BadRequest(msg);
                }

                if (Id != photo.Id)
                {
                    return BadRequest($"O id ({Id}) passado como paràmetro é incorreto");
                }

                var viewPhoto = _photosService.GetPhotoVMAsync(Id);
                if (viewPhoto == null)
                {
                    return NotFound("Foto não foi encontrada");
                }

                var validator = new GaleriaFotosValidator();
                var result = validator.Validate(photo);
                if (result.IsValid == false)
                {
                    var errorMessages = result.Errors.Select(x => x.ErrorMessage).ToList();
                    return BadRequest(errorMessages);
                }


                await _photosService.UpdateAsync(Id, photo);
                return NoContent();

            }
            catch (Exception e)
            {
                _logger.LogError(e.Message, "Erro ao atualizar Foto (API)");
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
                var viewPhoto = _photosService.GetPhotoVMAsync(Id);
                if (viewPhoto == null)
                {
                    return NotFound("Foto não foi encontrada");
                }

                await _photosService.DeleteAsync(Id);
                return NoContent();

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }


        /// <summary>
        /// Get photo by Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("{Id:int}")]
        public async Task<IActionResult> Get(int Id)
        {
            var location = GetControllerActionNames();

            try
            {
                var photo = await _photosService.FindByIdAsync(Id);
                if (photo is null)
                {
                    return NotFound();
                }

                return Ok(photo);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return InternalError($"{location}: {ex.Message} - {ex.InnerException}");
            }
        }

        /// <summary>
        /// Devolve todas as fotos (grid)
        /// </summary>
        /// <returns></returns>
        [HttpGet("AllPhotosVM")]
        public async Task<IActionResult> AllPhotosVM()
        {
            try
            {
                var listOfPhotos = await _photosService.GetAllPhotosVMAsync();
                if (listOfPhotos is null)
                { return NotFound(); }

                return Ok(listOfPhotos);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das fotos não encontrados");
            }
        }

        /// <summary>
        /// Devolve foto por Id
        /// </summary>
        /// <param name="Id"></param>
        /// <returns></returns>
        [HttpGet("PhotoVMById/{Id:int}")]
        public async Task<IActionResult> GetPhotoVMById(int Id)
        {
            try
            {
                var photoVM = await _photosService.GetPhotoVMAsync(Id);

                if (photoVM is null)
                { return NotFound(); }

                return Ok(photoVM);

            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, ex);
                return BadRequest("Dados das fotos não encontrados");
            }
        }

        /// <summary>
        /// Validate photo
        /// </summary>
        /// <param name="photo"></param>
        /// <returns></returns>
        [HttpPost("ValidatePhoto")]
        public IActionResult ValidatePhoto([FromBody] GaleriaFotosDto photo)
        {
            // Create validator instance (or inject it)
            var photosValidator = new GaleriaFotosValidator();

            // Call Validate or ValidateAsync and pass the object which needs to be validated
            var result = photosValidator.Validate(photo);

            if (result.IsValid)
            {
                return Ok(photosValidator);
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
