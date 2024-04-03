using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Services;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudyTypeController : ControllerBase
    {
        private readonly IStudyTypeService _studyTypeService;
        private readonly IMapper _mapper;

        public StudyTypeController(IMapper mapper, IStudyTypeService studyTypeService)
        {
            _studyTypeService = studyTypeService;
            _mapper = mapper;
        }

        [HttpGet("all")]
        public async Task<ActionResult<IEnumerable<StudyTypeResponse>>> Get()
        {
            var studyTypes = await _studyTypeService.GetAsync();
            return Ok(_mapper.Map<IEnumerable<StudyTypeResponse>>(studyTypes));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<StudyTypeResponse>> Get([FromRoute] int id)
        {
            var studyTypeEntity = await _studyTypeService.GetStudyTypeByIdAsync(id);
            return Ok(_mapper.Map<StudyTypeResponse>(studyTypeEntity));
        }

        [HttpPost("create")]
        public async Task<IActionResult> Post([FromBody] StudyTypeRequest studyTypeRequest)
        {
            try
            {
                var newStudyType = _mapper.Map<StudyType>(studyTypeRequest);
                await _studyTypeService.Add(newStudyType);

                return Ok("Nuevo estudio creado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put(int id, [FromBody] StudyTypeRequest studyTypeRequest)
        {
            try
            {
                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(id);
                if (studyType == null)
                {
                    return NotFound("Tipo de estudio no encontrado.");
                }

                studyType.Name = studyTypeRequest.Name;
                _studyTypeService.Edit(studyType);

                return Ok("Tipo de estudio actualizado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var studyType = await _studyTypeService.GetStudyTypeByIdAsync(id);
                if (studyType == null)
                {
                    return NotFound("Tipo de estudio no encontrado.");
                }

                _studyTypeService.Remove(studyType);
                return Ok("Tipo de estudio eliminada exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}
