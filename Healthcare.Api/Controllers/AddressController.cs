using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IMapper _mapper;

        public AddressController(IAddressService addressService, IMapper mapper)
        {
            _addressService = addressService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<AddressResponse>> GetById(int idAddress)
        {
            Address? address = await _addressService.GetById(idAddress);
            if (address == null) return NotFound("Dirección no encontrada");
            return Ok(_mapper.Map<AddressResponse>(address));
        }

        [HttpPost]
        public async Task<ActionResult<AddressResponse>> Create([FromBody] AddressRequest request)
        {
            Address addressEntity = _mapper.Map<Address>(request);
            Address newAddress = await _addressService.Add(addressEntity);
            AddressResponse response = _mapper.Map<AddressResponse>(newAddress);
            return Ok(response);
        }

        [HttpPut]
        public async Task<IActionResult> Put(int idAddress, [FromBody] AddressRequest request)
        {
            Address? address = await _addressService.GetById(idAddress);
            if (address == null) return NotFound("Dirección no encontrada.");

            _mapper.Map(request, address);
            _addressService.Edit(address);
            return Ok();
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int idAddress)
        {
            Address? address = await _addressService.GetById(idAddress);
            if (address == null) return NotFound("Dirección no encontrada.");

            _addressService.Remove(address);            
            return Ok();
        }
    }
}