using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly ISpecialityService _specialityService;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public SpecialityController(
            ISpecialityService specialityService,
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _specialityService = specialityService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<SpecialityResponse>>> Get()
        {
            var specialities = (await _specialityService.GetAsync())
                .OrderBy(x => x.Name)
                .AsEnumerable();

            return Ok(_mapper.Map<IEnumerable<SpecialityResponse>>(specialities));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] SpecialityRequest specialityRequest)
        {
            try
            {
                var newSpeciality = _mapper.Map<Speciality>(specialityRequest);
                await _specialityService.Add(newSpeciality);

                return Ok("Especialidad creada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] SpecialityRequest specialityRequest)
        {
            try
            {
                var speciality = await _specialityService.GetSpecialityByIdAsync(id);
                if (speciality == null)
                {
                    return NotFound("Especialidad no encontrada.");
                }

                speciality.Name = specialityRequest.Name;
                _specialityService.Edit(speciality);

                return Ok("Especialidad actualizada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var speciality = await _specialityService.GetSpecialityByIdAsync(id);
                if (speciality == null)
                {
                    return NotFound("Especialidad no encontrada.");
                }

                _specialityService.Remove(speciality);
                return Ok("Especialidad eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

    }
}
