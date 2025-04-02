using Healthcare.Api.Core.Entities;
using Healthcare.Api.Core.Entities.DTO;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPdfFileService
    {
        List<BloodTestData> ParsePdfText(string text, int idStudy, IEnumerable<BloodTest> properties);
        Task BuildMedicalReport(GenerateMedicalReportPdf medicalReport);
        Task SavePdfAsync(byte[] pdfBytes, string userName, string fileName);
    }
}
