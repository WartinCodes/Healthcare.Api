using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
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

        private readonly IMapper _mapper;

        public DoctorController(UserManager<User> userManager, IMapper mapper, IDoctorService doctorService, IAddressService addressService)
        {
            _userManager = userManager;
            _doctorService = doctorService;
            _mapper = mapper;
            _addressService = addressService;
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
