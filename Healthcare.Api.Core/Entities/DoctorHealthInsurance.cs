namespace Healthcare.Api.Core.Entities
{
    public class DoctorHealthInsurance
    {
        public int Id { get; set; }

        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }

        public int HealthInsuranceId { get; set; }
        public HealthInsurance HealthInsurance { get; set; }
    }
}
