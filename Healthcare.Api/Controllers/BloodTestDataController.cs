using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.Utilities;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Reflection;

namespace Healthcare.Api.Controllers
{
    public class BloodTestDataController : ControllerBase
    {
        private readonly IStudyService _studyService;
        private readonly IStudyTypeService _studyTypeService;
        private readonly IPatientService _patientService;
        private readonly IFileService _fileService;
        private readonly IJwtService _jwtService;
        private readonly IEmailService _emailService;
        private readonly ILaboratoryDetailService _laboratoryDetailService;
        private readonly IUltrasoundImageService _ultrasoundImageService;
        private readonly IMapper _mapper;
        private readonly UserManager<User> _userManager;

        public BloodTestDataController(
            IFileService fileService,
            IPatientService patientService,
            IStudyService studyService,
            IJwtService jwtService,
            IStudyTypeService studyTypeService,
            IEmailService emailService,
            IMapper mapper,
            ILaboratoryDetailService laboratoryDetailService,
            IUltrasoundImageService ultrasoundImageService,
            UserManager<User> userManager)
        {
            _fileService = fileService;
            _patientService = patientService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _jwtService = jwtService;
            _emailService = emailService;
            _mapper = mapper;
            _laboratoryDetailService = laboratoryDetailService;
            _ultrasoundImageService = ultrasoundImageService;
            _userManager = userManager;
        }



    }
}
