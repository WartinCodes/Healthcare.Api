using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StateController : ControllerBase
    {
        private readonly IStateService _StateService;
        private readonly IMapper _mapper;

        public StateController(IStateService StateService, IMapper mapper)
        {
            _StateService = StateService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<StateResponse>>> Get()
        {
            var states = await _StateService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<StateResponse>>(states));
        }
    }
}
