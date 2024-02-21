using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Api.Core.Entities
{
    public class Address
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Street { get; set; }
        public string Number { get; set; }
        public string Description { get; set; }
        public string PhoneNumber { get; set; }
        public int CityId { get; set; }
        public virtual City City { get; set; }
    }
}
