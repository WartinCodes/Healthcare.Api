using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [EnableCors("MyPolicy")]
    //[Authorize(Roles = "Administrador")]
    public class DoctorController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly IDoctorService _doctorService;
        private readonly IAddressService _addressService;
        private readonly ISpecialityService _specialityService;
        private readonly IDoctorSpecialityService _doctorSpecialityService;
        private readonly IHealthPlanService _healthPlanService;
        private readonly IDoctorHealthInsuranceService _doctorHealthInsuranceService;
        private readonly IFileService _fileService;

        private readonly IMapper _mapper;

        public DoctorController(
            UserManager<User> userManager, 
            IMapper mapper, 
            IDoctorService doctorService,
            IAddressService addressService,
            ISpecialityService specialityService, 
            IHealthPlanService healthPlanService,
            IDoctorSpecialityService doctorSpecialityService,
            IDoctorHealthInsuranceService doctorHealthInsuranceService,
            IFileService fileService)
        {
            _addressService = addressService;
            _doctorService = doctorService;
            _doctorSpecialityService = doctorSpecialityService;
            _doctorHealthInsuranceService = doctorHealthInsuranceService;
            _healthPlanService = healthPlanService;
            _mapper = mapper;
            _specialityService = specialityService;
            _userManager = userManager;
            _fileService = fileService;
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
                    HealthInsurances = _mapper.Map<ICollection<HealthInsuranceResponse>>(x.HealthInsurances),
                    Email = x.User.Email,
                    PhoneNumber = x.User.PhoneNumber,
                    Photo = x.User.Photo
                });

            return Ok(doctors);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<DoctorResponse>> Get([FromRoute] int userId)
        {
            var doctorEntity = await _doctorService.GetDoctorByUserIdAsync(userId);

            var doctor = new DoctorResponse()
            {
                Id = doctorEntity.Id,
                FirstName = doctorEntity.User.FirstName,
                LastName = doctorEntity.User.LastName,
                Matricula = doctorEntity.Matricula,
                DNI = doctorEntity.User.UserName,
                BirthDate = doctorEntity.User.BirthDate,
                Address = _mapper.Map<AddressResponse>(doctorEntity.Address),
                Specialities = _mapper.Map<ICollection<DoctorSpecialityResponse>>(doctorEntity.DoctorSpecialities),
                HealthInsurances = _mapper.Map<ICollection<HealthInsuranceResponse>>(doctorEntity.HealthInsurances),
                Email = doctorEntity.User.Email,
                PhoneNumber = doctorEntity.User.PhoneNumber,
                Photo = doctorEntity.User.Photo
            };

            return Ok(doctor);
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromForm] DoctorRequest userRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);
                var userDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if (user != null || userDocument != null)
                {
                    return Conflict("DNI/Email ya existe.");
                }

                string fileName = userRequest.Photo == null ? String.Empty : Guid.NewGuid().ToString();
                var newUser = _mapper.Map<User>(userRequest);
                newUser.PasswordHash = newUser.UserName;
                newUser.Photo = fileName;

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
                        HealthInsurances = null,
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

                    foreach (var healthPlan in userRequest.HealthInsurances)
                    {
                        var healthPlanEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthPlan.Id);
                        if (healthPlanEntity == null)
                        {
                            return BadRequest($"Plan con ID {healthPlanEntity} no encontrada.");
                        }

                        var doctorHealthPlan = new DoctorHealthInsurance { DoctorId = doctor.Id, HealthInsuranceId = healthPlanEntity.Id };
                        await _doctorHealthInsuranceService.Add(doctorHealthPlan);
                    }

                    if (!String.IsNullOrEmpty(fileName))
                    {
                        using (MemoryStream memoryStream = new MemoryStream())
                        {
                            await userRequest.Photo.CopyToAsync(memoryStream);
                            var imageResult = await _fileService.InsertPhotoAsync(memoryStream, fileName, "image/jpeg");
                            if (imageResult != HttpStatusCode.OK)
                            {
                                return StatusCode((int)imageResult, "Error al cargar la imagen en S3.");
                            }
                        }
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
        public async Task<IActionResult> Put([FromForm] DoctorRequest userRequest, int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound($"No se encontró el doctor con el ID: {id}");
            }

            var user = await _userManager.FindByIdAsync(doctor.UserId.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {id}");
            }

            var existEmail = await _userManager.FindByEmailAsync(userRequest.Email);
            var existDocument = await _userManager.FindByNameAsync(userRequest.UserName);
            if (existEmail != null && doctor.UserId != existEmail.Id)
            {
                return Conflict("Email ya existe.");
            }
            if (existDocument != null && doctor.UserId != existDocument.Id)
            {
                return Conflict("DNI ya existe.");
            }

            _mapper.Map(userRequest, user);
            var result = await _userManager.UpdateAsync(user);

            var newAddress = _mapper.Map<Address>(userRequest.Address);
            _addressService.Edit(newAddress);

            // borrado de las obras sociales asociadas al doctor en tabla DoctorHealthPlanService
            var doctorHealthInsurances = await _doctorHealthInsuranceService.GetHealthPlansByDoctor(id);
            foreach (var php in doctorHealthInsurances)
            {
                _doctorHealthInsuranceService.Remove(php);
            }

            foreach (var healthInsurance in userRequest.HealthInsurances)
            {
                var healthInsuranceEntity = await _healthPlanService.GetHealthPlanByIdAsync(healthInsurance.Id);
                if (healthInsuranceEntity == null)
                {
                    return BadRequest($"Obra social con ID {healthInsuranceEntity} no encontrada.");
                }

                var doctorHealthInsurance = new DoctorHealthInsurance { DoctorId = doctor.Id, HealthInsuranceId = healthInsuranceEntity.Id };
                await _doctorHealthInsuranceService.Add(doctorHealthInsurance);
            }

            // borrado de las especialidades asociadas al doctor
            var doctorSpecialities = await _doctorSpecialityService.GetSpecialitiesByDoctor(id);
            foreach (var ds in doctorSpecialities)
            {
                _doctorSpecialityService.Remove(ds);
            }

            // asignacion de nuevas especialidades
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

            if (!result.Succeeded)
            {
                return BadRequest($"Error al actualizar el médico: {user.UserName}");
            }

            return Ok($"Usuario con el ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(id);
            if (doctor == null)
            {
                return NotFound($"No se encontró el doctor con el ID: {id}");
            }

            var user = await _userManager.FindByIdAsync(doctor.UserId.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {id}");
            }
        
            // borrado de las obras sociales asociadas al doctor en tabla DoctorHealthPlanService
            var doctorHealthInsurances = await _doctorHealthInsuranceService.GetHealthPlansByDoctor(id);
            foreach (var php in doctorHealthInsurances)
            {
                _doctorHealthInsuranceService.Remove(php);
            }
            // borrado de las especialidades asociadas al doctor
            var doctorSpecialities = await _doctorSpecialityService.GetSpecialitiesByDoctor(id);
            foreach (var ds in doctorSpecialities)
            {
                _doctorSpecialityService.Remove(ds);
            }
            // borrado de doctor
            _doctorService.Remove(doctor);
            // borrado de direccion
            _addressService.Remove(doctor.Address);
            // borro foto
            await _fileService.DeletePhotoAsync(user.Photo);
            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al eliminar el usuario con el ID: {id}");
            }

            return Ok($"Médico con el DNI {user.UserName} eliminado exitosamente");
        }
    }
}
