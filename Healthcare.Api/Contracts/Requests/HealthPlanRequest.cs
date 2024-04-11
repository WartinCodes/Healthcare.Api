namespace Healthcare.Api.Contracts.Requests
{
    public class HealthPlanRequest
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HealthInsuranceRequest HealthInsurance { get; set; }
    }
}
