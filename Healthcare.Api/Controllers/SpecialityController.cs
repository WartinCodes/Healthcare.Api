using AutoMapper;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly ISpecialityService _specialityService;
        private readonly IMapper _mapper;

        public SpecialityController(
            ISpecialityService specialityService,
            IMapper mapper)
        {
            _specialityService = specialityService;
            _mapper = mapper;
        }

        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<Speciality>>> Get()
        //{
        //    return await _specialityService.;
        //}

        //[HttpGet]
        //public async Task<ActionResult<Role>> Get([FromQuery] int id)
        //{
        //    var role = await _roleManager.FindByIdAsync(id.ToString());
        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(role);
        //}

        //[HttpPost]
        //public async Task<ActionResult> Create(string Name)
        //{
        //    var role = await _roleManager.RoleExistsAsync(Name);
        //    if (role)
        //    {
        //        return Conflict("El rol ya existe.");
        //    }

        //    Role newRole = new Role();
        //    newRole.Name = Name;
        //    await _roleManager.CreateAsync(newRole);

        //    return Ok("Role creado con éxito.");
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(string id, [FromBody] string roleName)
        //{
        //    var role = await _roleManager.FindByIdAsync(id);
        //    if (role == null)
        //    {
        //        return NotFound($"Rol con ID {id} no encontrado.");
        //    }

        //    role.Name = roleName;
        //    var result = await _roleManager.UpdateAsync(role);
        //    if (result.Succeeded)
        //    {
        //        return Ok($"Rol con ID {id} actualizado exitosamente.");
        //    }
        //    else
        //    {
        //        return BadRequest($"Error al actualizar el rol con ID {id}.");
        //    }
        //}

        //[HttpDelete("{id}")]
        //public async Task<IActionResult> Delete(int id)
        //{
        //    var role = await _roleManager.FindByIdAsync(id.ToString());

        //    if (role == null)
        //    {
        //        return NotFound();
        //    }

        //    await _roleManager.DeleteAsync(role);
        //    return NoContent();
        //}
    }
}
