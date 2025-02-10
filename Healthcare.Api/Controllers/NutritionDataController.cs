using AutoMapper;
using Healthcare.Api.Contracts.Requests.NutritionData;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionDataController : ControllerBase
    { 
        private readonly INutritionDataService _nutritionDataService;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public NutritionDataController(
            INutritionDataService nutritionDataService,
            IPatientService patientService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _nutritionDataService = nutritionDataService;
            _patientService = patientService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<NutritionDataResponse>>> GetByPatientId(int userId)
        {
            var patient = await _patientService.GetPatientByUserIdAsync(userId);
            if (patient == null) return BadRequest("Paciente no encontrado.");

            IEnumerable<NutritionData> nutritionDatas = await _nutritionDataService.GetNutritionDatasByPatient(patient.Id);
            if (!nutritionDatas.Any()) return NoContent();

            IEnumerable<NutritionDataResponse> response = _mapper.Map<IEnumerable<NutritionDataResponse>>(nutritionDatas);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NutritionDataCreateRequest nutritionDataCreateRequest)
        {
            try
            {
                var patient = await _patientService.GetPatientByUserIdAsync(nutritionDataCreateRequest.UserId);
                if (patient == null) return BadRequest("Paciente no encontrado.");

                NutritionData newNutritionData = _mapper.Map<NutritionData>(nutritionDataCreateRequest);
                newNutritionData.PatientId = patient.Id;
                NutritionData savedNutritionData = await _nutritionDataService.Add(newNutritionData);
                return Ok(savedNutritionData);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] NutritionDataEditRequest nutritionDataEditRequest)
        {
            var nutritionData = await _nutritionDataService.GetByIdAsync(id);
            if (nutritionData == null)
            {
                return NotFound("Registro no encontrado.");
            }

            _mapper.Map(nutritionDataEditRequest, nutritionData);
            _nutritionDataService.Edit(nutritionData);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                NutritionData? nutritionData = await _nutritionDataService.GetByIdAsync(id);
                if (nutritionData == null)
                {
                    return NotFound("Registro no encontrado.");
                }

                _nutritionDataService.Remove(nutritionData);
                return Ok("Registro eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}