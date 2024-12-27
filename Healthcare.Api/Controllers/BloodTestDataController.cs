using AutoMapper;
using Healthcare.Api.Contracts.Requests;
using Healthcare.Api.Contracts.Responses;
using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Core.Utilities;
using iText.Kernel.Pdf;
using iText.Kernel.Pdf.Canvas.Parser;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Globalization;
using System.Net;
using System.Text.RegularExpressions;

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
        private readonly IBloodTestService _bloodTestService;
        private readonly IBloodTestDataService _bloodTestDataService;
        private readonly UserManager<User> _userManager;
        private List<BloodTestData> _addedBloodTestData = new List<BloodTestData>();

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
            IBloodTestService bloodTestService,
            IBloodTestDataService bloodTestDataService,
            UserManager<User> userManager)
        {
            _fileService = fileService;
            _patientService = patientService;
            _studyService = studyService;
            _studyTypeService = studyTypeService;
            _jwtService = jwtService;
            _emailService = emailService;
            _bloodTestService = bloodTestService;
            _bloodTestDataService = bloodTestDataService;
            _mapper = mapper;
            _laboratoryDetailService = laboratoryDetailService;
            _ultrasoundImageService = ultrasoundImageService;
            _userManager = userManager;
        }

        [HttpPost("upload-study")]
        public async Task<IActionResult> UploadStudy([FromForm] StudyRequest study)
        {
            var user = await _userManager.GetUserById(Convert.ToInt32(study.UserId));
            if (user == null)
            {
                return NotFound("Usuario no encontrado.");
            }

            var studyType = await _studyTypeService.GetStudyTypeByIdAsync(study.StudyTypeId);
            if (studyType == null)
            {
                return NotFound("Tipo de estudio no encontrado.");
            }

            var pdfFile = study.StudyFiles.SingleOrDefault(f => f.FileName.Contains(".pdf", StringComparison.InvariantCultureIgnoreCase));
            string pdfFileName = _studyService.GenerateFileName(new FileNameParameters(user, studyType, study.Date.ToShortDateString(), null, Path.GetExtension(pdfFile.FileName)));

            using (MemoryStream memoryStream = new MemoryStream())
            {
                pdfFile.CopyTo(memoryStream);
                var pdfResult = await _fileService.InsertFileStudyAsync(memoryStream, user.UserName, pdfFileName);
                if (pdfResult != HttpStatusCode.OK)
                {
                    return StatusCode((int)pdfResult, "Error al cargar el archivo PDF.");
                }
            }

            StudyResponse studyResponse = new StudyResponse();

            Study newStudy = new Study()
            {
                LocationS3 = pdfFileName,
                Date = study.Date,
                Note = study.Note,
                UserId = user.Id,
                StudyTypeId = study.StudyTypeId,
            };

            var insertedStudy = await _studyService.Add(newStudy);
            _mapper.Map(newStudy, studyResponse);

            var properties = await _bloodTestService.GetBloodTestsAsync();
            using (var memoryStream = new MemoryStream())
            {
                pdfFile.CopyTo(memoryStream);
                memoryStream.Position = 0;
                using (var pdfReader = new PdfReader(memoryStream))
                {
                    using (var pdfDocument = new PdfDocument(pdfReader))
                    {
                        for (int i = 1; i <= pdfDocument.GetNumberOfPages(); i++)
                        {
                            var page = pdfDocument.GetPage(i);
                            string text = PdfTextExtractor.GetTextFromPage(page);

                            properties = properties.Where(p => !_addedBloodTestData
                                .Any(b => b.IdBloodTest == p.Id))
                                .ToList();

                            List<BloodTestData> pageBloodTestData = ParsePdfText(text, insertedStudy.Id, properties);
                            await _bloodTestDataService.AddRangeAsync(pageBloodTestData);
                            _addedBloodTestData.AddRange(pageBloodTestData);
                        }
                    }
                }
            }

            return Ok();
        }

        private static List<BloodTestData> ParsePdfText(string text, int idStudy, IEnumerable<BloodTest> properties)
        {
            var datas = new List<BloodTestData>();
            string[] lines = text.Split('\n').Skip(1).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                string cleanLine = lines[i].Trim();

                foreach (var property in properties)
                {
                    if (datas.Any(d => d.IdBloodTest == property.Id))
                    {
                        continue;
                    }

                    if (cleanLine.Contains(property.Name, StringComparison.InvariantCultureIgnoreCase))
                    {
                        BloodTestData bloodTestData = new BloodTestData();
                        bloodTestData.IdStudy = idStudy;
                        bloodTestData.IdBloodTest = property.Id;

                        Match timeMatch = Regex.Match(cleanLine, @"\b\d{1,2}:\d{2}\b");

                        if (timeMatch.Success)
                        {
                            bloodTestData.Value = timeMatch.Value;
                        }
                        else
                        {
                            cleanLine = Regex.Replace(cleanLine, @"^\d+\s*-\s*", "").Trim();
                            cleanLine = Regex.Replace(cleanLine, @"\s*[-(]\s*\d+.*$", "").Trim();
                            MatchCollection matches = Regex.Matches(cleanLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");

                            if (matches.Count > 0)
                            {
                                string formattedNumber = string.Join(".", matches.Cast<Match>().Select(m => m.Value));
                                if (cleanLine.Contains("eritrosedimentacion", StringComparison.InvariantCultureIgnoreCase))
                                {
                                    formattedNumber = matches.Last().Value.Split(',').Last();
                                }
                                bloodTestData.Value = formattedNumber;
                            }
                            else if (i + 2 < lines.Length)
                            {
                                cleanLine = lines[i + 2].Trim();
                                matches = Regex.Matches(cleanLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");
                                if (matches.Count == 0 && i + 3 < lines.Length)
                                {
                                    cleanLine = lines[i + 3].Trim();
                                    matches = Regex.Matches(cleanLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");
                                }
                                if (matches.Count > 0)
                                {
                                    string formattedNumber = string.Join(",", matches.Cast<Match>().Select(m => m.Value));
                                    bloodTestData.Value += formattedNumber;
                                }
                            }
                        }

                        datas.Add(bloodTestData);
                        break;
                    }
                }
            }
            
            return datas;
        }
    }
}
