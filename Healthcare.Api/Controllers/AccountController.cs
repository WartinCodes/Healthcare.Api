using AutoMapper;
using Healthcare.Api.Contracts;
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
        [HttpGet]
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        [Authorize(Roles = "Administrador")]
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserLoginRequest userLogin)
        {
            var user = await _userService.FindUserByEmailOrDni(userLogin.NationalIdentityDocument, userLogin.Email);
            if (user == null)
            {
                return NotFound("DNI/Email inválidos.");
            }

            if (await _userService.ValidateUserCredentials(userLogin.NationalIdentityDocument, userLogin.Password))
            {
                var token = _jwtService.GenerateToken(userLogin.Email);
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

        // PUT api/<AccountController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<AccountController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
