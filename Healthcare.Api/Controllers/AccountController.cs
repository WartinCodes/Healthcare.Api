using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            IMapper mapper)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        // GET: api/<AccountController>
        //[Authorize(Roles = "Administrador")]
        //[HttpGet("all")]
        //public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        //{
        //    var users = await _userService.GetAsync();

        //    return Ok(_mapper.Map<IEnumerable<UserResponse>>(users));
        //}

        //[HttpGet]
        //public async Task<ActionResult<UserResponse>> Get([FromQuery] int id)
        //{
        //    var user = await _userService.GetUserByIdAsync(id);

        //    if (user == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(_mapper.Map<UserResponse>(user));
        //}

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            var userEmail = await _userManager.FindByEmailAsync(userLogin.Email);
            var userDocument = await _userManager.FindByNameAsync(userLogin.UserName);

            if (userEmail != null && await _userManager.CheckPasswordAsync(userEmail, userLogin.Password))
            {
                var roles = await _userManager.GetRolesAsync(userEmail);
                var token = _jwtService.GenerateToken(userEmail, roles);

                return Ok(new { Token = token });
            }

            return Unauthorized();
        }

        //[Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        //[Authorize(Roles = "Administrador")]
        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] User newUser)
        {
            var user = await _userManager.FindByEmailAsync(newUser.Email);
            var userDocument = await _userManager.FindByNameAsync(newUser.UserName);
            if (user != null || userDocument != null)
            {
                return Conflict("DNI/Email ya existe.");
            }
            var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);

            return Ok(result);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserRequest userRequest)
        {
            // AGREGAR VALIDACION DE SI DNI O EMAIL REPETIDO
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {id}");
            }

            _mapper.Map(userRequest, user);
            var result = await _userManager.UpdateAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al actualizar el usuario con el ID: {id}");
            }

            return Ok($"Usuario con el ID {id} actualizado exitosamente");
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {id}");
            }

            var result = await _userManager.DeleteAsync(user);
            if (!result.Succeeded)
            {
                return BadRequest($"Error al eliminar el usuario con el ID: {id}");
            }

            return Ok($"Usuario con el ID {id} eliminado exitosamente");
        }
    }
}
