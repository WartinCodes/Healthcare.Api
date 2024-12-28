using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Core.ServiceInterfaces
{
    public interface IPdfFileService
    {
        List<BloodTestData> ParsePdfText(string text, int idStudy, IEnumerable<BloodTest> properties);
    }
}
