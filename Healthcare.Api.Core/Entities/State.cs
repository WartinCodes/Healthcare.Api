using System.ComponentModel.DataAnnotations.Schema;

namespace Healthcare.Api.Core.Entities
{
    public class State
    {
        public State()
        {
            Name = String.Empty;
        }
        public int Id { get; set; }
        public string Name { get; set; }
        public int IdCountry { get; set; }
        public virtual Country Country { get; set; }
        public virtual IEnumerable<City> Cities { get; set; }
    }
}
