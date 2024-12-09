namespace Healthcare.Api.Contracts.Requests
{
    public class BloodTestRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string ReferenceValue { get; set; }
        public UnitRequest Unit { get; set; }
    }
}
