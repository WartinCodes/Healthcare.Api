namespace Healthcare.Api.Core.Entities
{
    public class Doctor
    {
        public Doctor()
        {
            
        }

        public Doctor(int userId, ICollection<Speciality> specialities, ICollection<HealthPlan> healthPlans)
        {
            UserId = userId;
            Specialities = specialities ?? new HashSet<Speciality>();
            HealthPlans = healthPlans ?? new HashSet<HealthPlan>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
        public ICollection<HealthPlan> HealthPlans { get; set; }
    }
}
