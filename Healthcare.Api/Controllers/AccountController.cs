using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
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
        private readonly IEmailService _emailService;
        private readonly IAddressService _addressService;
        private readonly ISupportService _supportService;
        private readonly IMapper _mapper;

        public AccountController(
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService,
            IAddressService addressService,
            ISupportService supportService,
            IMapper mapper)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _addressService = addressService;
            _supportService = supportService;
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

            user.LastLoginDate = DateTime.UtcNow.ToArgentinaTime();
            await _userManager.UpdateAsync(user);

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

                var code = Guid.NewGuid().ToString();
                var resetLink = _emailService.GenerateResetPasswordLink(email, code);

                user.ResetPasswordToken = code;
                user.LastActivityDate = DateTime.UtcNow.ToArgentinaTime();
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

        [HttpPost("change/password")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria},{RoleEnum.Paciente}")]
        public async Task<IActionResult> ChangePassword(ChangePasswordRequest change)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(change.UserId);
                if (user == null)
                {
                    return NotFound($"Usuario no encontrado.");
                }

                var validateCurrentPassword = await _userManager.CheckPasswordAsync(user, change.CurrentPassword);
                if (validateCurrentPassword == false)
                {
                    return BadRequest("Contraseña actual incorrecta");
                }

                if (change.NewPassword != change.ConfirmPassword)
                {
                    return Conflict("Las contraseñas no coinciden");
                }

                user.LastActivityDate = DateTime.UtcNow.ToArgentinaTime();
                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, change.NewPassword);
                await _userManager.UpdateAsync(user);

                return Ok("Nueva contraseña establecida con éxito.");
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
                var user = await UserManagerExtensions.FindByResetTokenAsync(_userManager, reset.Code);
                if (user == null)
                {
                    return NotFound($"Usuario no encontrado.");
                }

                if (reset.Password != reset.ConfirmPassword)
                {
                    return Conflict("Las contraseñas no coinciden");
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, reset.Password);
                user.ResetPasswordToken = null;
                await _userManager.UpdateAsync(user);

                return Ok("Nueva contraseña establecida con éxito.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("reset/default/password/{userId}")]
        [Authorize(Roles = $"{RoleEnum.Secretaria}")]
        public async Task<IActionResult> ResetDefaultPassword(int userId)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound($"Usuario no encontrado.");
                }

                user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, user.UserName);
                await _userManager.UpdateAsync(user);

                return Ok("Se ha restablecido la contraseña por defecto.");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("support")]
        [Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
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

                await _supportService.Add(support);
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
