namespace Healthcare.Api.Core.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Doctor> Doctors { get; set; }
    }
}
