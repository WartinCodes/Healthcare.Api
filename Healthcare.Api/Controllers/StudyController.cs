using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Entities.DTO;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.Utilities;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyController : ControllerBase
    {
        private readonly IStudyService _studyService;
        private readonly IStudyTypeService _studyTypeService;
        private readonly IPatientService _patientService;
        private readonly IDoctorService _doctorService;
        private readonly IFileService _fileService;
        private readonly IPdfFileService _pdfFileService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly IUltrasoundImageService _ultrasoundImageService;
        private readonly IMapper _mapper;
        private readonly IBloodTestService _bloodTestService;
        private readonly IBloodTestDataService _bloodTestDataService;
        private readonly UserManager<User> _userManager;
        private List<BloodTestData> _addedBloodTestData = new List<BloodTestData>();
        private readonly string _studiesFolder = "studies";

        public StudyController(
            IFileService fileService,
            IPdfFileService pdfFileService,
            IPatientService patientService,
            IDoctorService doctorService,
            IStudyService studyService,
            IJwtService jwtService,
            IStudyTypeService studyTypeService,
            IEmailService emailService,
            IMapper mapper,
            IUltrasoundImageService ultrasoundImageService,
            IBloodTestService bloodTestService,
            IBloodTestDataService bloodTestDataService,
            UserManager<User> userManager)
        {
            _fileService = fileService;
            _pdfFileService = pdfFileService;
            _patientService = patientService;
            _doctorService = doctorService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _jwtService = jwtService;
            _emailService = emailService;
            _mapper = mapper;
            _ultrasoundImageService = ultrasoundImageService;
            _bloodTestService = bloodTestService;
            _bloodTestDataService = bloodTestDataService;
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

        [HttpGet("byUserWithUrls/{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}, {RoleEnum.Paciente}")]
        public async Task<ActionResult<IEnumerable<StudyResponse>>> GetStudiesWithUrl([FromRoute] int userId)
        {
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

            IEnumerable<Study> studiesEntity = await _studyService.GetStudiesByUserId(userId);
            var studiesResponse = _mapper.Map<IEnumerable<StudyResponse>>(studiesEntity);
            foreach (var study in studiesResponse)
            {
                study.SignedUrl = _fileService.GetSignedUrl(_studiesFolder, currentUser.UserName, study.LocationS3) ?? String.Empty;
                study.UltrasoundImages = _mapper.Map<List<UltrasoundImageResponse>>(await _ultrasoundImageService.GetUltrasoundImagesByStudyIdAsync(study.Id));
            }

            return Ok(studiesResponse);
        }


        [HttpGet("getUrl/{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}, {RoleEnum.Paciente}")]
        public async Task<ActionResult<string>> GetUrlByUserId([FromRoute] int userId, string fileName)
        {
            var user = await _userManager.GetUserById(userId);
            if (user == null) return NoContent();

            var studyUrl = _fileService.GetSignedUrl(_studiesFolder, user.UserName, fileName);
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
        //[Authorize(Roles = $"{RoleEnum.Secretaria}")]
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
                var patientUser = await _userManager.GetUserById(Convert.ToInt32(study.UserId));
                if (patientUser == null)
                {
                    return NotFound("Paciente no encontrado.");
                }
                Doctor? doctorUser = await _doctorService.GetDoctorByUserIdAsync(Convert.ToInt32(study.DoctorUserId));

                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(study.StudyTypeId);
                if (studyType == null)
                {
                    return NotFound("Tipo de estudio no encontrado.");
                }

                var pdfFile = study.StudyFiles.SingleOrDefault(f => f.FileName.Contains(".pdf", StringComparison.InvariantCultureIgnoreCase));
                string pdfFileName = _studyService.GenerateFileName(new FileNameParameters(patientUser, studyType, study.Date.ToShortDateString(), null, Path.GetExtension(pdfFile.FileName)));

                Study newStudy = new Study()
                {
                    LocationS3 = pdfFileName,
                    Date = study.Date,
                    Created = DateTime.UtcNow.ToArgentinaTime(),
                    Note = study.Note,
                    UserId = patientUser.Id,
                    StudyTypeId = study.StudyTypeId,
                    SignedDoctorId = doctorUser?.Id
                };

                var insertedStudy = await _studyService.Add(newStudy);
                _mapper.Map(newStudy, studyResponse);
                byte[] studyBytes;
                using (var ms = new MemoryStream())
                {
                    await pdfFile.CopyToAsync(ms);
                    studyBytes = ms.ToArray();
                }

                await _pdfFileService.SavePdfAsync(studyBytes, patientUser.UserName, pdfFileName);
                studyResponse.SignedUrl = _fileService.GetSignedUrl(_studiesFolder, patientUser.UserName, pdfFileName);

                switch (study.StudyTypeId)
                {
                    case (int)StudyTypeEnum.Laboratorio:
                        IEnumerable<BloodTest> properties = await _bloodTestService.GetBloodTestsAsync();
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
                                        var page = pdfDocument.GetPage(i);
                                        string text = PdfTextExtractor.GetTextFromPage(page);

                                        properties = properties.Where(p => !_addedBloodTestData
                                            .Any(b => b.IdBloodTest == p.Id))
                                            .ToList();

                                        List<BloodTestData> pageBloodTestData = _pdfFileService.ParsePdfText(text, insertedStudy.Id, properties);
                                        await _bloodTestDataService.AddRangeAsync(pageBloodTestData);
                                        _addedBloodTestData.AddRange(pageBloodTestData);
                                    }
                                }
                            }
                        }
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
                            var imageName = _studyService.GenerateFileName(new FileNameParameters(patientUser, studyType, study.Date.ToShortDateString(), index, Path.GetExtension(imageFile.FileName)));
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
                                var imageResult = await _fileService.InsertFileStudyAsync(memoryStream, patientUser.UserName, imageName);
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

                }

                await _emailService.SendEmailForNewStudyAsync(patientUser.Email, $"{patientUser.FirstName} {patientUser.LastName}", study.Date);

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
