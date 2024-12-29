namespace Healthcare.Api.Contracts.Responses
{
    public class BloodTestDataResponse
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public virtual BloodTestResponse BloodTest { get; set; }
        public virtual StudyResponse Study { get; set; }
    }
}
