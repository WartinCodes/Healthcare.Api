namespace Healthcare.Api.Core.Entities
{
    public class Patient
    {
        public Patient()
        {
            
        }

        public Patient(int userId, string cuil, ICollection<HealthPlan> healthPlans)
        {
            UserId = userId;
            CUIL = cuil;
            HealthPlans = healthPlans ?? new HashSet<HealthPlan>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string CUIL { get; set; }
        public int IdAddress { get; set; }
        public virtual Address Address { get; set; }
        public ICollection<HealthPlan> HealthPlans { get; set; }
    }
}