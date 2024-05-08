namespace Healthcare.Api.Contracts.Responses
{
    public class HealthPlanResponse
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public HealthInsuranceByHealthPlanResponse HealthInsurance{ get; set; }
    }
}
