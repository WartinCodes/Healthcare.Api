using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
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
        private readonly IFileService _fileService;
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public StudyController(
            IFileService fileService,
            IPatientService patientService,
            IStudyService studyService,
            IStudyTypeService studyTypeService,
            IEmailService emailService,
            IMapper mapper)
        {
            _fileService = fileService;
            _patientService = patientService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _emailService = emailService;
            _mapper = mapper;
        }

        [HttpGet("byPatient/{userId}")]
        public async Task<ActionResult<IEnumerable<StudyResponse>>> GetStudiesByPatient([FromRoute] int userId)
        {
            var studies = await _studyService.GetStudiesByUserId(userId);

            return Ok(_mapper.Map<IEnumerable<StudyResponse>>(studies));
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
                var fileDate = study.Date.ToArgentinaTime().ToString().Replace("/", "").Replace(":", "").Trim();
                string fileName = patient.User.LastName + patient.User.FirstName + studyType.Name + '-' + fileDate + ".pdf";
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
                await _emailService.SendEmailForNewStudyAsync(patient.User.Email, $"{patient.User.FirstName} {patient.User.LastName}");

                return Ok("Se ha guardado correctamente el estudio.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
