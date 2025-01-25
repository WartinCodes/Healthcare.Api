namespace Healthcare.Api.Core.Entities.DTO
{
    public class BloodTestDataStudyDto
    {
        public StudyDto Study { get; set; }
        public List<BloodTestDataDto> BloodTestData { get; set; }
    }
}