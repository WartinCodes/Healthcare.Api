using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrador")]
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
            IHealthPlanService healthPlanService,
            IFileService fileService)
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
            var patientsEntities = (await _patientService.GetAsync())
                .OrderBy(x => x.User.LastName)
                .AsEnumerable();

            var patients = _mapper.Map<IEnumerable<PatientResponse>>(patientsEntities);
            return Ok(patients);
        }


        [HttpGet("lastPatients")]
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
        public async Task<ActionResult<PatientResponse>> Get([FromRoute] int userId)
        {
            var patientEntity = await _patientService.GetPatientByUserIdAsync(userId);
            if (patientEntity == null)
            {
                return NotFound($"El paciente con el ID usuario {userId} no existe.");
            }
            var patient = _mapper.Map<PatientResponse>(patientEntity);
            return Ok(patient);
        }

        [HttpPost("create")]
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
                    //if (!String.IsNullOrEmpty(fileName))
                    //{
                    //    using (MemoryStream memoryStream = new MemoryStream())
                    //    {
                    //        await userRequest.Photo.CopyToAsync(memoryStream);
                    //        var imageResult = await _fileService.InsertPhotoAsync(memoryStream, fileName, "image/jpeg");
                    //        if (imageResult != HttpStatusCode.OK)
                    //        {
                    //            return StatusCode((int)imageResult, "Error al cargar la imagen en S3.");
                    //        }
                    //    }
                    //}
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

                var existEmail = await _userManager.FindByEmailAsync(userRequest.Email);
                var existDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if (existEmail != null && patient.UserId != existEmail.Id)
                {
                    return Conflict("Email ya existe.");
                }
                if (existDocument != null && patient.UserId != existDocument.Id)
                {
                    return Conflict("DNI ya existe.");
                }

                //if (!String.IsNullOrEmpty(user.Photo))
                //{
                //    await _fileService.DeletePhotoAsync(user.Photo);
                //}
                // actualizacion de Address
                var newAddress = _mapper.Map<Address>(userRequest.Address);
                _addressService.Edit(newAddress);

                _mapper.Map(userRequest, user);
                var result = await _userManager.UpdateAsync(user);

                // borrado de las obras sociales asociadas al paciente en tabla PatientHealthPlan
                var patientHealthPlans = await _patientHealthPlanService.GetHealthPlansByPatient(patient.Id);
                foreach (var php in patientHealthPlans)
                {
                    _patientHealthPlanService.Remove(php);
                }

                // agregado de las nuevas obras sociales asociadas al paciente en tabla PatientHealthPlan
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

                //if (!String.IsNullOrEmpty(user.Photo))
                //{
                //    using (MemoryStream memoryStream = new MemoryStream())
                //    {
                //        await userRequest.Photo.CopyToAsync(memoryStream);
                //        var imageResult = await _fileService.InsertPhotoAsync(memoryStream, userRequest.Photo.FileName, "image/jpeg");
                //        if (imageResult != HttpStatusCode.OK)
                //        {
                //            return StatusCode((int)imageResult, "Error al cargar la imagen en S3.");
                //        }
                //    }
                //}

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
