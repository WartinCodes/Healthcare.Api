namespace Healthcare.Api.Core.Entities
{
    public class HealthPlan
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int HealthInsuranceId { get; set; }
        public virtual HealthInsurance HealthInsurance { get; set; }
        public ICollection<Patient> Patients { get; set; }
    }
}
