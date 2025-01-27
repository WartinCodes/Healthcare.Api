namespace Healthcare.Api.Contracts.Responses
{
    public class BloodTestDataStudyResponse
    {
        public StudyResponse Study { get; set; }
        public List<BloodTestDataResponse> BloodTestData { get; set; }
    }
}
