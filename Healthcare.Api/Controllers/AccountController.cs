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
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(
            IUserService userService,
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager, 
            IMapper mapper)
        {
            _userService = userService;
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

        //[HttpPut("{id}")]
        //public async Task<IActionResult> Put(int id, [FromBody] UserRequest userRequest)
        //{
        //    try
        //    {
        //        var existingUser = await _userService.GetUserByIdAsync(id);

        //        if (existingUser != null)
        //        {
        //            userRequest.Id = id;
        //            _mapper.Map(userRequest, existingUser);
        //            _userService.Edit(existingUser);

        //            return Ok($"Usuario actualizado exitosamente.");
        //        }

        //        return NotFound($"Usuario inválido.");
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest($"Error al actualizar usuario: {ex.Message}");
        //    }
        //}

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var existingUser = await _userService.GetUserByIdAsync(id);
                if (existingUser == null)
                {
                    return NotFound($"Usuario no encontrado.");
                }

                _userService.Remove(existingUser);
                return Ok($"Usuario con el DNI {existingUser.UserName} borrado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error borrando el usuario: {ex.Message}");
            }
        }
    }
}
