namespace Healthcare.Api.Core.Entities
{
    public class Support
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public string Status { get; set; }
        public string Priority { get; set; }
        public string Module { get; set; }
        public string Description { get; set; }
        public DateTime ReportDate { get; set; }
        public DateTime? ResolutionDate { get; set; }
    }
}
