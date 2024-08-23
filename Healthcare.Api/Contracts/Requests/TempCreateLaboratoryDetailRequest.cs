namespace Healthcare.Api.Contracts.Requests
{
    public class TempCreateLaboratoryDetailRequest
    {
        public IFormFile StudyFile { get; set; }
        public int StudyId { get; set; }
    }
}
