using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _roleManager = roleManager;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<RoleResponse>> Get([FromQuery] int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());
            if (role == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<RoleResponse>(role));
        }

        [HttpPost]
        public async Task<ActionResult> Create(string Name)
        {
            var role = await _roleManager.RoleExistsAsync(Name);
            if (role)
            {
                return Conflict("El rol ya existe.");
            }

            Role newRole = new Role();
            newRole.Name = Name;
            await _roleManager.CreateAsync(newRole);

            return Ok("Role creado con éxito.");
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] string roleName)
        {
            var role = await _roleManager.FindByIdAsync(id);
            if (role == null)
            {
                return NotFound($"Rol con ID {id} no encontrado.");
            }

            role.Name = roleName;
            var result = await _roleManager.UpdateAsync(role);
            if (result.Succeeded)
            {
                return Ok($"Rol con ID {id} actualizado exitosamente.");
            }
            else
            {
                return BadRequest($"Error al actualizar el rol con ID {id}.");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var role = await _roleManager.FindByIdAsync(id.ToString());

            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);
            return NoContent();
        }
    }
}
