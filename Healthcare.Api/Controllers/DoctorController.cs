using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
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
        private readonly string _photosFolder = "photos";

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
        //[Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<IEnumerable<DoctorAllResponse>>> Get()
        {
            var doctorsEntities = (await _doctorService.GetAsync())
                .OrderBy(x => x.User.LastName)
                .AsEnumerable();
            var doctors = _mapper.Map<IEnumerable<DoctorAllResponse>>(doctorsEntities);
            return Ok(doctors);
        }

        [HttpGet("lastDoctors")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<int>> GetLastPatient()
        {
            var latestUsers = await UserManagerExtensions.GetUsersRegisteredInLastWeek(_userManager);
            var countLatestDoctors = (await _doctorService.GetAsync())
                .Where(x => latestUsers.Contains(x.UserId))
                .ToList()
                .Count();

            return Ok(countLatestDoctors);
        }

        [HttpGet("{userId}")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<DoctorIdResponse>> Get([FromRoute] int userId)
        {
            var doctorEntity = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctorEntity == null)
            {
                return NotFound($"El doctor con el ID usuario {userId} no existe.");
            }

            var registeredById = await _userManager.FindByIdAsync(doctorEntity.User.RegisteredById.ToString());
            var RegisteredByName = $"{registeredById.FirstName}" + " " + $"{registeredById.LastName}";

            var doctorIdResponse = new DoctorIdResponse
            {
                RegisteredByName = RegisteredByName,
            };

            _mapper.Map(doctorEntity, doctorIdResponse);
            return Ok(doctorIdResponse);

        }

        [HttpPost("create")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> Post([FromBody] DoctorRequest userRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);
                var userDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if ((user != null && !String.IsNullOrEmpty(user.Email)) || userDocument != null)
                {
                    return Conflict("DNI/Email ya existe.");
                }

                string fileName = userRequest.Photo == null ? String.Empty : Guid.NewGuid().ToString();
                var newUser = _mapper.Map<User>(userRequest);
                newUser.PasswordHash = newUser.UserName;
                newUser.Photo = fileName;

                newUser.RegistrationDate = DateTime.UtcNow.ToArgentinaTime();
                newUser.RegisteredById = userRequest.RegisteredById;
                newUser.CUIL = string.IsNullOrEmpty(userRequest.CUIL) ? string.Empty : userRequest.CUIL;
                newUser.CUIT = string.IsNullOrEmpty(userRequest.CUIT) ? string.Empty : userRequest.CUIT;

                var address = _mapper.Map<Address>(userRequest.Address);
                await _addressService.Add(address);
                newUser.Address = address;

                var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, RoleEnum.Medico);


                    var doctor = new Doctor
                    {
                        UserId = newUser.Id,
                        Matricula = userRequest.Matricula,
                        Specialities = null,
                        HealthInsurances = null,
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

        [HttpPut("{userId}")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> Put(int userId, [FromBody] DoctorRequest userRequest)
        {
            var user = await _userManager.FindByIdAsync(userId.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {userId}");
            }

            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null)
            {
                return NotFound($"No se encontró el doctor con el ID: {userId}");
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

            var doctorData = _mapper.Map<Doctor>(userRequest);
            doctorData.User = user;
            doctorData.Id = doctor.Id;
            doctorData.HealthInsurances = _mapper.Map<ICollection<HealthInsurance>>(userRequest.HealthInsurances);
            doctorData.Specialities = _mapper.Map<ICollection<Speciality>>(userRequest.Specialities);

            await _doctorService.Edit(doctorData);

            var newAddress = _mapper.Map<Address>(userRequest.Address);
            _addressService.Edit(newAddress);

            _mapper.Map(userRequest, user);
            var result = await _userManager.UpdateAsync(user);

            if (!result.Succeeded)
            {
                return BadRequest($"Error al actualizar el médico: {user.UserName}");
            }

            return Ok($"Usuario con el ID {userId} actualizado exitosamente");
        }

        [HttpPut("{userId}/sello")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<IActionResult> UpdateSello(int userId, IFormFile sello)
        {
            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null)
            {
                return NotFound($"No se encontró el doctor con el ID: {userId}");
            }

            string fileName = $"{userId}_sello{Path.GetExtension(sello.FileName)}";
            using (var stream = sello.OpenReadStream())
            {
                await _fileService.InsertDoctorFileAsync(stream, doctor.User.UserName, fileName);
            }

            doctor.Sello = fileName;
            await _doctorService.Edit(doctor);
            var signedUrl = _fileService.GetSignedUrl(_photosFolder, doctor.User.UserName, fileName);
            return Ok(signedUrl);
        }

        [HttpPut("{userId}/firma")]
        public async Task<IActionResult> UpdateFirma(int userId, IFormFile firma)
        {
            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null)
            {
                return NotFound($"No se encontró el doctor con el ID: {userId}");
            }

            var fileName = $"{userId}_firma{Path.GetExtension(firma.FileName)}";
            using (var stream = firma.OpenReadStream())
            {
                await _fileService.InsertDoctorFileAsync(stream, doctor.User.UserName, fileName);
            }

            doctor.Firma = fileName;
            await _doctorService.Edit(doctor);
            var signedUrl = _fileService.GetSignedUrl(_photosFolder, doctor.User.UserName, fileName);
            return Ok(signedUrl);
        }

        [HttpDelete("{userId}")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> Delete(int userId)
        {
            var doctor = await _doctorService.GetDoctorByUserIdAsync(userId);
            if (doctor == null)
            {
                return NotFound($"No se encontró el doctor con el usuario ID: {userId}");
            }

            var user = await _userManager.FindByIdAsync(doctor.UserId.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {userId}");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al eliminar el usuario con el ID: {userId}");
            }

            return Ok($"Médico con el DNI {user.UserName} eliminado exitosamente");
        }
    }
}
