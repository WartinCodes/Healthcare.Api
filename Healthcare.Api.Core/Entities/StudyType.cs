namespace Healthcare.Api.Core.Entities
{
    public class StudyType
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Study> Studies { get; set; }
    }
}
