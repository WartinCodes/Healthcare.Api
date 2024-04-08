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
    [EnableCors("MyPolicy")]
    public class HealthPlanController : ControllerBase
    {
        private readonly IHealthPlanService _healthPlanService;
        private readonly IHealthInsuranceService _healthInsuranceService;
        private readonly IMapper _mapper;

        public HealthPlanController(IMapper mapper, IHealthPlanService healthPlanService, IHealthInsuranceService healthInsuranceService)
        {
            _healthPlanService = healthPlanService;
            _mapper = mapper;
            _healthInsuranceService = healthInsuranceService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<HealthPlanResponse>>> Get()
        {
            var healthPlans = await _healthPlanService.GetAsync();
            return Ok(_mapper.Map<IEnumerable<HealthPlanResponse>>(healthPlans));
        }

        [HttpGet("byHealthInsurance/{id}")]
        public async Task<ActionResult<IEnumerable<HealthPlanResponse>>> GetHealthPlansByHealthInsurance([FromRoute] int id)
        {
            var healthPlans = await _healthPlanService.GetHealthPlansByHealthInsuranceId(id);
            return Ok(_mapper.Map<IEnumerable<HealthPlanResponse>>(healthPlans));
        }

        //[HttpPost("create")]
        //public async Task<IActionResult> Post([FromBody] HealthPlanRequest healthPlanRequest)
        //{
        //    try
        //    {
        //        var healthInsurance = await _healthInsuranceService.GetHealthInsuranceByIdAsync(healthPlanRequest.HealthInsurance.Id);
        //        if (healthInsurance == null)
        //        {
        //            return BadRequest("La obra social especificado no existe.");
        //        }

        //        var newHealthPlan = _mapper.Map<HealthPlan>(healthPlanRequest);
        //        await _healthPlanService.Add(newHealthPlan);

        //        return Ok("Plan de obra social creada exitosamente.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return StatusCode(500, $"An error occurred while processing your request: {ex}");
        //    }
        //}

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