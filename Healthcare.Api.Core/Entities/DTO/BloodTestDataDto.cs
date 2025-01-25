namespace Healthcare.Api.Core.Entities.DTO
{
    public class BloodTestDataDto
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public BloodTestDto BloodTest { get; set; }
    }
}
