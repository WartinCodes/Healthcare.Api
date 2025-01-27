namespace Healthcare.Api.Core.Entities.DTO
{
    public class BloodTestDto
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string ParsedName { get; set; }
        public string ReferenceValue { get; set; }
        public UnitDto Unit { get; set; }
    }
}
