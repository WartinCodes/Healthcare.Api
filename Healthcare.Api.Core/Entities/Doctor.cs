namespace Healthcare.Api.Core.Entities
{
    public class Doctor
    {
        public Doctor()
        {
            
        }

        public Doctor(int userId, ICollection<DoctorSpeciality> doctorSpecialities, ICollection<HealthPlan> healthPlans)
        {
            UserId = userId;
            DoctorSpecialities = doctorSpecialities ?? new HashSet<DoctorSpeciality>();
            HealthPlans = healthPlans ?? new HashSet<HealthPlan>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User User { get; set; }
        public string Matricula { get; set; }
        public int IdAddress { get; set; }
        public virtual Address Address { get; set; }
        public ICollection<DoctorSpeciality> DoctorSpecialities { get; set; }
        public ICollection<HealthPlan> HealthPlans { get; set; }
    }
}
