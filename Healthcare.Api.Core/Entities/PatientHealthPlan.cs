namespace Healthcare.Api.Core.Entities
{
    public class PatientHealthPlan
    {
        public int Id { get; set; }

        public int PatientId { get; set; }
        public Patient Patient { get; set; }

        public int HealthPlanId { get; set; }
        public HealthPlan HealthPlan { get; set; }
    }
}
