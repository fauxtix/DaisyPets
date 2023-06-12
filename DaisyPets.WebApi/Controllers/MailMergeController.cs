using DaisyPets.Core.Application.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Syncfusion.DocIO;
using Syncfusion.DocIO.DLS;
using Syncfusion.DocIORenderer;
using Syncfusion.Pdf;

namespace DaisyPets.WebApi.Controllers
{
    /// <summary>
    /// Mail Merge
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    public class MailMergeController : ControllerBase
    {
        private readonly ILogger<MailMergeController> _logger;
        private readonly IWebHostEnvironment _environment;

        /// <summary>
        /// Mail merge controller
        /// </summary>
        /// <param name="logger"></param>
        /// <param name="environment"></param>
        public MailMergeController(ILogger<MailMergeController> logger, IWebHostEnvironment environment)
        {
            _logger = logger;
            _environment = environment;
        }

        /// <summary>
        /// Cria pdf a partir de template do word
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost("MailMergeDocument")]
        public IActionResult MailMergeDocument([FromBody] MailMergeModel model)
        {
            string Abreviatura_DocGerado = "Pet_";
            string PastaDestino = "Pdf";

            var location = GetControllerActionNames();
            try
            {
                string? extension = Path.GetExtension(model.WordDocument);
                string result = "";
                string sRestFilename = "";

                string templatePath = Path.Combine(_environment.ContentRootPath, "Resources", "Templates");
                if (!templatePath.EndsWith(@"\"))
                    templatePath += @"\";

                string sDir2Save = Path.Combine(_environment.ContentRootPath, "Reports", "Docs");

                if (!Directory.Exists(Path.Combine(sDir2Save, PastaDestino)))
                    Directory.CreateDirectory(Path.Combine(sDir2Save, PastaDestino));

                sDir2Save = Path.Combine(sDir2Save, PastaDestino);


                if (model.SaveFile)
                {
                    sRestFilename = "_" + model.PetId.ToString() + "_" + DateTime.Now.ToString("ddMMyyyyHHmm");
                    result = Path.Combine(sDir2Save, Abreviatura_DocGerado + sRestFilename);
                }
                string sOutputPDF = result + ".pdf";
                string sOutputWord = result;

                string sSourceDoc = Path.Combine(templatePath, model.WordDocument!);
                if (!System.IO.File.Exists(sSourceDoc))
                {
                    _logger.LogWarning("Ficheiro " + templatePath + model.WordDocument + " não foi encontrado.\r\n\r\nVerifique, p.f.", "Erro na abrtura de ficheiro");
                    return BadRequest("");
                }

                FileStream fileStreamPath = new FileStream(sSourceDoc, FileMode.Open, FileAccess.Read, FileShare.ReadWrite);
                WordDocument document = new WordDocument(fileStreamPath, FormatType.Dotx);
                document.MailMerge.Execute(model.MergeFields, model.ValuesFields);

                sOutputWord = sOutputWord.Replace(@"\\\", @"\").Replace(@"\\", @"\");
                sOutputWord += ".docx";
                using (FileStream outFileStreamPath = new FileStream(sOutputWord, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite))
                {
                    document.Save(outFileStreamPath, FormatType.Docx);
                    document.Close();
                    document.Dispose();
                }

                GeneratePDF_FromDocx(sOutputWord, sOutputPDF);

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return Ok(sOutputWord);
            }
            catch (Exception e)
            {
                return InternalError($"{location}: {e.Message} - {e.InnerException}");
            }
        }

        private bool GeneratePDF_FromDocx(string docWord, string docWordPDF)
        {
            try
            {
                FileStream docStream = new FileStream(docWord, FileMode.Open, FileAccess.Read);
                WordDocument wordDocument = new WordDocument(docStream, FormatType.Automatic);
                DocIORenderer render = new DocIORenderer();
                PdfDocument pdfDocument = render.ConvertToPDF(wordDocument);


                //Set page size~(não funciona...)
                pdfDocument.PageSettings.Size = PdfPageSize.A4;

                FileStream outFileStreamPath = new FileStream(docWordPDF, FileMode.CreateNew, FileAccess.Write, FileShare.ReadWrite);

                pdfDocument.Save(outFileStreamPath);

                //Process.Start(docWordPDF);
                pdfDocument.Close(true);
                wordDocument.Close();

                if (wordDocument != null)
                {
                    wordDocument.Dispose();
                }

                GC.Collect();
                GC.WaitForPendingFinalizers();
                GC.Collect();
                GC.WaitForPendingFinalizers();

                return true;
            }
            catch (Exception ex)
            {
                throw new ApplicationException(ex.Message);
            }
        }

        /// <summary>
        /// Database structure (pdf)
        /// </summary>
        /// <returns></returns>

        [HttpGet("DatabaseStructure")]
        public IActionResult GetDatabaseStructurePdf()
        {
            var pdfFile = Path.Combine(_environment.ContentRootPath, "Reports", "Docs", "Pdf", "DatabaseStructure.pdf" );
            if (!System.IO.File.Exists(pdfFile))
            {
                return NotFound();
            }
            else
                return Ok(pdfFile);
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
