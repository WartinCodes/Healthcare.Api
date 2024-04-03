namespace Healthcare.Api.Core.Entities
{
    public class LaboratoryDetail
    {
        public int Id { get; set; }
        public int IdStudy { get; set; }
        public virtual Study Study { get; set; }
        public string GlobulosRojos { get; set; }
        public string GlobulosBlancos { get; set; }
        public string Hemoglobina { get; set; }
        public string Hematocrito { get; set; }
        public string VCM { get; set; }
        public string HCM { get; set; }
        public string CHCM { get; set; }
        public string NeutrofilosCayados { get; set; }
        public string NeutrofilosSegmentados { get; set; }
        public string Eosinofilos { get; set; }
        public string Basofilos { get; set; }
        public string Linfocitos { get; set; }
        public string Monocitos { get; set; }
        public string Eritrosedimentacion1 { get; set; }
        public string Eritrosedimentacion2 { get; set; }
        public string Plaquetas { get; set; }
        public string Glucemia { get; set; }
        public string Uremia { get; set; }
        public string Creatininemia { get; set; }
        public string ColesterolTotal { get; set; }
        public string ColesterolHdl { get; set; }
        public string Trigliceridos { get; set; }
        public string Uricemia { get; set; }
        public string BilirrubinaDirecta { get; set; }
        public string BilirrubinaIndirecta { get; set; }
        public string BilirrubinaTotal { get; set; }
        public string TransaminasaGlutamicoOxalac { get; set; }
        public string TransaminasaGlutamicoPiruvic { get; set; }
        public string FosfatasaAlcalina { get; set; }
        public string TirotrofinaPlamatica { get; set; }
    }
}
