﻿namespace Healthcare.Api.Contracts.Responses
{
    public class UserResponse
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string DNI { get; set; }
        public string Photo { get; set; }
        public string Email { get; set; }
        public DateTime BirthDate { get; set; }
        public AddressResponse Address { get; set; }
        public string PhoneNumber { get; set; }
    }
}
