namespace Healthcare.Api.Core.Entities
{
    public class UltrasoundImage
    {
        public int Id { get; set; }
        public int IdStudy { get; set; }
        public Study Study { get; set; }
        public string LocationS3 { get; set; }
    }
}
