namespace Healthcare.Api.Contracts.Requests
{
    public class HealthPlanRequest
    {
        public string Name { get; set; }
        public HealthInsuranceRequest HealthInsuranceRequest { get; set; }
    }
}
