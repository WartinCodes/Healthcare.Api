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

        public BloodTestController(IBloodTestService unitService, IMapper mapper)
        {
            _bloodTestService = bloodTestService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public ActionResult<IEnumerable<UnitResponse>> Get()
        {
            var units = _unitService.GetAsQueryable();

            return Ok(_mapper.Map<IEnumerable<UnitResponse>>(units));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<UnitResponse>> GetUnitById([FromRoute] int id)
        {
            var unit = await _unitService.GetUnitByIdAsync(id);
            if (unit == null)
            {
                return NotFound("Unidad no encontrada.");
            }
            return Ok(_mapper.Map<UnitResponse>(unit));
        }

        [HttpPost]
        public async Task<ActionResult<UnitResponse>> Create([FromBody] UnitRequest request)
        {
            var existUnit = await _unitService.GetUnitByNameOrShortNameAsync(request.Name, request.ShortName);
            if (existUnit != null)
            {
                return Conflict("El nombre o abreviatura de la unidad ya existe.");
            }

            Unit newUnit = _mapper.Map<Unit>(request);
            var unit = await _unitService.Add(newUnit);
            return Ok(unit);
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UnitRequest unitRequest)
        {
            try
            {
                var unit = await _unitService.GetUnitByIdAsync(id);
                if (unit == null)
                {
                    return NotFound("Unidad no encontrada.");
                }
                _mapper.Map(unitRequest, unit);
                await _unitService.Edit(unit);
                return Ok("Unidad actualizada exitosamente.");
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
                var unit = await _unitService.GetUnitByIdAsync(id);
                if (unit == null)
                {
                    return NotFound("Unidad no encontrada.");
                }

                _unitService.Remove(unit);
                return Ok("Unidad eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}
