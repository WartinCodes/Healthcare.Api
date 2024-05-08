using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthInsuranceController : ControllerBase
    {
        private readonly IHealthInsuranceService _healthInsuranceService;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IMapper _mapper;

        public HealthInsuranceController(IMapper mapper, IHealthInsuranceService healthInsuranceService, IHealthPlanService healthPlanService)
        {
            _healthInsuranceService = healthInsuranceService;
            _healthPlanService = healthPlanService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<HealthInsuranceResponse>>> Get()
        {
            var healthInsurances = await _healthInsuranceService.GetAsync();
            return Ok(_mapper.Map<IEnumerable<HealthInsuranceResponse>>(healthInsurances));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] HealthInsuranceRequest healthInsuranceRequest)
        {
            try
            {
                var newHealthInsurance = _mapper.Map<HealthInsurance>(healthInsuranceRequest);
                await _healthInsuranceService.Add(newHealthInsurance);

                HealthPlan newHealthPlan = new HealthPlan()
                {
                    HealthInsurance = newHealthInsurance,
                    Name = healthInsuranceRequest.Name
                };
                await _healthPlanService.Add(newHealthPlan);

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
