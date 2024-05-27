using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class CountryController : ControllerBase
    {
        private readonly ICountryService _countryService;
        private readonly IMapper _mapper;

        public CountryController(ICountryService CountryService, IMapper mapper)
        {
            _countryService = CountryService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<CountryResponse>>> Get()
        {
            var countries = await _countryService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<CountryResponse>>(countries));
        }
    }
}
