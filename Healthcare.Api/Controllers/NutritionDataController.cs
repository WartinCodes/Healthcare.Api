using AutoMapper;
using Healthcare.Api.Contracts.Requests.NutritionData;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Helper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NutritionDataController : ControllerBase
    { 
        private readonly INutritionDataService _nutritionDataService;
        private readonly IExcelHelper _excelHelper;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public NutritionDataController(
            INutritionDataService nutritionDataService,
            IPatientService patientService,
            IExcelHelper excelHelper,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _nutritionDataService = nutritionDataService;
            _userManager = userManager;
            _excelHelper = excelHelper;
            _mapper = mapper;
        }

        [HttpGet("userId")]
        public async Task<ActionResult<IEnumerable<NutritionDataResponse>>> GetByUserId(int userId)
        {
            var user = await _userManager.GetUserById(userId);
            if (user == null) return BadRequest("Usuario no encontrado.");

            IEnumerable<NutritionData> nutritionDatas = await _nutritionDataService.GetNutritionDatasByPatient(userId);
            if (!nutritionDatas.Any()) return NoContent();

            IEnumerable<NutritionDataResponse> response = _mapper.Map<IEnumerable<NutritionDataResponse>>(nutritionDatas);
            return Ok(response);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] NutritionDataCreateRequest nutritionDataCreateRequest)
        {
            try
            {
                var user = await _userManager.GetUserById(nutritionDataCreateRequest.UserId);
                if (user == null) return BadRequest("Usuario no encontrado.");

                NutritionData newNutritionData = _mapper.Map<NutritionData>(nutritionDataCreateRequest);
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
                return NotFound("Registro no encontrado.");

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
                return StatusCode(500, $"Ocurrió un error al eliminar el registro: {ex}");
            }
        }

        [HttpPost("upload-excel")]
        public async Task<IActionResult> UploadExcel(IFormFile file, int userId)
        {
            var user = await _userManager.GetUserById(userId);
            if (user == null) return BadRequest("Usuario no encontrado.");

            try
            {
                List<NutritionData> nutritionDataList = await _excelHelper.ParseNutritionDataExcel(file, userId);
                if (!nutritionDataList.Any()) return NoContent();

                await _nutritionDataService.AddRange(nutritionDataList);
                return Ok("Datos nutricionales importados exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocurrió un error: {ex.Message}");
            }
        }
    }
}