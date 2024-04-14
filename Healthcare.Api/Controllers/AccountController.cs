using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
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
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AccountController(
            IJwtService jwtService,
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IEmailService emailService,
            IAddressService addressService,
            IMapper mapper)
        {
            _jwtService = jwtService;
            _mapper = mapper;
            _userManager = userManager;
            _signInManager = signInManager;
            _emailService = emailService;
            _addressService = addressService;
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
            var user = await UserManagerExtensions.GetUserById(_userManager, Convert.ToInt32(id));
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

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(userRequest.Email);
                var userDocument = await _userManager.FindByNameAsync(userRequest.UserName);
                if (user != null || userDocument != null)
                {
                    return Conflict("DNI/Email ya existe.");
                }

                string fileName = userRequest.Photo == null ? String.Empty : Guid.NewGuid().ToString();
                var newUser = _mapper.Map<User>(userRequest);
                newUser.PasswordHash = newUser.UserName;
                newUser.Photo = fileName;
                DateTime birthDate = userRequest.BirthDate.ToArgentinaTime();
                newUser.BirthDate = birthDate;

                var address = _mapper.Map<Address>(userRequest.Address);
                await _addressService.Add(address);
                newUser.Address = address;

                var result = await _userManager.CreateAsync(newUser, newUser.PasswordHash);
                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(newUser, RoleEnum.Secretaria);



                    return Ok("Administrador creado exitosamente.");
                }
                else
                {
                    return BadRequest(result.Errors);
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpPut("{userId}")]
        public async Task<IActionResult> Put(int userId, [FromBody] UserRequest userEdit)
        {
            try
            {
                var user = await _userManager.FindByIdAsync(userId.ToString());
                if (user == null)
                {
                    return NotFound($"No se encontró el usuario con el ID: {userId}");
                }

                var existEmail = await _userManager.FindByEmailAsync(userEdit.Email);
                var existDocument = await _userManager.FindByNameAsync(userEdit.UserName);
                if (existEmail != null && user.Id != existEmail.Id)
                {
                    return Conflict("Email ya existe.");
                }
                if (existDocument != null && user.Id != existDocument.Id)
                {
                    return Conflict("DNI ya existe.");
                }

                var newAddress = _mapper.Map<Address>(userEdit.Address);
                _addressService.Edit(newAddress);

                user.LastActivityDate = DateTime.UtcNow.ToArgentinaTime();
                await _userManager.UpdateAsync(user);

                return Ok($"Usuario con el ID {userId} actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }

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
