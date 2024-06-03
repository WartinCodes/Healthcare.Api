namespace Healthcare.Api.Core.Entities
{
    public class Patient
    {
        public Patient()
        {
            
        }

        public Patient(int userId, ICollection<HealthPlan> healthPlans)
        {
            UserId = userId;
            HealthPlans = healthPlans ?? new HashSet<HealthPlan>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string AffiliationNumber { get; set; }
        public string Observations { get; set; }
        public string Died { get; set; }
        public ICollection<HealthPlan> HealthPlans { get; set; }
    }
}