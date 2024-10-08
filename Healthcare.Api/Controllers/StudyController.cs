using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.Utilities;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel;
using System.Globalization;
using System.Net;
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
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly ILaboratoryDetailService _laboratoryDetailService;
        private readonly IUltrasoundImageService _ultrasoundImageService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public StudyController(
            IFileService fileService,
            IPatientService patientService,
            IStudyService studyService,
            IJwtService jwtService,
            IStudyTypeService studyTypeService,
            IEmailService emailService,
            IMapper mapper,
            ILaboratoryDetailService laboratoryDetailService,
            IUltrasoundImageService ultrasoundImageService,
            UserManager<User> userManager)
        {
            _fileService = fileService;
            _patientService = patientService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _jwtService = jwtService;
            _emailService = emailService;
            _mapper = mapper;
            _laboratoryDetailService = laboratoryDetailService;
            _ultrasoundImageService = ultrasoundImageService;
            _userManager = userManager;
        }

        [HttpGet("byUser/{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}, {RoleEnum.Paciente}")]
        public async Task<ActionResult<IEnumerable<StudyResponse>>> GetStudiesByUserId([FromRoute] int userId)
        {
            var studiesEntity = await _studyService.GetStudiesByUserId(userId);
            var studiesResponse = _mapper.Map<IEnumerable<StudyResponse>>(studiesEntity);
            foreach (var study in studiesResponse)
            {
                study.UltrasoundImages = _mapper.Map<List<UltrasoundImageResponse>>(await _ultrasoundImageService.GetUltrasoundImagesByStudyIdAsync(study.Id));
            }

            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (!int.TryParse(currentUserId, out int parsedUserId))
            {
                return Unauthorized("Usuario no autorizado.");
            }

            var currentUser = await _userManager.FindByIdAsync(parsedUserId.ToString());

            if (currentUser == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            bool isValid = await _jwtService.ValidatePatientToken(currentUser);

            if (!isValid && parsedUserId != userId)
            {
                return Forbid("No tiene permiso para acceder a los datos de este paciente.");
            }

            return Ok(studiesResponse);
        }

        [HttpGet("getUrl/{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}, {RoleEnum.Paciente}")]
        public async Task<ActionResult<string>> GetUrlByUserId([FromRoute] int userId, string fileName)
        {
            var user = await _userManager.GetUserById(userId);
            if (user == null) return NoContent();

            var studyUrl = _fileService.GetUrl(user.UserName, fileName);

            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (!int.TryParse(currentUserId, out int parsedUserId))
            {
                return Unauthorized("Usuario no autorizado.");
            }

            var currentUser = await _userManager.FindByIdAsync(parsedUserId.ToString());

            if (currentUser == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            bool isValid = await _jwtService.ValidatePatientToken(currentUser);

            if (!isValid && parsedUserId != userId)
            {
                return Forbid("No tiene permiso para acceder a los datos de este paciente.");
            }

            return Ok(studyUrl);
        }

        [HttpGet("laboratories/byUser/{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria},{RoleEnum.Paciente}")]
        public async Task<ActionResult<IEnumerable<LaboratoryDetailResponse>>> GetLaboratoriesByUser([FromRoute] int userId)
        {
            var laboratoriesDetail = await _laboratoryDetailService.GetLaboratoriesDetailsByUserIdAsync(userId);
            if (!laboratoriesDetail.Any())
            {
                return NoContent();
            }

            var currentUserId = User.Claims.FirstOrDefault(x => x.Type == "Id")?.Value;

            if (!int.TryParse(currentUserId, out int parsedUserId))
            {
                return Unauthorized("Usuario no autorizado.");
            }

            var currentUser = await _userManager.FindByIdAsync(parsedUserId.ToString());

            if (currentUser == null)
            {
                return Unauthorized("Usuario no encontrado.");
            }

            bool isValid = await _jwtService.ValidatePatientToken(currentUser);

            if (!isValid && parsedUserId != userId)
            {
                return Forbid("No tiene permiso para acceder a los datos de este paciente.");
            }

            return Ok(_mapper.Map<IEnumerable<LaboratoryDetailResponse>>(laboratoriesDetail));
        }

        [HttpGet("laboratoryDetails/byStudies")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<IEnumerable<LaboratoryDetail>>> GetLaboratoryDetails([FromQuery] int[] studiesId)
        {
            var laboratoriesDetails = await _laboratoryDetailService.GetLaboratoriesDetailsByStudiesIds(studiesId);
            return Ok(laboratoriesDetails);
        }

        [HttpGet("ultrasoundImages/byStudy/{studyId}")]
        public async Task<ActionResult<IEnumerable<UltrasoundImageResponse>>> GetUltrasoundImages([FromRoute] int studyId)
        {
            var ultrasoundImages = await _ultrasoundImageService.GetUltrasoundImagesByStudyIdAsync(studyId);
            if (!ultrasoundImages.Any())
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<UltrasoundImageResponse>>(ultrasoundImages));
        }

        [HttpGet("all")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<int>> GetStudies([FromQuery] int? studyTypeId)
        {
            var studies = await _studyService.GetAsync();
            if (studyTypeId.HasValue)
            {
                var validStudyTypeId = await _studyTypeService.GetStudyTypeByIdAsync(studyTypeId.Value);
                if (validStudyTypeId == null)
                {
                    return BadRequest();
                }
                studies = studies.Where(x => x.StudyTypeId == studyTypeId.Value);
            }

            var countStudies = studies.Count();
            return Ok(countStudies);
        }

        [HttpGet("lastStudies")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<int>> GetLastStudies([FromQuery] int? studyTypeId)
        {
            var oneWeekAgo = DateTime.UtcNow.AddDays(-7);
            var studies = (await _studyService.GetAsync())
                .Where(x => x.Date >= oneWeekAgo)
                .ToList();
            if (studyTypeId.HasValue)
            {
                var validStudyTypeId = await _studyTypeService.GetStudyTypeByIdAsync(studyTypeId.Value);
                if (validStudyTypeId == null)
                {
                    return BadRequest();
                }
                studies = studies.Where(x => x.StudyTypeId == studyTypeId.Value).ToList();
            }

            var countStudies = studies.Count();
            return Ok(countStudies);
        }

        [HttpPost("upload-study")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> UploadStudy([FromForm] StudyRequest study)
        {
            if (study.StudyFiles == null || study.StudyFiles.Count == 0)
            {
                return BadRequest("Es necesario el estudio.");
            }
            if (study.StudyTypeId == (int)StudyTypeEnum.Laboratorio && study.StudyFiles.Count > 1)
            {
                return BadRequest("Solo se permite un archivo para este tipo de estudio.");
            }

            try
            {
                StudyResponse studyResponse = new StudyResponse();

                var user = await _userManager.GetUserById(Convert.ToInt32(study.UserId));
                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(study.StudyTypeId);
                if (studyType == null)
                {
                    return NotFound("Tipo de estudio no encontrado.");
                }

                var pdfFile = study.StudyFiles.SingleOrDefault(f => f.FileName.Contains(".pdf", StringComparison.InvariantCultureIgnoreCase));
                string pdfFileName = _studyService.GenerateFileName(new FileNameParameters(user, studyType, study.Date.ToShortDateString(), null, Path.GetExtension(pdfFile.FileName)));

                using (MemoryStream memoryStream = new MemoryStream())
                {
                    pdfFile.CopyTo(memoryStream);
                    var pdfResult = await _fileService.InsertFileStudyAsync(memoryStream, user.UserName, pdfFileName);
                    if (pdfResult != HttpStatusCode.OK)
                    {
                        return StatusCode((int)pdfResult, "Error al cargar el archivo PDF.");
                    }
                }

                Study newStudy = new Study()
                {
                    LocationS3 = pdfFileName,
                    Date = study.Date,
                    Note = study.Note,
                    UserId = user.Id,
                    StudyTypeId = study.StudyTypeId,
                };

                await _studyService.Add(newStudy);
                _mapper.Map(newStudy, studyResponse);

                switch (study.StudyTypeId)
                {
                    case (int)StudyTypeEnum.Laboratorio:
                        var mergedLaboratoryDetails = new LaboratoryDetailRequest();
                        using (var memoryStream = new MemoryStream())
                        {
                            pdfFile.CopyTo(memoryStream);
                            memoryStream.Position = 0;
                            using (var pdfReader = new PdfReader(memoryStream))
                            {
                                using (var pdfDocument = new PdfDocument(pdfReader))
                                {
                                    for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                                    {
                                        var properties = typeof(LaboratoryDetailRequest)
                                            .GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase)
                                            .Where(property => property.GetValue(mergedLaboratoryDetails) == null)
                                            .ToList();

                                        var page = pdfDocument.GetPage(i);
                                        string text = PdfTextExtractor.GetTextFromPage(page);

                                        var pageLaboratoryDetails = ParsePdfText(text, properties);
                                        MergeLaboratoryDetails(mergedLaboratoryDetails, pageLaboratoryDetails);
                                    }
                                }
                            }
                        }
                        mergedLaboratoryDetails.IdStudy = newStudy.Id;
                        await _laboratoryDetailService.Add(_mapper.Map<LaboratoryDetail>(mergedLaboratoryDetails));
                        break;

                    case (int)StudyTypeEnum.Ecografia:
                        var imageFiles = study.StudyFiles.Where(f =>
                            f.FileName.Contains(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                            f.FileName.Contains(".png", StringComparison.InvariantCultureIgnoreCase) ||
                            f.FileName.Contains(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                        ).ToList();

                        int index = 1;

                        List<UltrasoundImageResponse> ultrasoundImageResponse = new List<UltrasoundImageResponse>();
                        foreach (var imageFile in imageFiles) 
                        {
                            var imageName = _studyService.GenerateFileName(new FileNameParameters(user, studyType, study.Date.ToShortDateString(), index, Path.GetExtension(imageFile.FileName)));
                            UltrasoundImage newUltrasoundImage = new UltrasoundImage()
                            {
                                IdStudy = newStudy.Id,
                                LocationS3 = imageName
                            };
                            await _ultrasoundImageService.Add(newUltrasoundImage);
                            ultrasoundImageResponse.Add(_mapper.Map<UltrasoundImageResponse>(newUltrasoundImage));
                            using (var memoryStream = new MemoryStream())
                            {
                                imageFile.CopyTo(memoryStream);
                                var imageResult = await _fileService.InsertFileStudyAsync(memoryStream, user.UserName, imageName);
                                if (imageResult != HttpStatusCode.OK)
                                {
                                    return StatusCode((int)imageResult, "Error al cargar las imagenes.");
                                }
                            }

                            index++;
                        }
                        studyResponse.UltrasoundImages = ultrasoundImageResponse;
                        studyResponse.StudyType = _mapper.Map<StudyTypeResponse>(studyType);
                        break;

                    default:
                        return NotFound("Tipo de estudio no encontrado.");
                }

                await _emailService.SendEmailForNewStudyAsync(user.Email, $"{user.FirstName} {user.LastName}", study.Date);

                return Ok(studyResponse);
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }

        [HttpPut("update/ultrasoundImages")]
        public async Task<IActionResult> UpdateStudyWithUltrasoundImage([FromForm] PutUltrasoundImageRequest study)
        {
            try
            {
                StudyResponse studyResponse = new StudyResponse();

                var getStudy = await _studyService.GetStudyByIdAsync(study.StudyId);
                if (getStudy == null) 
                {
                    return NotFound("Estudio no encontrado.");
                }

                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(getStudy.StudyTypeId);
                if (getStudy.StudyTypeId != (int)StudyTypeEnum.Ecografia)
                {
                    return NotFound("Tipo de estudio inválido para cargar ecografías.");
                }

                _mapper.Map(getStudy, studyResponse);

                var imageFiles = study.StudyFiles.Where(f =>
                    f.FileName.Contains(".jpg", StringComparison.InvariantCultureIgnoreCase) ||
                    f.FileName.Contains(".png", StringComparison.InvariantCultureIgnoreCase) ||
                    f.FileName.Contains(".jpeg", StringComparison.InvariantCultureIgnoreCase)
                ).ToList();

                int index = 1;
                List<UltrasoundImageResponse> ultrasoundImageResponse = new List<UltrasoundImageResponse>();
                foreach (var imageFile in imageFiles)
                {
                    var imageName = _studyService.GenerateFileName(new FileNameParameters(getStudy.User, studyType, getStudy.Date.ToShortDateString(), index, Path.GetExtension(imageFile.FileName)));
                    UltrasoundImage newUltrasoundImage = new UltrasoundImage()
                    {
                        IdStudy = study.StudyId,
                        LocationS3 = imageName
                    };
                    await _ultrasoundImageService.Add(newUltrasoundImage);
                    ultrasoundImageResponse.Add(_mapper.Map<UltrasoundImageResponse>(newUltrasoundImage));
                    using (var memoryStream = new MemoryStream())
                    {
                        imageFile.CopyTo(memoryStream);
                        var imageResult = await _fileService.InsertFileStudyAsync(memoryStream, getStudy.User.UserName, imageName);
                        if (imageResult != HttpStatusCode.OK)
                        {
                            return StatusCode((int)imageResult, "Error al cargar las imagenes.");
                        }
                    }

                    index++;
                }
                studyResponse.UltrasoundImages = ultrasoundImageResponse;
                return Ok(studyResponse);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        private void MergeLaboratoryDetails(LaboratoryDetailRequest mergedDetails, LaboratoryDetailRequest pageDetails)
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
            mergedDetails.Creatinfosfoquinasa = MergeProperty(mergedDetails.Creatinfosfoquinasa, pageDetails.Creatinfosfoquinasa);
            mergedDetails.ColesterolTotal = MergeProperty(mergedDetails.ColesterolTotal, pageDetails.ColesterolTotal);
            mergedDetails.ColesterolHdl = MergeProperty(mergedDetails.ColesterolHdl, pageDetails.ColesterolHdl);
            mergedDetails.ColesterolLdl = MergeProperty(mergedDetails.ColesterolLdl, pageDetails.ColesterolLdl);
            mergedDetails.Trigliceridos = MergeProperty(mergedDetails.Trigliceridos, pageDetails.Trigliceridos);
            mergedDetails.Uricemia = MergeProperty(mergedDetails.Uricemia, pageDetails.Uricemia);
            mergedDetails.BilirrubinaDirecta = MergeProperty(mergedDetails.BilirrubinaDirecta, pageDetails.BilirrubinaDirecta);
            mergedDetails.BilirrubinaIndirecta = MergeProperty(mergedDetails.BilirrubinaIndirecta, pageDetails.BilirrubinaIndirecta);
            mergedDetails.BilirrubinaTotal = MergeProperty(mergedDetails.BilirrubinaTotal, pageDetails.BilirrubinaTotal);
            mergedDetails.Amilasemia = MergeProperty(mergedDetails.Amilasemia, pageDetails.Amilasemia);
            mergedDetails.TransaminasaGlutamicoOxalac = MergeProperty(mergedDetails.TransaminasaGlutamicoOxalac, pageDetails.TransaminasaGlutamicoOxalac);
            mergedDetails.TransaminasaGlutamicoPiruvic = MergeProperty(mergedDetails.TransaminasaGlutamicoPiruvic, pageDetails.TransaminasaGlutamicoPiruvic);
            mergedDetails.FosfatasaAlcalina = MergeProperty(mergedDetails.FosfatasaAlcalina, pageDetails.FosfatasaAlcalina);
            mergedDetails.TirotrofinaPlamatica = MergeProperty(mergedDetails.TirotrofinaPlamatica, pageDetails.TirotrofinaPlamatica);
            mergedDetails.Sodio = MergeProperty(mergedDetails.Sodio, pageDetails.Sodio);
            mergedDetails.Potasio = MergeProperty(mergedDetails.Potasio, pageDetails.Potasio);
            mergedDetails.CloroPlasmatico = MergeProperty(mergedDetails.CloroPlasmatico, pageDetails.CloroPlasmatico);
            mergedDetails.CalcemiaTotal = MergeProperty(mergedDetails.CalcemiaTotal, pageDetails.CalcemiaTotal);
            mergedDetails.MagnesioSangre = MergeProperty(mergedDetails.MagnesioSangre, pageDetails.MagnesioSangre);
            mergedDetails.ProteinasTotales = MergeProperty(mergedDetails.ProteinasTotales, pageDetails.ProteinasTotales);
            mergedDetails.Albumina = MergeProperty(mergedDetails.Albumina, pageDetails.Albumina);
            mergedDetails.Pseudocolinesterasa = MergeProperty(mergedDetails.Pseudocolinesterasa, pageDetails.Pseudocolinesterasa);
            mergedDetails.Ferremia = MergeProperty(mergedDetails.Ferremia, pageDetails.Ferremia);
            mergedDetails.Transferrina = MergeProperty(mergedDetails.Transferrina, pageDetails.Transferrina);
            mergedDetails.SaturacionTransferrina = MergeProperty(mergedDetails.SaturacionTransferrina, pageDetails.SaturacionTransferrina);
            mergedDetails.Ferritina = MergeProperty(mergedDetails.Ferritina, pageDetails.Ferritina);
            mergedDetails.TiroxinaEfectiva = MergeProperty(mergedDetails.TiroxinaEfectiva, pageDetails.TiroxinaEfectiva);
            mergedDetails.TiroxinaTotal = MergeProperty(mergedDetails.TiroxinaTotal, pageDetails.TiroxinaTotal);
            mergedDetails.HemoglobinaGlicosilada = MergeProperty(mergedDetails.HemoglobinaGlicosilada, pageDetails.HemoglobinaGlicosilada);
            mergedDetails.GlutamilTranspeptidasa = MergeProperty(mergedDetails.GlutamilTranspeptidasa, pageDetails.GlutamilTranspeptidasa);
            mergedDetails.TiempoCoagulacion = MergeProperty(mergedDetails.TiempoCoagulacion, pageDetails.TiempoCoagulacion);
            mergedDetails.TiempoProtrombina = MergeProperty(mergedDetails.TiempoProtrombina, pageDetails.TiempoProtrombina);
            mergedDetails.TiempoSangria = MergeProperty(mergedDetails.TiempoSangria, pageDetails.TiempoSangria);
            mergedDetails.TiempoTromboplastina = MergeProperty(mergedDetails.TiempoTromboplastina, pageDetails.TiempoTromboplastina);
            mergedDetails.AntigenoProstaticoEspecifico = MergeProperty(mergedDetails.AntigenoProstaticoEspecifico, pageDetails.AntigenoProstaticoEspecifico);
            mergedDetails.PsaLibre = MergeProperty(mergedDetails.PsaLibre, pageDetails.PsaLibre);
            mergedDetails.RelacionPsaLibre = MergeProperty(mergedDetails.RelacionPsaLibre, pageDetails.RelacionPsaLibre);
            mergedDetails.VitaminaD3 = MergeProperty(mergedDetails.VitaminaD3, pageDetails.VitaminaD3);
            mergedDetails.CocienteAlbumina = MergeProperty(mergedDetails.CocienteAlbumina, pageDetails.CocienteAlbumina);
            mergedDetails.Nucleotidasa = MergeProperty(mergedDetails.Nucleotidasa, pageDetails.Nucleotidasa);
        }

        private string MergeProperty(string existingValue, string newValue)
        {
            if (existingValue == null)
                return newValue;

            if (newValue == null)
                return existingValue;

            return existingValue + newValue;
        }

        private static LaboratoryDetailRequest ParsePdfText(string text, List<PropertyInfo> properties)
        {
            var laboratoryDetail = new LaboratoryDetailRequest();

            string[] lines = text.Split('\n').Skip(1).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                string cleanLine = lines[i].Trim();

                foreach (var property in properties.Where(property => property.GetValue(laboratoryDetail) == null))
                {
                    var displayNameAttribute = (DisplayNameAttribute)property.GetCustomAttribute(typeof(DisplayNameAttribute));
                    string propertyNameToShow = displayNameAttribute != null ? displayNameAttribute.DisplayName : property.Name;

                    if (cleanLine.Contains(propertyNameToShow, StringComparison.InvariantCultureIgnoreCase))
                    {
                        Match timeMatch = Regex.Match(cleanLine, @"\b\d{1,2}:\d{2}\b");

                        if (timeMatch.Success)
                        {
                            var timeValue = Convert.ChangeType(timeMatch.Value, property.PropertyType, CultureInfo.InvariantCulture);
                            if (property.GetValue(laboratoryDetail) == null)
                            {
                                property.SetValue(laboratoryDetail, timeValue);
                            }
                        }
                        else
                        {
                            cleanLine = Regex.Replace(cleanLine, @"^\d+\s*-\s*", "").Trim();
                            cleanLine = Regex.Replace(cleanLine, @"\s*[-(]\s*\d+.*$", "").Trim();
                            MatchCollection matches = Regex.Matches(cleanLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");

                            if (matches.Count > 0)
                            {
                                string formattedNumber = string.Join(".", matches.Cast<Match>().Select(m => m.Value));
                                if (cleanLine.Contains("eritrosedimentacion", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    formattedNumber = matches.Last().Value.Split(',').Last();
                                }

                                if (property.GetValue(laboratoryDetail) == null)
                                {
                                    property.SetValue(laboratoryDetail, formattedNumber);
                                }
                            }
                            else if (i + 2 < lines.Length)
                            {
                                cleanLine = lines[i + 2].Trim();
                                matches = Regex.Matches(cleanLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");
                                if (matches.Count == 0 && i + 3 < lines.Length)
                                {
                                    cleanLine = lines[i + 3].Trim();
                                    matches = Regex.Matches(cleanLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");
                                }
                                if (matches.Count > 0)
                                {
                                    string formattedNumber = string.Join(",", matches.Cast<Match>().Select(m => m.Value));
                                    if (property.GetValue(laboratoryDetail) == null)
                                    {
                                        property.SetValue(laboratoryDetail, formattedNumber);
                                    }
                                }
                            }
                        }

                        break;
                    }
                }
            }
            return laboratoryDetail;
        }


        [HttpDelete("{id}")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var study = await _studyService.GetStudyByIdAsync(id);
                if (study == null)
                {
                    return NotFound("Estudio no encontrado.");
                }

                _studyService.Remove(study);
                return Ok("Estudio eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}
