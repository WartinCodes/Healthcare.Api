namespace Healthcare.Api.Core.Entities
{
    public class Patient
    {
        public int Id { get; set; }
        public int IdUser { get; set; }
        public virtual User User { get; set; }
        public string CUIL { get; set; }
    }
}