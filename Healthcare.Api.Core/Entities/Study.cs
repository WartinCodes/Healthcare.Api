namespace Healthcare.Api.Core.Entities
{
    public class Study
    {
        public int Id { get; set; }
        public string LocationS3 { get; set; }

        public int UserId { get; set; }
        public User User { get; set; }

        public int StudyTypeId { get; set; }
        public StudyType StudyType { get; set; }

        public DateTime Date { get; set; }
        public string Note { get; set; }
    }
}