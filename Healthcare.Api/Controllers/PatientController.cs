﻿using AutoMapper;
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
        private readonly IFileService _fileService;

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
            _fileService = fileService;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<PatientResponse>>> Get()
        {
            var patients = (await _patientService.GetAsync())
                .Select(x => new PatientResponse()
                {
                    Id = x.Id,
                    UserId = x.UserId,
                    FirstName = x.User.FirstName,
                    LastName = x.User.LastName,
                    DNI = x.User.UserName,
                    Address = _mapper.Map<AddressResponse>(x.User.Address),
                    HealthPlans = _mapper.Map<ICollection<HealthPlanResponse>>(x.HealthPlans),
                    BirthDate = x.User.BirthDate,
                    Email = x.User.Email,
                    Photo = x.User.Photo,
                    PhoneNumber = x.User.PhoneNumber
                })
                .OrderBy(x => x.LastName)
                .AsEnumerable();
            
            return Ok(patients);
        }

        [HttpGet("{userId}")]
        public async Task<ActionResult<PatientResponse>> Get([FromRoute] int userId)
        {
            var patientEntity = await _patientService.GetPatientByUserIdAsync(userId);

            if (patientEntity == null)
            {
                return NotFound($"El paciente con el ID usuario {userId} no existe.");
            }

            var patient = new PatientResponse()
            {
                Id = patientEntity.Id,
                UserId = patientEntity.UserId,
                FirstName = patientEntity.User.FirstName,
                LastName = patientEntity.User.LastName,
                DNI = patientEntity.User.UserName,
                Address = _mapper.Map<AddressResponse>(patientEntity.User.Address),
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

                string fileName = String.Empty;
                var newUser = _mapper.Map<User>(userRequest);
                newUser.PasswordHash = newUser.UserName;
                newUser.Photo = fileName;
                DateTime birthDate = DateTime.SpecifyKind(userRequest.BirthDate, DateTimeKind.Utc).ToArgentinaTime();
                newUser.BirthDate = birthDate;

                var address = _mapper.Map<Address>(userRequest.Address);
                await _addressService.Add(address);
                newUser.Address = address;
                newUser.RegistrationDate = DateTime.Now.ToArgentinaTime();
                newUser.RegisteredById = userRequest.RegisteredById;

                var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, RoleEnum.Paciente);

                    var patient = new Patient
                    {
                        UserId = newUser.Id,
                        CUIL = String.Empty,
                        HealthPlans = null,
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
        public async Task<IActionResult> Put(int userId, [FromBody] PatientRequestEdit userRequest)
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

                // validacion de si user/document no esten asociadas a otro usuario.
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
