using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReceptionistController : ControllerBase
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public ReceptionistController(
            UserManager<User> userManager,
            SignInManager<User> signInManager,
            IAddressService addressService,
            IMapper mapper)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _addressService = addressService;
            _mapper = mapper;
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
                    return Ok("Secretario/a creado exitosamente.");
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
    }
}
