namespace Healthcare.Api.Core.Entities
{
    public class DoctorHealthPlan
    {
        public int DoctorId { get; set; }
        public Doctor Doctor { get; set; }
        public int HealthPlanId { get; set; }
        public HealthPlan HealthPlan { get; set; }
    }
}
