using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Repository.Repositories;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.DataProtection.XmlEncryption;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryController : ControllerBase
    {
        private readonly ILaboratoryDetailService _laboratoryDetailService;
        private readonly IMapper _mapper;

        public LaboratoryController(IMapper mapper, ILaboratoryDetailService laboratoryDetailService)
        {
            _mapper = mapper;
            _laboratoryDetailService = laboratoryDetailService;
        }

        [HttpPost("upload-pdf")]
        public IActionResult UploadPdf([FromForm] IFormFile file)
        {
            if (file == null || file.Length <= 0)
            {
                return BadRequest("Es necesario el archivo.");
            }

            try
            {
                var mergedLaboratoryDetails = new LaboratoryDetailRequest();
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

                                var pageLaboratoryDetails = ParsePdfText(text);
                                MergeLaboratoryDetails(mergedLaboratoryDetails, pageLaboratoryDetails);
                            }
                        }
                    }
                }
                
                _laboratoryDetailService.Add(_mapper.Map<LaboratoryDetail>(mergedLaboratoryDetails));
                return Ok("Se ha guardado correctamente los datos del laboratorio.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }

        void MergeLaboratoryDetails(LaboratoryDetailRequest mergedDetails, LaboratoryDetailRequest pageDetails)
        {
            mergedDetails.GlobulosRojos = MergeProperty(mergedDetails.GlobulosRojos, pageDetails.GlobulosRojos);
            mergedDetails.GlobulosBlancos = MergeProperty(mergedDetails.GlobulosBlancos, pageDetails.GlobulosBlancos);
            mergedDetails.Hemoglobina = MergeProperty(mergedDetails.Hemoglobina, pageDetails.Hemoglobina);
            mergedDetails.Hematocrito = MergeProperty(mergedDetails.Hematocrito, pageDetails.Hematocrito);
            mergedDetails.VCM = MergeProperty(mergedDetails.VCM, pageDetails.VCM);
            mergedDetails.HCM = MergeProperty(mergedDetails.HCM, pageDetails.HCM);
            mergedDetails.CHCM = MergeProperty(mergedDetails.CHCM, pageDetails.CHCM);
            mergedDetails.NeutrofilosCayados = MergeProperty(mergedDetails.NeutrofilosCayados, pageDetails.NeutrofilosCayados);
            mergedDetails.NeutrofilosSegmentados = MergeProperty(mergedDetails.NeutrofilosSegmentados, pageDetails.NeutrofilosSegmentados);
            mergedDetails.Eosinofilos = MergeProperty(mergedDetails.Eosinofilos, pageDetails.Eosinofilos);
            mergedDetails.Basofilos = MergeProperty(mergedDetails.Basofilos, pageDetails.Basofilos);
            mergedDetails.Linfocitos = MergeProperty(mergedDetails.Linfocitos, pageDetails.Linfocitos);
            mergedDetails.Monocitos = MergeProperty(mergedDetails.Monocitos, pageDetails.Monocitos);
            mergedDetails.Eritrosedimentacion1 = MergeProperty(mergedDetails.Eritrosedimentacion1, pageDetails.Eritrosedimentacion1);
            mergedDetails.Eritrosedimentacion2 = MergeProperty(mergedDetails.Eritrosedimentacion2, pageDetails.Eritrosedimentacion2);
            mergedDetails.Plaquetas = MergeProperty(mergedDetails.Plaquetas, pageDetails.Plaquetas);
            mergedDetails.Glucemia = MergeProperty(mergedDetails.Glucemia, pageDetails.Glucemia);
            mergedDetails.Uremia = MergeProperty(mergedDetails.Uremia, pageDetails.Uremia);
            mergedDetails.Creatininemia = MergeProperty(mergedDetails.Creatininemia, pageDetails.Creatininemia);
            mergedDetails.ColesterolTotal = MergeProperty(mergedDetails.ColesterolTotal, pageDetails.ColesterolTotal);
            mergedDetails.ColesterolHdl = MergeProperty(mergedDetails.ColesterolHdl, pageDetails.ColesterolHdl);
            mergedDetails.Trigliceridos = MergeProperty(mergedDetails.Trigliceridos, pageDetails.Trigliceridos);
            mergedDetails.Uricemia = MergeProperty(mergedDetails.Uricemia, pageDetails.Uricemia);
            mergedDetails.BilirrubinaDirecta = MergeProperty(mergedDetails.BilirrubinaDirecta, pageDetails.BilirrubinaDirecta);
            mergedDetails.BilirrubinaIndirecta = MergeProperty(mergedDetails.BilirrubinaIndirecta, pageDetails.BilirrubinaIndirecta);
            mergedDetails.BilirrubinaTotal = MergeProperty(mergedDetails.BilirrubinaTotal, pageDetails.BilirrubinaTotal);
            mergedDetails.TransaminasaGlutamicoOxalac = MergeProperty(mergedDetails.TransaminasaGlutamicoOxalac, pageDetails.TransaminasaGlutamicoOxalac);
            mergedDetails.TransaminasaGlutamicoPiruvic = MergeProperty(mergedDetails.TransaminasaGlutamicoPiruvic, pageDetails.TransaminasaGlutamicoPiruvic);
            mergedDetails.FosfatasaAlcalina = MergeProperty(mergedDetails.FosfatasaAlcalina, pageDetails.FosfatasaAlcalina);
            mergedDetails.TirotrofinaPlamatica = MergeProperty(mergedDetails.TirotrofinaPlamatica, pageDetails.TirotrofinaPlamatica);
        }

        string MergeProperty(string existingValue, string newValue)
        {
            if (existingValue == null)
                return newValue;

            if (newValue == null)
                return existingValue;

            return existingValue + newValue;
        }

        private static LaboratoryDetailRequest ParsePdfText(string text)
        {
            var laboratoryDetail = new LaboratoryDetailRequest();
            var properties = typeof(LaboratoryDetailRequest).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase);
            string[] lines = text.Split('\n');

            for (int i = 0; i < lines.Length; i++)
            {
                string cleanLine = lines[i].Trim().ToLowerInvariant().Replace(".", "");
                foreach (var property in properties)
                {
                    var displayNameAttribute = (DisplayNameAttribute)property.GetCustomAttribute(typeof(DisplayNameAttribute));
                    string propertyNameToShow = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.Name;

                    if (cleanLine.Contains(propertyNameToShow.ToLowerInvariant()))
                    {
                        MatchCollection matches = Regex.Matches(cleanLine, @"m?\d+([,.]\d+)?");
                        if (matches.Count > 0)
                        {
                            var numericValue = Convert.ChangeType(
                                cleanLine.Contains("eritrosedimentacion") ? matches.Last().Value : matches.First().Value,
                                property.PropertyType, CultureInfo.InvariantCulture);

                            if (property.GetValue(laboratoryDetail) == null)
                            {
                                property.SetValue(laboratoryDetail, numericValue);
                            }

                            break;
                        }
                        else if (i + 2 < lines.Length)
                        {
                            cleanLine = lines[i + 2].Trim().ToLowerInvariant().Replace(".", "");
                            matches = Regex.Matches(cleanLine, @"m?\d+([,.]\d+)?");
                            if (matches.Count > 0)
                            {
                                var numericValue = Convert.ChangeType(matches.First().Value, property.PropertyType, CultureInfo.InvariantCulture);
                                if (property.GetValue(laboratoryDetail) == null)
                                {
                                    property.SetValue(laboratoryDetail, numericValue);
                                }
                            }
                            break;
                        }
                    }
                }
            }
            return laboratoryDetail;
        }
    }
}
