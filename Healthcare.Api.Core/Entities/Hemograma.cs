using System.ComponentModel;

namespace Healthcare.Api.Core.Entities
{
    public class Hemograma
    {
        public int Id { get; set; }
        public string Metodo { get; set; }
        [DisplayName("Globulos Rojos")]
        public string GlobulosRojos { get; set; }
        [DisplayName("Globulos Blancos")]
        public string GlobulosBlancos { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string VCM { get; set; }
        public string HCM { get; set; }
        public string CHCM { get; set; }
        [DisplayName("Neutrofilos Cayados")]
        public string NeutrofilosCayados { get; set; }
        [DisplayName("Neutrofilos Segmentados")]
        public string NeutrofilosSegmentados { get; set; }
        public string Eosinofilos { get; set; }
        public string Basofilos { get; set; }
        public string Linfocitos { get; set; }
        public string Monocitos { get; set; }
    }
}
