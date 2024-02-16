using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserRoleController : ControllerBase
    {
        private readonly UserManager<User> _userManager;

        public UserRoleController(UserManager<User> userManager)
        {
            _userManager = userManager;
        }

        [HttpGet("{userId}/roles")]
        public async Task<IActionResult> GetUserRoles(string userId)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Usuario con ID {userId} no encontrado.");
            }

            var roles = await _userManager.GetRolesAsync(user);
            return Ok(roles);
        }

        [HttpPost("assign")]
        public async Task<IActionResult> AssignRolesToUser(string userId, [FromBody] string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Usuario con ID {userId} no encontrado.");
            }

            var result = await _userManager.AddToRolesAsync(user, roles);
            if (result.Succeeded)
            {
                return Ok($"Roles asignados al usuario con ID {userId} exitosamente.");
            }
            else
            {
                return BadRequest($"Error al asignar roles al usuario con ID {userId}.");
            }
        }

        [HttpPost("remove")]
        public async Task<IActionResult> RemoveRolesFromUser(string userId, [FromBody] string[] roles)
        {
            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return NotFound($"Usuario con ID {userId} no encontrado.");
            }

            var result = await _userManager.RemoveFromRolesAsync(user, roles);
            if (result.Succeeded)
            {
                return Ok($"Roles eliminados del usuario con ID {userId} exitosamente.");
            }
            else
            {
                return BadRequest($"Error al eliminar roles del usuario con ID {userId}.");
            }
        }
    }
}
