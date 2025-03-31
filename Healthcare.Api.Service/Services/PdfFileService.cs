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
            byte[] finalPdfBytes;
            if (medicalReport.DoctorId == null)
            {
                finalPdfBytes = medicalReport.StudyFileBytes;
                using (MemoryStream memoryStream = new MemoryStream(finalPdfBytes))
                {
                    var pdfResult = await _fileService.InsertFileStudyAsync(memoryStream, medicalReport.UserName, medicalReport.PdfFileName);
                }
                return;
            }

            Doctor? doctor = await _doctorRepository.GetDoctorByIdAsync(medicalReport.DoctorId.Value);
            string fullNameText = StringExtensions.Gender(doctor.User.Gender) + doctor.User.FirstName + " " + doctor.User.LastName;
            string matriculaFormatted = "Mt. " + int.Parse(doctor.Matricula).ToString("N0").Replace(",", ".");
            string coverTemplatePath = Path.Combine(_fileHelper.GetExecutingDirectory(), _templateConfiguration.MedicalReport);

            try
            {
                string signatureUrl = _fileService.GetSignedUrl(_photosFolder, doctor.User.UserName, doctor.Firma, expiryHours: 1);
                byte[] signatureBytes = await _httpClient.GetByteArrayAsync(signatureUrl);
                byte[] studyBytes = medicalReport.StudyFileBytes;

                using (MemoryStream outputStream = new MemoryStream())
                {
                    using (var writer = new PdfWriter(outputStream))
                    using (var finalPdfDoc = new PdfDocument(writer))
                    {
                        using (var coverReader = new PdfReader(coverTemplatePath))
                        using (var coverPdfDoc = new PdfDocument(coverReader))
                        {
                            int coverPages = coverPdfDoc.GetNumberOfPages();
                            for (int i = 1; i <= coverPages; i++)
                            {
                                finalPdfDoc.AddPage(coverPdfDoc.GetPage(i).CopyTo(finalPdfDoc));
                            }
                        }

                        using (var studyStream = new MemoryStream(studyBytes))
                        using (var studyReader = new PdfReader(studyStream))
                        using (var studyPdfDoc = new PdfDocument(studyReader))
                        {
                            int studyPages = studyPdfDoc.GetNumberOfPages();
                            var document = new Document(finalPdfDoc);

                            for (int i = 1; i <= studyPages; i++)
                            {
                                var importedPage = studyPdfDoc.GetPage(i).CopyTo(finalPdfDoc);
                                finalPdfDoc.AddPage(importedPage);

                                int finalPageNumber = finalPdfDoc.GetNumberOfPages();
                                var page = finalPdfDoc.GetPage(finalPageNumber);
                                var pageSize = page.GetPageSize();
                                float pageWidth = pageSize.GetWidth();
                                float pageHeight = pageSize.GetHeight();

                                float imageWidth = 73;
                                float imageHeight = 85;
                                float signatureX = pageWidth - imageWidth - 85;
                                float signatureY = 125;

                                var signatureImage = new Image(ImageDataFactory.Create(signatureBytes))
                                    .ScaleAbsolute(imageWidth, imageHeight)
                                    .SetFixedPosition(finalPageNumber, signatureX, signatureY);
                                document.Add(signatureImage);

                                Paragraph nameParagraph = new Paragraph(fullNameText)
                                    .SetFontSize(10)
                                    .SetFontColor(ColorConstants.BLACK)
                                    .SetFixedPosition(finalPageNumber, signatureX, signatureY - 15, 190);
                                document.Add(nameParagraph);

                                Paragraph matriculaParagraph = new Paragraph(matriculaFormatted)
                                    .SetFontSize(10)
                                    .SetFontColor(ColorConstants.BLACK)
                                    .SetFixedPosition(finalPageNumber, signatureX, signatureY - 30, imageWidth);
                                document.Add(matriculaParagraph);
                            }

                            document.Close();
                        }
                    }
                    finalPdfBytes = outputStream.ToArray();
                }

                using (MemoryStream memoryStream = new MemoryStream(finalPdfBytes))
                {
                    var pdfResult = await _fileService.InsertFileStudyAsync(memoryStream, medicalReport.UserName, medicalReport.PdfFileName);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }


    }
}