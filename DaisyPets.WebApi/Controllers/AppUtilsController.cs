using DaisyPets.Core.Application.Interfaces.Repositories;
using DaisyPets.Core.Application.Interfaces.Services;
using DaisyPets.WebApi.Helpers;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{

    /// <summary>
    /// Utils controller
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class AppUtilsController : ControllerBase
    {
        private  readonly IConfiguration _configuration;
        private readonly ILogger<AppUtilsController> _logger;
        private readonly IAppSettingsService _appSettingsService;

        /// <summary>
        /// Constuctor
        /// </summary>
        /// <param name="configuration"></param>
        public AppUtilsController(IConfiguration configuration, ILogger<AppUtilsController> logger, IAppSettingsService appSettingsService)
        {
            _configuration = configuration;
            _logger = logger;
            _appSettingsService = appSettingsService;
        }
        /// <summary>
        /// Backup Sqlite Db
        /// </summary>
        /// <returns></returns>
        [HttpGet("BackupSqlite")]
        public IActionResult SqliteBackup()
        {
            try
            {
                var _databaseName = _configuration?.GetConnectionString("PetsConnection") ?? "";

                UtilsService.BackupDatabase(_databaseName);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Get language selected
        /// </summary>
        /// <returns></returns>
        [HttpGet("Settings/Language")]
        public async Task<IActionResult> GetAppCulture()
        {
            try
            {
                var selectedCulture = await _appSettingsService.GetLanguage();
                return Ok(selectedCulture);

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

        /// <summary>
        /// Set app culture
        /// </summary>
        /// <param name="languageName"></param>
        /// <returns></returns>
        [HttpPost("Settings/Language")]
        public async Task<IActionResult> SetAppCulture([FromBody] string culture)
        {
            try
            {
                await _appSettingsService.SetLanguage(culture);
                return Ok();

            }
            catch (Exception)
            {
                return BadRequest();
            }
        }

    }
}
