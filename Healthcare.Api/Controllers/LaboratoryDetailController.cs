using AutoMapper;
using Healthcare.Api.Contracts.Requests.LaboratoryDetail;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Healthcare.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LaboratoryDetailController : ControllerBase
    {
        private readonly ILaboratoryDetailService _laboratoryDetailService;
        private readonly IStudyService _studyService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public LaboratoryDetailController(
            IStudyService studyService,
            ILaboratoryDetailService laboratoryDetailService,
            IMapper mapper,
            UserManager<User> userManager)
        {
            _studyService = studyService;
            _laboratoryDetailService = laboratoryDetailService;
            _mapper = mapper;
            _userManager = userManager;
        }


        [HttpPost("create")]
        public async Task<IActionResult> Create([FromBody] LaboratoryDetailCreateRequest laboratoryDetailRequest)
        {
            try
            {
                var user = await _userManager.GetUserById(laboratoryDetailRequest.UserId);
                if (user == null)
                {
                    return NotFound("Usuario no encontrado.");
                }

                var newStudy = new Study()
                {
                    Date = laboratoryDetailRequest.Date,
                    LocationS3 = string.Empty,
                    Created = DateTime.UtcNow.ToArgentinaTime(),
                    StudyTypeId = (int)StudyTypeEnum.Laboratorio,
                    UserId = user.Id,
                    Note = laboratoryDetailRequest.Note
                };
                Study insertedStudy = await _studyService.Add(newStudy);

                var laboratoryDetail = _mapper.Map<LaboratoryDetail>(laboratoryDetailRequest.LaboratoryDetail);
                laboratoryDetail.IdStudy = insertedStudy.Id;
                await _laboratoryDetailService.Add(laboratoryDetail);

                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


        [HttpPut("edit")]
        public async Task<IActionResult> Update([FromBody] LaboratoryDetailEditRequest laboratoryDetailRequest)
        {
            try
            {
                var laboratoryDetail = _mapper.Map<LaboratoryDetail>(laboratoryDetailRequest.LaboratoryDetail);
                laboratoryDetail.Id = laboratoryDetailRequest.Id;
                _laboratoryDetailService.Edit(laboratoryDetail);
                return Ok();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
