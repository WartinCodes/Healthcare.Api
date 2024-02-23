using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Api.Core.Entities
{
    public class City
    {
        public City()
        {
            Name = String.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdState { get; set; }
        public virtual State State { get; set; }
        public virtual IEnumerable<Address> Addresses { get; set; }
    }
}