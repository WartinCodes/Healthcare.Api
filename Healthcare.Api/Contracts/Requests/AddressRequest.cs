namespace Healthcare.Api.Contracts.Requests
{
    public class AddressRequest
    {
        public int Id { get; set; }
        public string? Street { get; set; }
        public string? Number { get; set; }
        public string? Description { get; set; }
        public string? PhoneNumber { get; set; }
        public CityRequest City { get; set; }
    }
}
