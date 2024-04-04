using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
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
        private readonly IMapper _mapper;

        public StudyController(
            IFileService fileService,
            IPatientService patientService,
            IStudyService studyService,
            IStudyTypeService studyTypeService,
            IMapper mapper)
        {
            _fileService = fileService;
            _patientService = patientService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _mapper = mapper;
        }

        [HttpGet("byPatient/{id}")]
        public async Task<ActionResult<IEnumerable<StudyResponse>>> GetStudiesByPatient([FromRoute] int id)
        {
            var studies = await _studyService.GetStudiesByPatientId(id);

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
                var patient = await _patientService.GetPatientByIdAsync(study.PatientId);
                if (patient == null)
                {
                    return NotFound($"Paciente no encontrado.");
                }

                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(study.StudyTypeId);
                if (studyType == null)
                {
                    return NotFound($"Tipo de estudio no encontrado.");
                }

                string fileName = Guid.NewGuid().ToString() + ".pdf"; 
                using (MemoryStream memoryStream = new MemoryStream())
                {
                    study.StudyFile.CopyTo(memoryStream);
                    var pdfResult = await _fileService.InsertStudyAsync(memoryStream, fileName, "application/pdf");
                    if (pdfResult != HttpStatusCode.OK)
                    {
                        return StatusCode((int)pdfResult, "Error al cargar el archivo PDF.");
                    }
                }

                Study newStudy = new Study() 
                {
                    LocationS3 = fileName,
                    Date = DateTime.Now,
                    Note = study.Note,
                    Patient = patient,
                    StudyType = studyType
                };

                await _studyService.Add(newStudy);

                return Ok("Se ha guardado correctamente el estudio.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }
    }
}
