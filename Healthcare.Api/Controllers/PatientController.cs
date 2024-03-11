using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrador")]
    [ApiController]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly IAddressService _addressService;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IPatientHealthPlanService _patientHealthPlanService;

        public PatientController(
            UserManager<User> userManager,
            IMapper mapper,
            IPatientService patientService,
            IPatientHealthPlanService patientHealthPlanService,
            IAddressService addressService,
            IHealthPlanService healthPlanService)
        {
            _userManager = userManager;
            _patientService = patientService;
            _mapper = mapper;
            _addressService = addressService;
            _healthPlanService = healthPlanService;
            _patientHealthPlanService = patientHealthPlanService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> Get()
        {
            var patients = (await _patientService.GetAsync())
                .Select(x => new PatientResponse()
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    DNI = x.User.UserName,
                    Address = _mapper.Map<AddressResponse>(x.Address),
                    HealthPlans = _mapper.Map<ICollection<HealthPlanResponse>>(x.HealthPlans),
                    BirthDate = x.User.BirthDate,
                    Email = x.User.Email,
                    Photo = x.User.Photo,
                    PhoneNumber = x.User.PhoneNumber
                });
            
            return Ok(patients);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<PatientResponse>> Get([FromRoute] int id)
        {
            var patientEntity = await _patientService.GetPatientByIdAsync(id);

            var patient = new PatientResponse()
            {
                Id = patientEntity.Id,
                FirstName = patientEntity.User.FirstName,
                LastName = patientEntity.User.LastName,
                DNI = patientEntity.User.UserName,
                Address = _mapper.Map<AddressResponse>(patientEntity.Address),
                HealthPlans = _mapper.Map<ICollection<HealthPlanResponse>>(patientEntity.HealthPlans),
                BirthDate = patientEntity.User.BirthDate,
                Email = patientEntity.User.Email,
                Photo = patientEntity.User.Photo,
                PhoneNumber = patientEntity.User.PhoneNumber
            };

            return Ok(patient);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] PatientRequest userRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);
                var userDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if (user != null || userDocument != null)
                {
                    return Conflict("DNI/Email ya existe.");
                }

                var newUser = _mapper.Map<User>(userRequest);
                newUser.PasswordHash = newUser.UserName;

                var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, RoleEnum.Paciente);

                    var address = _mapper.Map<Address>(userRequest.Address);
                    await _addressService.Add(address);

                    var patient = new Patient
                    {
                        UserId = newUser.Id,
                        CUIL = String.Empty,
                        HealthPlans = null,
                        Address = address
                    };

                    await _patientService.Add(patient);

                    foreach (var healthPlan in userRequest.HealthPlans)
                    {
                        var healthPlanEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthPlan.Id);
                        if (healthPlanEntity == null)
                        {
                            return BadRequest($"Plan con ID {healthPlan.Id} no encontrada.");
                        }

                        var patientHealthPlan = new PatientHealthPlan { PatientId = patient.Id, HealthPlanId = healthPlan.Id };
                        await _patientHealthPlanService.Add(patientHealthPlan);
                    }

                    return Ok("Paciente creado exitosamente.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] PatientRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el paciente con el ID: {id}");
            }

            var existEmail = await _userManager.FindByEmailAsync(userRequest.Email);
            var existDocument = await _userManager.FindByNameAsync(userRequest.UserName);
            if (existEmail != null || existDocument != null)
            {
                return Conflict("DNI/Email ya existe.");
            }

            _mapper.Map(userRequest, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al actualizar el paciente: {user.UserName}");
            }

            return Ok($"Paciente con el ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el paciente con el ID: {id}");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al eliminar el paciente con el ID: {id}");
            }

            return Ok($"Paciente con el DNI {user.UserName} eliminado exitosamente");
        }
    }
}
