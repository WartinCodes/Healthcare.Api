using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _cityService;
        private readonly IMapper _mapper;

        public CityController(ICityService CityService, IMapper mapper)
        {
            _cityService = CityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityResponse>>> Get()
        {
            var cities = await _cityService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<CityResponse>>(cities));
        }

        [HttpGet("byState/{id}")]
        public async Task<ActionResult<IEnumerable<CityResponse>>> GetCitiesByState([FromRoute] int id)
        {
            var cities = await _cityService.GetCitiesByStateId(id);

            return Ok(_mapper.Map<IEnumerable<CityResponse>>(cities));
        }
    }
}
