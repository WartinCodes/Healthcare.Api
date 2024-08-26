using Healthcare.Api.Core.Entities;

namespace Healthcare.Api.Contracts.Responses
{
    public class StudyResponse
    {
        public int Id { get; set; }
        public string LocationS3 { get; set; }
        public int StudyTypeId { get; set; }
        public StudyTypeResponse StudyType { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public List<UltrasoundImageResponse> UltrasoundImages { get; set; }
    }
}