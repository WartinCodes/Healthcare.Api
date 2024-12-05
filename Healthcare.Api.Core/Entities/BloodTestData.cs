namespace Healthcare.Api.Core.Entities
{
    public class BloodTestData
    {
        public int Id { get; set; }
        public string Value { get; set; }
        public int IdBloodTest { get; set; }
        public virtual BloodTest BloodTest{ get; set; }
        public int IdStudy { get; set; }
        public virtual Study Study { get; set; }
    }
}