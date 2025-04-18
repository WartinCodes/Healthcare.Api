﻿using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Requests.LaboratoryDetail;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BloodTestDataController : ControllerBase
    {
        private readonly IBloodTestDataService _bloodTestDataService;
        private readonly IBloodTestService _bloodTestService;
        private readonly IStudyService _studyService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public BloodTestDataController(
            IBloodTestDataService bloodTestDataService,
            IBloodTestService bloodTestService,
            IStudyService studyService,
            UserManager<User> userManager,
            IMapper mapper)
        {
            _bloodTestDataService = bloodTestDataService;
            _bloodTestService = bloodTestService;
            _studyService = studyService;
            _userManager = userManager;
            _mapper = mapper;
        }

        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] BloodTestDataCreateRequest bloodTestDataRequest)
        {
            try
            {
                var user = await _userManager.GetUserById(bloodTestDataRequest.UserId);
                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                var newStudy = new Study()
                {
                    Date = bloodTestDataRequest.Date,
                    Created = DateTime.UtcNow.ToArgentinaTime(),
                    LocationS3 = string.Empty,
                    StudyTypeId = (int)StudyTypeEnum.Laboratorio,
                    UserId = user.Id,
                    Note = bloodTestDataRequest.Note
                };
                Study insertedStudy = await _studyService.Add(newStudy);

                var dataLaboratories = _mapper.Map<List<BloodTestData>>(bloodTestDataRequest.BloodTestDatas);
                await _bloodTestDataService.AddRangeAsync(insertedStudy.Id, dataLaboratories);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpGet("byStudies")]
        public async Task<ActionResult<IEnumerable<BloodTestDataStudyResponse>>> GetByStudiesIds([FromQuery] int[] studiesIds)
        {
            if (studiesIds == null || studiesIds.Length == 0)
            {
                return BadRequest("Debe proporcionar al menos un ID de estudio.");
            }

            var bloodDataTests = await _bloodTestDataService.GetBloodTestDatasByStudyIdsAsync(studiesIds);
            if (!bloodDataTests.Any())
            {
                return NoContent();
            }

            var response = _mapper.Map<IEnumerable<BloodTestDataStudyResponse>>(bloodDataTests);

            return Ok(response);
        }

        [HttpPut("{idStudy}")]
        public async Task<IActionResult> Put(int idStudy, [FromBody] List<BloodTestDataRequest> bloodTestDataRequests)
        {
            var study = await _studyService.GetStudyByIdAsync(idStudy);
            if (study == null)
            {
                return NotFound("Estudio no encontrado.");
            }
            if (study.Created.HasValue && (DateTime.UtcNow.ToArgentinaTime() - study.Created.Value).TotalHours > 24)
            {
                return StatusCode(StatusCodes.Status403Forbidden, "No se puede modificar el estudio después de 24 horas de su creación.");
            }
            var dataLaboratories = _mapper.Map<List<BloodTestData>>(bloodTestDataRequests);
            await _bloodTestDataService.AddRangeAsync(idStudy, dataLaboratories);

            return Ok();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var bloodTestData = await _bloodTestDataService.GetBloodTestDataByIdAsync(id);
                if (bloodTestData == null)
                {
                    return NotFound("Registro no encontrado.");
                }

                _bloodTestDataService.Remove(bloodTestData);
                return Ok("Registro eliminado exitosamente.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"An error occurred while processing your request: {ex}");
            }
        }
    }
}