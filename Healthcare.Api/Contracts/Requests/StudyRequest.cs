namespace Healthcare.Api.Contracts.Requests
{
    public class StudyRequest
    {
        public IFormFile StudyFile { get; set; }
        public int PatientId { get; set; }
        public int StudyTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}
