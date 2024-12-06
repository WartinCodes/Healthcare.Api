using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UnitController : ControllerBase
    {
        private readonly IUnitService _unitService;
        private readonly IMapper _mapper;

        public UnitController(IUnitService unitService, IMapper mapper)
        {
            _unitService = unitService;
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
    }
}
