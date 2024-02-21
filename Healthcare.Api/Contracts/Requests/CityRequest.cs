namespace Healthcare.Api.Contracts.Requests
{
    public class CityRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public StateRequest State { get; set; }
    }
}
