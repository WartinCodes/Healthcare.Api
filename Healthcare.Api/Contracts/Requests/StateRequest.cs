namespace Healthcare.Api.Contracts.Requests
{
    public class StateRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public CountryRequest Country { get; set; }
    }
}
