﻿namespace Healthcare.Api.Contracts.Responses
{
    public class PatientSignatureResponse
    {
        public string FullName { get; set; }
        public string DNI { get; set; }
        public IEnumerable<string> HealthInsurance { get; set; }
        public string AffiliationNumber { get; set; }
        public string BirthDate { get; set; }
    }
}