using Healthcare.Api.Core.Entities;
using Healthcare.Api.Repository.Repositories;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Reflection;
using System.Text.RegularExpressions;
using static iText.IO.Codec.TiffWriter;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryController : ControllerBase
    {

        [HttpPost("upload-pdf")]
        public IActionResult UploadPdf([FromForm] IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("File is required");
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    file.CopyTo(memoryStream);
                    memoryStream.Position = 0;

                    using (var pdfReader = new PdfReader(memoryStream))
                    {
                        using (var pdfDocument = new PdfDocument(pdfReader))
                        {
                            for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                            {
                                var page = pdfDocument.GetPage(i);
                                string text = PdfTextExtractor.GetTextFromPage(page);

                                // Aquí necesitas implementar la lógica para analizar el texto
                                // extraído del PDF y convertirlo en un objeto LaboratoryDetails
                                // que puedas guardar en el repositorio.

                                var laboratoryDetails = ParsePdfText(text);
                                //_repository.Save(laboratoryDetails);
                            }

                            return Ok("Laboratory details saved successfully");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred: {ex.Message}");
            }
        }

        private LaboratoryDetail ParsePdfText(string text)
        {
            var laboratoryDetails = new LaboratoryDetail();

            foreach (var line in text.Split('\n'))
            {

            }

            return laboratoryDetails;
        }
    }
}
