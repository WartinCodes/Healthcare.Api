using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
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
        private readonly IEmailService _emailService;
        private readonly IMapper _mapper;

        public AccountController(
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService,
            IMapper mapper)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<User>>> Get()
        {
            return await _userManager.Users.Select(x => x).ToListAsync();
        }

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
            var user = await _userManager.FindByEmailAsync(userLogin.UserName) ?? await _userManager.FindByNameAsync(userLogin.UserName);
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

        [HttpPost("forgot/password")]
        public async Task<IActionResult> ForgotPassword(string email)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(email);
                if (user == null)
                {
                    return NotFound($"Usuario con correo electrónico {email} no encontrado.");
                }

                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var resetLink = _emailService.GenerateResetPasswordLink(email, code);

                await _emailService.SendEmailAsync(email, "Restablecer contraseña", $"Para restablecer tu contraseña, haz clic en el siguiente enlace: {resetLink}");

                return Ok("Correo electrónico de restablecimiento de contraseña enviado exitosamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
