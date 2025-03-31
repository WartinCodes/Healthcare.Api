namespace Healthcare.Api.Contracts.Requests
{
    public class StudyRequest
    {
        public List<IFormFile> StudyFiles { get; set; }
        public string UserId { get; set; }
        public int StudyTypeId { get; set; }
        public DateTime Date { get; set; }
        public string Note { get; set; }
        public string? DoctorUserId { get; set; }
    }
}
