namespace Healthcare.Api.Contracts.Responses
{
    public class BloodTestResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceValue { get; set; }
        public UnitResponse Unit { get; set; }
    }
}
