namespace Healthcare.Api.Contracts.Responses
{
    public class HealthInsuranceResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<HealthPlanResponse> HealthPlans { get; set; }
    }
}
