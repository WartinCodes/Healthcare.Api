using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthPlanController : ControllerBase
    {
        private readonly IHealthPlanService _healthPlanService;
        private readonly IMapper _mapper;

        public HealthPlanController(IMapper mapper, IHealthPlanService healthPlanService)
        {
            _healthPlanService = healthPlanService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<HealthPlan>>> Get()
        {
            var healthPlans = await _healthPlanService.GetAsync();
            return Ok(healthPlans);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] HealthPlanRequest healthPlanRequest)
        {
            try
            {
                var newHealthPlan = _mapper.Map<HealthPlan>(healthPlanRequest);
                await _healthPlanService.Add(newHealthPlan);

                return Ok("Plan de obra social creada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] HealthPlanRequest healthPlanRequest)
        {
            try
            {
                var healthPlan = await _healthPlanService.GetHealthPlanByIdAsync(id);
                if (healthPlan == null)
                {
                    return NotFound("Plan de obra social no encontrada.");
                }

                healthPlan.Name = healthPlanRequest.Name;
                _healthPlanService.Edit(healthPlan);

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
                var healthPlan = await _healthPlanService.GetHealthPlanByIdAsync(id);
                if (healthPlan == null)
                {
                    return NotFound("Plan de obra social no encontrada.");
                }

                _healthPlanService.Remove(healthPlan);
                return Ok("Plan de obra social eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}