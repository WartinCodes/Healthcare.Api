namespace Healthcare.Api.Contracts.Requests
{
    public class StudyRequestErgometria
    {
        public List<IFormFile> StudyFiles { get; set; }
        public string UserId { get; set; }
        public DateTime Date { get; set; }
        public int StudyTypeId { get; set; }
        public string Note { get; set; }
        public string? DoctorUserId { get; set; }
    }
}
