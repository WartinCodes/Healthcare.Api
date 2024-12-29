namespace Healthcare.Api.Contracts.Responses
{
    public class BloodTestResponse
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string ParsedName { get; set; }
        public string ReferenceValue { get; set; }
        public UnitResponse Unit { get; set; }
    }
}
