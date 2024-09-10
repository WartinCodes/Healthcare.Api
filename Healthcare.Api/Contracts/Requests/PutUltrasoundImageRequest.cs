using iText.StyledXmlParser.Node;

namespace Healthcare.Api.Contracts.Requests
{
    public class PutUltrasoundImageRequest
    {
        public int StudyId { get; set; }
        public List<IFormFile> StudyFiles { get; set; }
    }
}
