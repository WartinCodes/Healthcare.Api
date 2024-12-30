using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodTestController : ControllerBase
    {
        private readonly IBloodTestService _bloodTestService;
        private readonly IMapper _mapper;

        public BloodTestController(IBloodTestService bloodTestService, IMapper mapper)
        {
            _bloodTestService = bloodTestService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<BloodTestResponse>>> Get()
        {
            var bloodTest = await _bloodTestService.GetBloodTestsAsync();
            return Ok(_mapper.Map<IEnumerable<BloodTestResponse>>(bloodTest));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<BloodTestResponse>> GetUnitById([FromRoute] int id)
        {
            var bloodTest = await _bloodTestService.GetBloodTestByIdAsync(id);
            if (bloodTest == null)
            {
                return NotFound("Parametro no encontrado.");
            }
            return Ok(_mapper.Map<BloodTestResponse>(bloodTest));
        }

        [HttpPost]
        public async Task<ActionResult<BloodTestResponse>> Create([FromBody] BloodTestRequest request)
        {
            var existBloodTest = await _bloodTestService.GetBloodTestByNamesAsync(request.OriginalName, request.ParsedName);
            if (existBloodTest != null)
            {
                return Conflict("El parametro ya existe.");
            }

            BloodTest newBloodTest = _mapper.Map<BloodTest>(request);
            var unit = await _bloodTestService.Add(newBloodTest);
            return Ok(unit);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] BloodTestRequest bloodTestRequest)
        {
            try
            {
                var bloodTest = await _bloodTestService.GetBloodTestByIdAsync(id);
                if (bloodTest == null)
                {
                    return NotFound("Parametro no encontrado.");
                }
                _mapper.Map(bloodTestRequest, bloodTest);
                await _bloodTestService.Edit(bloodTest);
                return Ok("Parametro actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bloodTest = await _bloodTestService.GetBloodTestByIdAsync(id);
                if (bloodTest == null)
                {
                    return NotFound("Parametro no encontrado.");
                }

                _bloodTestService.Remove(bloodTest);
                return Ok("Parametro eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}
