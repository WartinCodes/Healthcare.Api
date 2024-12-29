using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodTestDataController : ControllerBase
    {
        private readonly IBloodTestDataService _bloodTestDataService;
        private readonly IMapper _mapper;

        public BloodTestDataController(IBloodTestDataService bloodTestDataService, IMapper mapper)
        {
            _bloodTestDataService = bloodTestDataService;
            _mapper = mapper;
        }

        [HttpGet("byStudy/{studyId}")]
        public async Task<ActionResult<IEnumerable<BloodTestDataResponse>>> GetByStudyId([FromRoute] int studyId)
        {
            var bloodDataTests = await _bloodTestDataService.GetBloodTestDatasByStudyIdAsync(studyId);
            if (!bloodDataTests.Any())
            {
                return NoContent();
            }
            return Ok(_mapper.Map<IEnumerable<BloodTestDataResponse>>(bloodDataTests));
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bloodTestData = await _bloodTestDataService.GetBloodTestDataByIdAsync(id);
                if (bloodTestData == null)
                {
                    return NotFound("Registro no encontrado.");
                }

                _bloodTestDataService.Remove(bloodTestData);
                return Ok("Registro eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}