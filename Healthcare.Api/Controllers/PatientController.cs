using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.Utilities;
using Healthcare.Api.Extensions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [ServiceFilter(typeof(ValidationUserFilter))]
    [Route("api/[controller]")]
    public class PatientController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IPatientService _patientService;
        private readonly IMapper _mapper;
        private readonly IJwtService _jwtService;
        private readonly IAddressService _addressService;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IPatientHealthPlanService _patientHealthPlanService;
        private readonly IEmailService _emailService;

        public PatientController(
            UserManager<User> userManager,
            IMapper mapper,
            IPatientService patientService,
            IPatientHealthPlanService patientHealthPlanService,
            IJwtService jwtService,
            IAddressService addressService,
            IHealthPlanService healthPlanService,
            IFileService fileService,
            IEmailService emailService)
        {
            _userManager = userManager;
            _jwtService = jwtService;
            _patientService = patientService;
            _mapper = mapper;
            _addressService = addressService;
            _healthPlanService = healthPlanService;
            _patientHealthPlanService = patientHealthPlanService;
            _emailService = emailService;
        }

        [HttpGet("all")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<IEnumerable<PatientAllResponse>>> Get()
        {
            var patientsEntities = (await _patientService.GetAsync())
                .OrderBy(x => x.User.LastName)
                .AsEnumerable();

            var patients = _mapper.Map<IEnumerable<PatientAllResponse>>(patientsEntities);
            return Ok(patients);
        }

        [HttpGet]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult<PagedResult<PatientAllResponse>>> Get([FromQuery] PaginationParams paginationParams)
        {
            var result = await _patientService.GetPagedPatientsAsync(paginationParams);
            return Ok(result);
        }

        [HttpGet("lastPatients")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<int>> GetLastPatient()
        {
            var latestUsers = await UserManagerExtensions.GetUsersRegisteredInLastWeek(_userManager);
            var countLatestPatients = (await _patientService.GetAsync())
                .Where(x => latestUsers.Contains(x.UserId))
                .ToList()
                .Count();

            return Ok(countLatestPatients);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}, {RoleEnum.Paciente}")]
        public async Task<ActionResult<PatientIdResponse>> Get([FromRoute] int userId)
        {
            var patientEntity = await _patientService.GetPatientByUserIdAsync(userId);
            if (patientEntity == null)
            {
                return NotFound($"El paciente con el ID usuario {userId} no existe.");
            }
            var registeredById = await _userManager.FindByIdAsync(patientEntity.User.RegisteredById.ToString());
            var RegisteredByName = $"{registeredById.FirstName}" + " " + $"{registeredById.LastName}";

            var patientIdResponse = new PatientIdResponse
            {
                RegisteredByName = RegisteredByName,
            };

            _mapper.Map(patientEntity, patientIdResponse);
            return Ok(patientIdResponse);
        }

        [HttpPost("create")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> Post([FromBody] PatientRequest userRequest)
        {
            try
            {
                User user = new User();
                if (!string.IsNullOrEmpty(userRequest.Email))
                {
                    user = await _userManager.FindByEmailAsync(userRequest.Email);
                }
                var userDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if ((user != null && !String.IsNullOrEmpty(user.Email)) || userDocument != null)
                {
                    return Conflict("DNI/Email ya existe.");
                }

                User newUser = _mapper.Map<User>(userRequest);
                newUser.PasswordHash = userRequest.UserName;
                newUser.Photo = string.Empty;
                DateTime birthDate = DateTime.SpecifyKind(userRequest.BirthDate, DateTimeKind.Utc).ToArgentinaTime();
                newUser.BirthDate = birthDate;
                newUser.RegistrationDate = DateTime.UtcNow.ToArgentinaTime();
                newUser.CUIL = string.IsNullOrEmpty(userRequest.CUIL) ? string.Empty : userRequest.CUIL;
                newUser.CUIT = string.IsNullOrEmpty(userRequest.CUIT) ? string.Empty : userRequest.CUIT;

                var address = _mapper.Map<Address>(userRequest.Address);
                await _addressService.Add(address);
                newUser.Address = address;

                var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, RoleEnum.Paciente);

                    var patient = new Patient
                    {
                        UserId = newUser.Id,
                        Died = string.IsNullOrEmpty(userRequest.Died) ? string.Empty : userRequest.CUIT,
                        AffiliationNumber = string.IsNullOrEmpty(userRequest.AffiliationNumber) ? string.Empty : userRequest.AffiliationNumber,
                        Observations = string.IsNullOrEmpty(userRequest.Observations) ? string.Empty : userRequest.Observations,
                        HealthPlans = null
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

                    if (!String.IsNullOrEmpty(newUser.Email))
                    {
                        await _emailService.SendWelcomeEmailAsync(newUser.Email, newUser.UserName, $"{newUser.FirstName} {newUser.LastName}");
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

        [HttpPut("{userId}")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}, {RoleEnum.Paciente}")]
        public async Task<IActionResult> Put(int userId, [FromBody] PatientRequest userRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound($"No se encontró el usuario con el ID: {userId}");
                }

                var patient = await _patientService.GetPatientByUserIdAsync(userId);
                if (patient == null)
                {
                    return NotFound($"No se encontró el paciente con el usuario ID: {userId}");
                }

                if (!string.IsNullOrEmpty(userRequest.Email)) { 
                   var existEmail = await _userManager.FindByEmailAsync(userRequest.Email);
                   if (existEmail != null && patient.UserId != existEmail.Id)
                   {
                       return Conflict("Email ya existe.");
                   }
                }
                var existDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if (existDocument != null && patient.UserId != existDocument.Id)
                {
                    return Conflict("DNI ya existe.");
                }

                var patientData = _mapper.Map<Patient>(userRequest);
                patientData.User = user;
                patientData.Id = patient.Id;
                patientData.HealthPlans = _mapper.Map<ICollection<HealthPlan>>(userRequest.HealthPlans);

                await _patientService.Edit(patientData);
                await _patientHealthPlanService.Update(patientData);

                var newAddress = _mapper.Map<Address>(userRequest.Address);
                _addressService.Edit(newAddress);

                _mapper.Map(userRequest, user);
                var result = await _userManager.UpdateAsync(user);

                if (!result.Succeeded)
                {
                    return BadRequest($"Error al actualizar el paciente: {user.UserName}");
                }

                return Ok($"Paciente con el ID {userId} actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var patient = await _patientService.GetPatientByUserIdAsync(userId);
            if (patient == null)
            {
                return NotFound($"No se encontró el paciente con el usuario ID: {userId}");
            }

            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {userId}");
            }

            var resultUser = await _userManager.DeleteAsync(user);
            if (!resultUser.Succeeded)
            {
                return BadRequest($"Error al eliminar el usuario con el ID: {userId}");
            }

            return Ok($"Paciente con el DNI {user.UserName} eliminado exitosamente");
        }
    }
}
