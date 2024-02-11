using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [AllowAnonymous]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly IUserService _userService;
        private readonly IJwtService _jwtService;
        private readonly IMapper _mapper;

        public AccountController(IUserService userService, IJwtService jwtService, IMapper mapper)
        {
            _userService = userService;
            _jwtService = jwtService;
            _mapper = mapper;
        }

        // GET: api/<AccountController>
        [Authorize(Roles = "Administrador")]
        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var users = await _userService.GetAsync();

            return Ok(_mapper.Map<IEnumerable<UserResponse>>(users));
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet]
        public async Task<ActionResult<UserResponse>> Get([FromQuery] int id)
        {
            var user = await _userService.GetUserByIdAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponse>(user));
        }

        [HttpPost("login")]
        public async Task<IActionResult> Post([FromBody] UserLoginRequest userLogin)
        {
            var user = await _userService.FindUserByEmailOrDni(userLogin.Email, userLogin.NationalIdentityDocument);
            if (user == null)
            {
                return NotFound("DNI/Email inválidos.");
            }

            if (await _userService.ValidateUserCredentials(user.NationalIdentityDocument, userLogin.Password))
            {
                user.LastLoginDate = DateTime.Now;
                user.LastActivityDate = DateTime.Now;
                _userService.Edit(user);
                var token = _jwtService.GenerateToken(userLogin.Email, "TEST");
                return Ok(token);
            }

            return Unauthorized();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Post([FromBody] UserRequest userRequest)
        {
            var user = await _userService.FindUserByEmailOrDni(userRequest.NationalIdentityDocument, userRequest.Email);
            if (user != null)
            {
                return Conflict("DNI/Email ya existe.");
            }
            var newUser = _mapper.Map<User>(userRequest);

            await _userService.AddAsync(newUser);

            return Ok(newUser);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] UserRequest userRequest)
        {
            try
            {
                var existingUser = await _userService.GetUserByIdAsync(id);

                if (existingUser != null)
                {
                    userRequest.Id = id;
                    _mapper.Map(userRequest, existingUser);
                    _userService.Edit(existingUser);

                    return Ok($"Usuario actualizado exitosamente.");
                }

                return NotFound($"Usuario inválido.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error al actualizar usuario: {ex.Message}");
            }
        }

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

                return Ok($"Usuario con el DNI {existingUser.NationalIdentityDocument} borrado exitosamente.");
            }
            catch (Exception ex)
            {
                return BadRequest($"Error borrando el usuario: {ex.Message}");
            }
        }
    }
}
