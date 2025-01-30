namespace Healthcare.Api.Core.Entities
{
    public class BloodTest
    {
        public int Id { get; set; }
        public string OriginalName { get; set; }
        public string ParsedName { get; set; }
        public string? ReferenceValue { get; set; }
        public int IdUnit { get; set; }
        public virtual Unit Unit { get; set; }
    }
}