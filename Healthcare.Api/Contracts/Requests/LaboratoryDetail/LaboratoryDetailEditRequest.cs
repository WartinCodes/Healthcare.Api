namespace Healthcare.Api.Contracts.Requests.LaboratoryDetail
{
    public class LaboratoryDetailEditRequest
    {
        public int Id { get; set; }
        public LaboratoryDetailRequest LaboratoryDetail { get; set; }
    }
}
