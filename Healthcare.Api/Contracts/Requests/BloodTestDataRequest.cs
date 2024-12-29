namespace Healthcare.Api.Contracts.Requests
{
    public class BloodTestDataRequest
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int IdBloodTest { get; set; }
    }
}
