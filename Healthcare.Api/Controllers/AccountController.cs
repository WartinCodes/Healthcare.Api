using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static iText.StyledXmlParser.Jsoup.Select.Evaluator;

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
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var users = await _userManager.Users.Select(x => x).ToListAsync();
            return Ok(_mapper.Map<IEnumerable<UserResponse>>(users));
        }

        [HttpGet("user")]
        public async Task<ActionResult<UserResponse>> GetUserById([FromQuery] string id)
        {
            var user = await _userManager.FindByIdAsync(id);
            if (user == null)
            {
                return NotFound($"Usuario con ID {id} no encontrado.");
            }
            return Ok(_mapper.Map<UserResponse>(user));
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

                user.ResetPasswordToken = code;
                await _userManager.UpdateAsync(user);
                string userName = $"{user.FirstName} {user.LastName}";
                await _emailService.SendForgotPasswordEmailAsync(email, userName, resetLink);

                return Ok("Correo electrónico de restablecimiento de contraseña enviado exitosamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("reset/password")]
        public async Task<IActionResult> ResetPassword(ResetPasswordRequest reset)
        {
            try
            {
                if (reset.Password != reset.ConfirmPassword)
                {
                    return Conflict("Las contraseñas no coinciden");
                }

                var user = await UserManagerExtensions.FindByResetTokenAsync(_userManager, reset.Code);
                if (user == null)
                {
                    return NotFound($"Usuario no encontrado.");
                }

                var result = await _userManager.ResetPasswordAsync(user, reset.Code, reset.Password);

                return Ok("Nueva contraseña establecida con éxito.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("support")]
        public async Task<IActionResult> Support(SupportRequest supportRequest)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(supportRequest.UserId);
                if (user == null)
                {
                    return NotFound($"Usuario con ID {supportRequest.UserId} no encontrado.");
                }

                var support = _mapper.Map<Support>(supportRequest);
                support.ReportDate = DateTime.UtcNow.ToArgentinaTime();
                support.ResolutionDate = null;
                support.Status = StatusEnum.Pendiente;
                string userName = $"{user.FirstName} {user.LastName}";

                await _emailService.SendEmailSupportAsync(userName, support);

                return Ok("Correo electrónico enviado a soporte exitosamente.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
