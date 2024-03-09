namespace Healthcare.Api.Contracts.Responses
{
    public class AddressResponse
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
    }
}
