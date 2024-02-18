namespace Healthcare.Api.Core.Entities
{
    public class HealthInsurance
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<HealthPlan> HealthPlans { get; set; }
    }
}
