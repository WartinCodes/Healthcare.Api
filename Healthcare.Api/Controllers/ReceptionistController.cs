using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
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

        [HttpGet("{userId}")]
        public async Task<ActionResult<UserResponse>> GetUserById(string userId)
        {
            var recepcionist = await UserManagerExtensions.GetUserById(_userManager, Convert.ToInt32(userId));
            if (recepcionist == null)
            {
                return NotFound($"Secretario/a con ID {userId} no encontrado.");
            }
            return Ok(_mapper.Map<UserResponse>(recepcionist));
        }

        [HttpGet("all")]
        //[Authorize(Roles = $"{RoleEnum.Medico},{RoleEnum.Secretaria}")]
        public async Task<ActionResult<IEnumerable<UserResponse>>> Get()
        {
            var usersEntities = await _userManager.GetUsersInRoleAsync(RoleEnum.Secretaria);
            var recepcionists = _mapper.Map<IEnumerable<UserResponse>>(usersEntities);
            return Ok(usersEntities);
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
                return StatusCode(500, $"Hubo un error al crearse el nuevo Secretario/a: {ex}");
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
                    return NotFound($"No se encontró Secretario/a con el ID: {userId}");
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

                _mapper.Map(userEdit, user);
                var newAddress = _mapper.Map<Address>(userEdit.Address);
                _addressService.Edit(newAddress);
                user.LastActivityDate = DateTime.UtcNow.ToArgentinaTime();

                await _userManager.UpdateAsync(user);

                return Ok($"Secretario/a con el ID {userId} actualizado exitosamente");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Hubo un error al crearse el nuevo Secretario/a: {ex}");
            }

        }
    }
}
