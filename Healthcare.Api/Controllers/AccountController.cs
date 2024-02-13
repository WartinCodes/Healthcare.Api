using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IJwtService _jwtService;
        private readonly IEmailSender _emailSender;
        private readonly IMapper _mapper;

        public AccountController(
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailSender emailSender,
            IMapper mapper)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userManager.Users.Select(x => x).ToListAsync();
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("user")]
        public async Task<ActionResult<User>> GetUserById([FromQuery] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }

            return Ok(user);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] UserLoginRequest userLogin)
        {
            var user = await _userManager.FindByEmailAsync(userLogin.Email);
            if (user == null)
            {
                user = await _userManager.FindByNameAsync(userLogin.UserName);
            }

            if (user == null || !await _userManager.CheckPasswordAsync(user, userLogin.Password))
            {
                return Unauthorized();
            }

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user, roles);

            return Ok(new { Token = token });
        }

        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return Ok();
        }

        [Authorize(Roles = "Administrador")]
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
            var user = await _userManager.FindByIdAsync(id.ToString());
            if (user == null)
            {
                return NotFound($"No se encontró el usuario con el ID: {id}");
            }

            var existEmail = await _userManager.FindByEmailAsync(userRequest.Email);
            var existDocument = await _userManager.FindByNameAsync(userRequest.UserName);
            if (existEmail != null || existDocument != null)
            {
                return Conflict("DNI/Email ya existe.");
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

        [HttpPost("forgot-password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user == null)
            {
                // Devuelve NotFound si el usuario no se encuentra
                return NotFound($"Usuario con correo electrónico {email} no encontrado.");
            }

            var token = await _userManager.GeneratePasswordResetTokenAsync(user);

            await _emailSender.SendEmailAsync(email, "Restablecer contraseña", $"Para restablecer tu contraseña, haz clic en el siguiente enlace: {token}");

            return Ok("Correo electrónico de restablecimiento de contraseña enviado exitosamente.");
        }
    }
}
