﻿namespace Healthcare.Api.Contracts.Requests.LaboratoryDetail
{
    public class LaboratoryDetailCreateRequest
    {
        public int UserId { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public LaboratoryDetailRequest LaboratoryDetail { get; set; }
    }
}
