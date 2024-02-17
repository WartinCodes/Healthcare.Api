namespace Healthcare.Api.Core.Entities
{
    public class Doctor
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public virtual User User { get; set; }
        public ICollection<Speciality> Specialities { get; set; }
    }
}
