namespace Healthcare.Api.Contracts.Responses
{
    public class StateResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryResponse Country { get; set; }
    }
}
