using AutoMapper;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    //[Authorize(Roles = "Administrador")]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;
        private readonly IMapper _mapper;

        public RoleController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
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
            try
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
            catch (Exception ex)
            {
                throw ex;
            }
        }

        //// PUT api/<RoleController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

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
