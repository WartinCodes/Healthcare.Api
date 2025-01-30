namespace Healthcare.Api.Contracts.Requests
{
    public class BloodTestRequest
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string ParsedName { get; set; }
        public string? ReferenceValue { get; set; }
        public int IdUnit { get; set; }
    }
}
