using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Text.RegularExpressions;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly IStudyService _studyService;
        private readonly IStudyTypeService _studyTypeService;
        private readonly IPatientService _patientService;
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly ILaboratoryDetailService _laboratoryDetailService;
        private readonly IMapper _mapper;

        public StudyController(
            IFileService fileService,
            IPatientService patientService,
            IStudyService studyService,
            IStudyTypeService studyTypeService,
            IEmailService emailService,
            IMapper mapper,
            ILaboratoryDetailService laboratoryDetailService)
        {
            _fileService = fileService;
            _patientService = patientService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _emailService = emailService;
            _mapper = mapper;
            _laboratoryDetailService = laboratoryDetailService;
        }

        [HttpGet("byPatient/{userId}")]
        public async Task<ActionResult<IEnumerable<StudyResponse>>> GetStudiesByPatient([FromRoute] int userId)
        {
            var studies = await _studyService.GetStudiesByUserId(userId);
            return Ok(_mapper.Map<IEnumerable<StudyResponse>>(studies));
        }

        [HttpGet("getUrl/{userId}")]
        public async Task<ActionResult<string>> GetByPatient([FromRoute] int userId, string fileName)
        {
            var user = await _patientService.GetPatientByUserIdAsync(userId);
            var studyUrl = _fileService.GetUrl(user.User.UserName, fileName);
            return Ok(studyUrl);
        }

        [HttpGet("laboratories/{userId}")]
        public async Task<ActionResult<IEnumerable<LaboratoryDetailResponse>>> GetLaboratoriesByPatient([FromRoute] int userId)
        {
            var laboratoriesDetail = await _laboratoryDetailService.GetLaboratoriesDetailsByUserIdAsync(userId);
            if (!laboratoriesDetail.Any())
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<LaboratoryDetailResponse>>(laboratoriesDetail));
        }

        [HttpPost("upload-study")]
        public async Task<IActionResult> UploadStudy([FromForm] StudyRequest study)
        {
            if (study.StudyFile == null || study.StudyFile.Length <= 0)
            {
                return BadRequest("Es necesario el estudio.");
            }

            try
            {
                var patient = await _patientService.GetPatientByUserIdAsync(Convert.ToInt32(study.UserId));
                if (patient == null)
                {
                    return NotFound($"Paciente no encontrado.");
                }

                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(study.StudyTypeId);
                if (studyType == null)
                {
                    return NotFound($"Tipo de estudio no encontrado.");
                }

                string fileName = _studyService.GenerateFileName(patient, studyType, study.Date);

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    study.StudyFile.CopyTo(memoryStream);
                    var pdfResult = await _fileService.InsertStudyAsync(memoryStream, patient.User.UserName, fileName);
                    if (pdfResult != HttpStatusCode.OK)
                    {
                        return StatusCode((int)pdfResult, "Error al cargar el archivo PDF.");
                    }
                }

                DateTime date = study.Date.ToArgentinaTime();
                Study newStudy = new Study() 
                {
                    LocationS3 = fileName,
                    Date = date,
                    Note = study.Note,
                    PatientId = patient.Id,
                    StudyTypeId = study.StudyTypeId,
                };

                await _studyService.Add(newStudy);

                if (study.StudyTypeId == 1)
                {
                    var mergedLaboratoryDetails = new LaboratoryDetailRequest();
                    using (var memoryStream = new MemoryStream())
                    {
                        study.StudyFile.CopyTo(memoryStream);
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
                    mergedLaboratoryDetails.IdStudy = newStudy.Id;
                    await _laboratoryDetailService.Add(_mapper.Map<LaboratoryDetail>(mergedLaboratoryDetails));
                }
                //await _emailService.SendEmailForNewStudyAsync(patient.User.Email, $"{patient.User.FirstName} {patient.User.LastName}");

                return Ok("Se ha guardado correctamente el estudio.");
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
