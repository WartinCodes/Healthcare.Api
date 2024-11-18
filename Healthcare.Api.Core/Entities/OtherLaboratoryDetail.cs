namespace Healthcare.Api.Core.Entities
{
    public class OtherLaboratoryDetail
    {
        public int Id { get; set; }
        public int IdLaboratoryDetail { get; set; }
        public LaboratoryDetail LaboratoryDetail { get; set; }
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
    }
}
