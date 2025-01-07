namespace Healthcare.Api.Contracts.Requests.LaboratoryDetail
{
    public class BloodTestDataCreateRequest
    {
        public int UserId { get; set; }
        public string Note { get; set; }
        public DateTime Date { get; set; }
        public List<BloodTestDataRequest> BloodTestDatas { get; set; }
    }
}
