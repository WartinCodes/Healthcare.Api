using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CityController : ControllerBase
    {
        private readonly ICityService _CityService;
        private readonly IMapper _mapper;

        public CityController(ICityService CityService, IMapper mapper)
        {
            _CityService = CityService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CityResponse>>> Get()
        {
            var cities = await _CityService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<CityResponse>>(cities));
        }
    }
}
