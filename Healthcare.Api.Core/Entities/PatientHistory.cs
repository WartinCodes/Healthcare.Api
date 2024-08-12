namespace Healthcare.Api.Core.Entities
{
    public class PatientHistory
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public Patient Patient { get; set; }
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public string Diagnosis { get; set; }
        public DateTime RegistrationDate { get; set; }
        public string Description { get; set; }
        public DateTime? StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string? Severity { get; set; }
        public string? Status { get; set; }
        public string? Observation { get; set; }
    }
}