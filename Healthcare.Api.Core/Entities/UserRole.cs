namespace Healthcare.Api.Core.Entities
{
    public class UserRole
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public virtual User User { get; set; }
        public int IdRole { get; set; }
        public virtual Role Role { get; set; }
    }
}
