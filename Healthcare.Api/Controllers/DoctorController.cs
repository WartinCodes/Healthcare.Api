using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    //[Authorize(Roles = "Administrador")]
    [ApiController]
    public class DoctorController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IAddressService _addressService;
        private readonly ISpecialityService _specialityService;
        private readonly IDoctorSpecialityService _doctorSpecialityService;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IDoctorHealthPlanService _doctorHealthPlanService;

        private readonly IMapper _mapper;

        public DoctorController(
            UserManager<User> userManager, 
            IMapper mapper, 
            IDoctorService doctorService,
            IAddressService addressService,
            ISpecialityService specialityService, 
            IHealthPlanService healthPlanService,
            IDoctorSpecialityService doctorSpecialityService,
            IDoctorHealthPlanService doctorHealthPlanService)
        {
            _addressService = addressService;
            _doctorService = doctorService;
            _doctorSpecialityService = doctorSpecialityService;
            _doctorHealthPlanService = doctorHealthPlanService;
            _healthPlanService = healthPlanService;
            _mapper = mapper;
            _specialityService = specialityService;
            _userManager = userManager;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<DoctorResponse>>> Get()
        {
            var doctors = (await _doctorService.GetAsync())
                .Select(x => new DoctorResponse()
                {
                    Id = x.Id,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    Matricula = x.Matricula,
                    DNI = x.User.UserName,
                    Address = _mapper.Map<AddressResponse>(x.Address),
                    Specialities = _mapper.Map<ICollection<DoctorSpecialityResponse>>(x.DoctorSpecialities),
                    HealthPlans = _mapper.Map<ICollection<HealthPlanResponse>>(x.HealthPlans),
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    Photo = x.User.Photo
                });

            return Ok(doctors);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<DoctorResponse>> Get([FromRoute] int id)
        {
            var doctorEntity = await _doctorService.GetDoctorByIdAsync(id);

            var doctor = new DoctorResponse()
            {
                Id = doctorEntity.Id,
                FirstName = doctorEntity.User.FirstName,
                LastName = doctorEntity.User.LastName,
                Matricula = doctorEntity.Matricula,
                DNI = doctorEntity.User.UserName,
                Address = _mapper.Map<AddressResponse>(doctorEntity.Address),
                Specialities = _mapper.Map<ICollection<DoctorSpecialityResponse>>(doctorEntity.DoctorSpecialities),
                HealthPlans = _mapper.Map<ICollection<HealthPlanResponse>>(doctorEntity.HealthPlans),
                Email = doctorEntity.User.Email,
                PhoneNumber = doctorEntity.User.PhoneNumber,
                Photo = doctorEntity.User.Photo
            };

            return Ok(doctor);
        }

        // REVISAR VALIDACIONES, EN EL CASO DE QUE HAYA UN ERROR QUE CANCELE TODO Y NO CREE NADA M{AS
        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] DoctorRequest userRequest)
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
                    await _userManager.AddToRoleAsync(newUser, RoleEnum.Medico);

                    var address = _mapper.Map<Address>(userRequest.Address);
                    await _addressService.Add(address);

                    var doctor = new Doctor
                    {
                        UserId = newUser.Id,
                        Matricula = userRequest.Matricula,
                        DoctorSpecialities = null,
                        HealthPlans = null,
                        Address = address
                    };

                    await _doctorService.Add(doctor);

                    foreach (var speciality in userRequest.Specialities)
                    {
                        var specialityEntity = await _specialityService.GetSpecialityByIdAsync(speciality.Id);
                        if (specialityEntity == null)
                        {
                            return BadRequest($"Especialidad con ID {specialityEntity} no encontrada.");
                        }

                        var doctorSpeciality = new DoctorSpeciality { DoctorId = doctor.Id, SpecialityId = specialityEntity.Id };
                        await _doctorSpecialityService.Add(doctorSpeciality);
                    }

                    foreach (var healthPlan in userRequest.HealthPlans)
                    {
                        var healthPlanEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthPlan.Id);
                        if (healthPlanEntity == null)
                        {
                            return BadRequest($"Plan con ID {healthPlanEntity} no encontrada.");
                        }

                        var doctorHealthPlan = new DoctorHealthPlan { DoctorId = doctor.Id, HealthPlanId = healthPlanEntity.Id };
                        await _doctorHealthPlanService.Add(doctorHealthPlan);
                    }

                    return Ok("Médico creado exitosamente.");
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
        public async Task<IActionResult> Put(int id, [FromBody] DoctorRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el médico con el ID: {id}");
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
                return BadRequest($"Error al actualizar el médico: {user.UserName}");
            }

            return Ok($"Usuario con el ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el médico con el ID: {id}");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al eliminar el médico con el ID: {id}");
            }

            return Ok($"Médico con el DNI {user.UserName} eliminado exitosamente");
        }
    }
}
