using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Entities.DTO;
using Healthcare.Api.Core.Extensions;
using Healthcare.Api.Core.RepositoryInterfaces;
using Healthcare.Api.Core.ServiceInterfaces;
using Healthcare.Api.Service.Helper;
using iText.IO.Image;
using iText.Kernel.Colors;
using iText.Kernel.Pdf;
using iText.Layout;
using iText.Layout.Element;
using iText.Signatures;
using Microsoft.Extensions.Options;
using System.IO;
using System.Text.RegularExpressions;

namespace Healthcare.Api.Service.Services
{
    public class PdfFileService : IPdfFileService
    {
        private readonly TemplateConfiguration _templateConfiguration;
        private readonly IDoctorRepository _doctorRepository;
        private readonly IFileHelper _fileHelper;
        private readonly IFileService _fileService;
        private readonly HttpClient _httpClient;
        private readonly string _photosFolder = "photos";
        private readonly string _studiesFolder = "studies";

        public PdfFileService(
            IOptions<TemplateConfiguration> templateConfiguration,
            IFileHelper fileHelper,
            IFileService fileService,
            IHttpClientFactory httpClientFactory,
            IDoctorRepository doctorRepository
            )
        {
            _fileHelper = fileHelper;
            _templateConfiguration = templateConfiguration?.Value ?? throw new ArgumentNullException(nameof(templateConfiguration));
            _httpClient = httpClientFactory.CreateClient();
            _fileService = fileService;
            _doctorRepository = doctorRepository;
        }

        /// <summary>
        /// Parses PDF text to extract blood test data based on provided properties.
        /// </summary>
        /// <param name="text">The raw text extracted from the PDF.</param>
        /// <param name="idStudy">The identifier of the study associated with the data.</param>
        /// <param name="properties">A collection of blood test properties to match against the text.</param>
        /// <returns>A list of extracted blood test data.</returns>
        public List<BloodTestData> ParsePdfText(string text, int idStudy, IEnumerable<BloodTest> properties)
        {
            var datas = new List<BloodTestData>();
            string[] lines = text.Split('\n').Skip(1).ToArray();

            for (int i = 0; i < lines.Length; i++)
            {
                string cleanLine = lines[i].Trim();

                foreach (var property in properties)
                {
                    if (datas.Any(d => d.IdBloodTest == property.Id))
                        continue;

                    if (!cleanLine.Contains(property.ParsedName, StringComparison.InvariantCultureIgnoreCase))
                        continue;

                    var bloodTestData = new BloodTestData
                    {
                        IdStudy = idStudy,
                        IdBloodTest = property.Id,
                        Value = ExtractBloodTestValue(cleanLine, lines, i)
                    };

                    datas.Add(bloodTestData);
                    break;
                }
            }

            return datas;
        }

