using Healthcare.Api.Core.Entities;
using Healthcare.Api.Repository.Repositories;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;
using static iText.IO.Codec.TiffWriter;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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

        // PAGE 1 READY
        private Hemograma ParsePdfText(string text)
        {
            var laboratoryDetails = new Hemograma();
            var properties = typeof(Hemograma).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            foreach (string line in text.Split('\n'))
            {
                string cleanLine = line.Trim().ToLowerInvariant().Replace(".", "");
                foreach (var property in properties)
                {
                    var displayNameAttribute = (DisplayNameAttribute)property.GetCustomAttribute(typeof(DisplayNameAttribute));
                    string propertyNameToShow = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.Name;

                    if (cleanLine.Contains(propertyNameToShow.ToLowerInvariant()))
                    {
                        MatchCollection matches = Regex.Matches(cleanLine, @"m?\d+([,.]\d+)?");
                        
                        var numericValue = Convert.ChangeType(matches.First().Value, property.PropertyType, CultureInfo.InvariantCulture);
                        property.SetValue(laboratoryDetails, numericValue);
                    }
                }

            }

            return laboratoryDetails;
        }

        //public static IEnumerable<int> GetNumbersOfLine(string line)
        //{
        //}
    }
}
