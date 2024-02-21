using AutoMapper;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DoctorSpecialityController : ControllerBase
    {
        private readonly IDoctorService _doctorService;
        private readonly ISpecialityService _specialityService;
        private readonly IDoctorSpecialityService _doctorSpecialityService;
        private readonly IMapper _mapper;

        public DoctorSpecialityController(IMapper mapper, IDoctorService doctorService, ISpecialityService specialityService, IDoctorSpecialityService doctorSpecialityService)
        {
            _doctorService = doctorService;
            _specialityService = specialityService;
            _doctorSpecialityService = doctorSpecialityService;
            _mapper = mapper;

        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignSpecialitiesToDoctor(int doctorId, [FromBody] int[] specialityIds)
        {
            var doctor = await _doctorService.GetDoctorByIdAsync(doctorId);
            if (doctor == null)
            {
                return NotFound($"Doctor no encontrado.");
            }

            foreach (var specialityId in specialityIds)
            {
                var speciality = await _specialityService.GetSpecialityByIdAsync(specialityId);
                if (speciality == null)
                {
                    return BadRequest($"Especialidad con ID {specialityId} no encontrada.");
                }

                var doctorSpeciality = new DoctorSpeciality { DoctorId = doctorId, SpecialityId = specialityId };
                await _doctorSpecialityService.Add(doctorSpeciality);
            }

            return Ok($"Especialidades asignadas al doctor exitosamente.");
        }

        // DELETE api/<DoctorSpecialityController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