        /// <summary>
        /// Extracts the value of a blood test from a specific line and nearby lines if needed.
        /// </summary>
        /// <param name="line">The current line being processed.</param>
        /// <param name="lines">All lines from the PDF text.</param>
        /// <param name="currentIndex">The index of the current line.</param>
        /// <returns>The extracted value as a string.</returns>
        private string ExtractBloodTestValue(string line, string[] lines, int currentIndex)
        {
            if (TryExtractTime(line, out var timeValue))
                return timeValue;

            string cleanedLine = CleanLine(line);
            var matches = Regex.Matches(cleanedLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");

            if (matches.Count > 0)
                return FormatMatches(matches, cleanedLine);

            return SearchNextLinesForValue(lines, currentIndex);
        }

        /// <summary>
        /// Attempts to extract a time value (e.g., HH:MM) from a line.
        /// </summary>
        /// <param name="line">The line to search for a time value.</param>
        /// <param name="timeValue">The extracted time value if found.</param>
        /// <returns>True if a time value is found; otherwise, false.</returns>
        private bool TryExtractTime(string line, out string timeValue)
        {
            var timeMatch = Regex.Match(line, @"\b\d{1,2}:\d{2}\b");
            if (timeMatch.Success)
            {
                timeValue = timeMatch.Value;
                return true;
            }

            timeValue = null;
            return false;
        }

        /// <summary>
        /// Cleans a line by removing unwanted prefixes and patterns.
        /// </summary>
        /// <param name="line">The line to clean.</param>
        /// <returns>The cleaned line.</returns>
        private string CleanLine(string line)
        {
            line = Regex.Replace(line, @"^\d+\s*-\s*", "").Trim();
            line = Regex.Replace(line, @"\s*[-(]\s*\d+.*$", "").Trim();
            return line;
        }

        /// <summary>
        /// Formats matched numeric values from a regex collection.
        /// </summary>
        /// <param name="matches">A collection of regex matches.</param>
        /// <param name="line">The original line for context.</param>
        /// <returns>A formatted string representing the numeric value.</returns>
        private string FormatMatches(MatchCollection matches, string line)
        {
            string formattedNumber = string.Join(".", matches.Cast<Match>().Select(m => m.Value));

            if (line.Contains("eritrosedimentacion", StringComparison.InvariantCultureIgnoreCase))
                formattedNumber = matches.Last().Value.Split(',').Last();

            return formattedNumber;
        }

        /// <summary>
        /// Searches subsequent lines for numeric values if none are found in the current line.
        /// </summary>
        /// <param name="lines">All lines from the PDF text.</param>
        /// <param name="currentIndex">The current line index.</param>
        /// <returns>The extracted value if found; otherwise, an empty string.</returns>
        private string SearchNextLinesForValue(string[] lines, int currentIndex)
        {
            for (int offset = 2; offset <= 3 && currentIndex + offset < lines.Length; offset++)
            {
                var nextLine = lines[currentIndex + offset].Trim();
                var matches = Regex.Matches(nextLine, @"(?<![a-zA-Z])\d{1,3}(?:\.\d{3})*(?:,\d+)?(?![a-zA-Z])");

                if (matches.Count > 0)
                    return string.Join(",", matches.Cast<Match>().Select(m => m.Value));
            }

            return string.Empty;
        }

        public async Task BuildMedicalReport(GenerateMedicalReportPdf medicalReport)
        {
            if (string.IsNullOrEmpty(medicalReport.DoctorUserId))
            {
                await SavePdfAsync(medicalReport.StudyFileBytes, medicalReport.UserName, medicalReport.PdfFileName);
                return;
            }

            var doctor = await _doctorRepository.GetDoctorByUserIdAsync(Convert.ToInt32(medicalReport.DoctorUserId));
            if (string.IsNullOrEmpty(doctor.Firma))
                throw new InvalidOperationException("Archivo subido. Doctor sin firma asociada.");

            string coverTemplatePath = Path.Combine(_fileHelper.GetExecutingDirectory(), _templateConfiguration.MedicalReport);

            try
            {
                var signatureUrl = _fileService.GetSignedUrl(_photosFolder, doctor.User.UserName, doctor.Firma, expiryHours: 1);
                var signatureBytes = await _httpClient.GetByteArrayAsync(signatureUrl);

                var finalPdfBytes = await BuildPdfWithCoverAndSignatureAsync(
                    coverTemplatePath,
                    medicalReport.StudyFileBytes,
                    signatureBytes,
                    doctor
                );

                await SavePdfAsync(finalPdfBytes, medicalReport.UserName, medicalReport.PdfFileName);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }

        private async Task SavePdfAsync(byte[] pdfBytes, string userName, string fileName)
        {
            using var memoryStream = new MemoryStream(pdfBytes);
            await _fileService.InsertFileStudyAsync(memoryStream, userName, fileName);
        }

        private async Task<byte[]> BuildPdfWithCoverAndSignatureAsync(
            string coverTemplatePath,
            byte[] studyBytes,
            byte[] signatureBytes,
            Doctor doctor)
        {
            using var outputStream = new MemoryStream();
            using var writer = new PdfWriter(outputStream);
            using var finalPdfDoc = new PdfDocument(writer);

            AddCoverPages(finalPdfDoc, coverTemplatePath);
            AddStudyPagesWithSignature(finalPdfDoc, studyBytes, signatureBytes, doctor);

            return outputStream.ToArray();
        }

        private void AddCoverPages(PdfDocument finalDoc, string coverTemplatePath)
        {
            using var coverReader = new PdfReader(coverTemplatePath);
            using var coverPdfDoc = new PdfDocument(coverReader);
            for (int i = 1; i <= coverPdfDoc.GetNumberOfPages(); i++)
            {
                finalDoc.AddPage(coverPdfDoc.GetPage(i).CopyTo(finalDoc));
            }
        }

        private void AddStudyPagesWithSignature(
            PdfDocument finalDoc,
            byte[] studyBytes,
            byte[] signatureBytes,
            Doctor doctor)
        {
            string fullNameText = BuildPdfSignature.FullName(doctor.User.Gender, doctor.User.FirstName, doctor.User.LastName);
            string matriculaFormatted = BuildPdfSignature.Matricula(doctor.Matricula);
            string specialityText = BuildPdfSignature.DoctorSpeciality(doctor.User.Gender, doctor.Specialities.Select(x => x.Name).ToList());

            using var studyStream = new MemoryStream(studyBytes);
            using var studyReader = new PdfReader(studyStream);
            using var studyPdfDoc = new PdfDocument(studyReader);
            var document = new Document(finalDoc);

            for (int i = 1; i <= studyPdfDoc.GetNumberOfPages(); i++)
            {
                var importedPage = studyPdfDoc.GetPage(i).CopyTo(finalDoc);
                finalDoc.AddPage(importedPage);

                int pageNumber = finalDoc.GetNumberOfPages();
                var pageSize = finalDoc.GetPage(pageNumber).GetPageSize();

                float imageWidth = 64, imageHeight = 75;
                float signatureX = pageSize.GetWidth() - imageWidth - 85;
                float signatureY = 125;

                var signatureImage = new Image(ImageDataFactory.Create(signatureBytes))
                    .ScaleAbsolute(imageWidth, imageHeight)
                    .SetFixedPosition(pageNumber, signatureX, signatureY);

                var nameParagraph = new Paragraph(fullNameText)
                    .SetFontSize(10)
                    .SetFontColor(ColorConstants.BLACK)
                    .SetFixedPosition(pageNumber, signatureX, signatureY - 15, 190);

                var specialityParagraph = new Paragraph(specialityText)
                    .SetFontSize(10)
                    .SetFontColor(ColorConstants.BLACK)
                    .SetFixedPosition(pageNumber, signatureX, signatureY - 25, 190);

                var matriculaParagraph = new Paragraph(matriculaFormatted)
                    .SetFontSize(10)
                    .SetFontColor(ColorConstants.BLACK)
                    .SetFixedPosition(pageNumber, signatureX, signatureY - 35, imageWidth);

                document.Add(signatureImage);
                document.Add(nameParagraph);
                document.Add(specialityParagraph);
                document.Add(matriculaParagraph);
            }


            document.Close();
        }
    }
}