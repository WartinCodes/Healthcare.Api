using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Produces("application/json")]
    [Route("api/[controller]")]
    public class PatientHistoryController : ControllerBase
    {
        private readonly IPatientHistoryService _patientHistoryService;
        private readonly IMapper _mapper;

        public PatientHistoryController(IPatientHistoryService patientHistoryService, IMapper mapper)
        {
            _patientHistoryService = patientHistoryService;
            _mapper = mapper;
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] PatientHistoryRequest patientHistoryRequest)
        {
            try
            {
                var newPatientHistory = _mapper.Map<PatientHistory>(patientHistoryRequest);
                newPatientHistory.RegistrationDate = DateTime.UtcNow.ToArgentinaTime();
                await _patientHistoryService.Add(newPatientHistory);

                return Ok("Antecedente creado exitosamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<IEnumerable<PatientHistoryResponse>>> GetPatientHistoriesByUserId([FromRoute] int userId)
        {
            var patientHistories = await _patientHistoryService.GetPatientHistoryByUserIdAsync(userId);
            
            return Ok(_mapper.Map<IEnumerable<PatientHistoryResponse>>(patientHistories));
        }
    }
}
