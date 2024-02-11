using Healthcare.Api.Core.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Healthcare.Api.Controllers
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    [ApiController]
    public class RoleController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<Role> _roleManager;

        public RoleController(UserManager<User> userManager,
            SignInManager<User> signInManager,
            RoleManager<Role> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        // GET: api/<RoleController>
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<RoleController>/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST api/<RoleController>
        //[HttpPost]
        //public async Task<IActionResult> Post([FromBody] Role newRole)
        //{
        //    var s = _roleManager.CreateAsync(newRole);

        //    return Ok();
        //}

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

        // PUT api/<RoleController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(string id)
        {
            var role = await _roleManager.FindByIdAsync(id);

            if (role == null)
            {
                return NotFound();
            }

            await _roleManager.DeleteAsync(role);
            return NoContent();
        }
    }
}
