using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Api.Core.Entities
{
    public class Country
    {
        public Country()
        {
            Name = String.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public virtual ICollection<State> States { get; set; }
    }
}
