namespace Healthcare.Api.Contracts.Requests
{
    public class OtherLaboratoryDetailRequest
    {
        public string Name { get; set; }
        public string? Description { get; set; }
        public string? Value { get; set; }
    }
}
