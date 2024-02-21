using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Api.Core.Entities
{
    public class Country
    {
        public Country()
        {
            Name = String.Empty;
        }
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
