using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInsuranceController : ControllerBase
    {
        private readonly IHealthInsuranceService _healthInsuranceService;
        private readonly IMapper _mapper;

        public HealthInsuranceController(IMapper mapper, IHealthInsuranceService healthInsuranceService)
        {
            _healthInsuranceService = healthInsuranceService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<HealthInsurance>>> Get()
        {
            var healthInsurances = await _healthInsuranceService.GetAsync();
            return Ok(healthInsurances);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] HealthInsuranceRequest healthInsuranceRequest)
        {
            try
            {
                var newHealthInsurance = _mapper.Map<HealthInsurance>(healthInsuranceRequest);
                await _healthInsuranceService.Add(newHealthInsurance);

                return Ok("Obra social creada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HealthInsuranceRequest healthInsuranceRequest)
        {
            try
            {
                var healthInsurance = await _healthInsuranceService.GetHealthInsuranceByIdAsync(id);
                if (healthInsurance == null)
                {
                    return NotFound("Obra social no encontrada.");
                }

                healthInsurance.Name = healthInsuranceRequest.Name;
                _healthInsuranceService.Edit(healthInsurance);

                return Ok("Obra social actualizada exitosamente.");
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
                var healthInsurance = await _healthInsuranceService.GetHealthInsuranceByIdAsync(id);
                if (healthInsurance == null)
                {
                    return NotFound("Obra social no encontrada.");
                }

                _healthInsuranceService.Remove(healthInsurance);
                return Ok("Obra social eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}
