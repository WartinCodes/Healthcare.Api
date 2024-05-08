using System.ComponentModel;

namespace Healthcare.Api.Contracts.Requests
{
    public class LaboratoryDetailRequest
    {
        public int IdStudy { get; set; }
        [DisplayName("Globulos Rojos")]
        public string GlobulosRojos { get; set; }
        [DisplayName("Globulos Blancos")]
        public string GlobulosBlancos { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string VCM { get; set; }
        public string CHCM { get; set; }
        public string HCM { get; set; }
        [DisplayName("Neutrofilos Cayados")]
        public string NeutrofilosCayados { get; set; }
        [DisplayName("Neutrofilos Segmentados")]
        public string NeutrofilosSegmentados { get; set; }
        public string Eosinofilos { get; set; }
        public string Basofilos { get; set; }
        public string Linfocitos { get; set; }
        public string Monocitos { get; set; }
        [DisplayName("ERITROSEDIMENTACION 1° HORA")]
        public string Eritrosedimentacion1 { get; set; }
        [DisplayName("ERITROSEDIMENTACION  2° HORA")]
        public string Eritrosedimentacion2 { get; set; }
        public string Plaquetas { get; set; }
        public string Glucemia { get; set; }
        public string Uremia { get; set; }
        public string Creatininemia { get; set; }
        [DisplayName("Colesterol Total")]
        public string ColesterolTotal { get; set; }
        [DisplayName("Colesterol Hdl")]
        public string ColesterolHdl { get; set; }
        public string Trigliceridos { get; set; }
        public string Uricemia { get; set; }
        [DisplayName("Bilirrubina Directa")]
        public string BilirrubinaDirecta { get; set; }
        [DisplayName("Bilirrubina Indirecta")]
        public string BilirrubinaIndirecta { get; set; }
        [DisplayName("Bilirrubina Total")]
        public string BilirrubinaTotal { get; set; }
        [DisplayName("Transaminasa Glutamico Oxalac")]
        public string TransaminasaGlutamicoOxalac { get; set; }
        [DisplayName("Transaminasa Glutamico Piruvic")]
        public string TransaminasaGlutamicoPiruvic { get; set; }
        [DisplayName("Fosfatasa Alcalina")]
        public string FosfatasaAlcalina { get; set; }
        [DisplayName("Tirotrofina Plamatica")]
        public string TirotrofinaPlamatica { get; set; }
    }
}
