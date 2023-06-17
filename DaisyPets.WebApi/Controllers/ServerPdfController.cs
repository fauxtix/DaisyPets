using Microsoft.AspNetCore.Mvc;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Server pdf manager
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class ServerPdfController : ControllerBase
    {

        private readonly IWebHostEnvironment _webHostEnvironment;

        /// <summary>
        /// Construtor
        /// </summary>
        /// <param name="webHostEnvironment"></param>
        public ServerPdfController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        /// <summary>
        /// download pdf
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet("Download/{folder}/{filename}"), DisableRequestSizeLimit]
        public IActionResult Download(string folder, string filename)
        {
            var fileLocation = Path.Combine(_webHostEnvironment.ContentRootPath, "reports", "Docs", folder, filename);

            var stream = new FileStream(fileLocation, FileMode.Open);
            return File(stream, "application/pdf", filename);

        }

        /// <summary>
        /// GetPdf from server
        /// </summary>
        /// <param name="folder"></param>
        /// <param name="filename"></param>
        /// <returns></returns>
        [HttpGet("GetServerPdfName/{folder}/{filename}")]
        public string GetFileName(string folder, string filename)
        {
            var fileLocation = Path.Combine(_webHostEnvironment.ContentRootPath, "reports", "docs", folder, filename);

            return fileLocation;
        }

    }
}
