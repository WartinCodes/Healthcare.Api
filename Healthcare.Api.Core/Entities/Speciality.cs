namespace Healthcare.Api.Core.Entities
{
    public class Speciality
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdRole { get; set; }
        public virtual Role Role { get; set; }
    }
}
