using System.ComponentModel;

namespace Healthcare.Api.Contracts.Requests
{
    public class LaboratoryDetailRequest
    {
        public int IdStudy { get; set; }

        #region Hemograma
        [DisplayName("Globulos Rojos")]
        public string GlobulosRojos { get; set; }

        [DisplayName("Globulos Blancos")]
        public string GlobulosBlancos { get; set; }

        public string Hemoglobina { get; set; }

        public string Hematocrito { get; set; }

        [DisplayName("V.C.M.")]
        public string VCM { get; set; }
        
        [DisplayName("C.H.C.M.")]
        public string CHCM { get; set; }

        [DisplayName("H.C.M.")]
        public string HCM { get; set; }

        [DisplayName("Neutrofilos Cayados")]
        public string NeutrofilosCayados { get; set; }

        [DisplayName("Neutrofilos Segmentados")]
        public string NeutrofilosSegmentados { get; set; }

        public string Eosinofilos { get; set; }

        public string Basofilos { get; set; }

        public string Linfocitos { get; set; }

        public string Monocitos { get; set; }
        #endregion

        #region Eritrosedimentacion
        [DisplayName("ERITROSEDIMENTACION 1° HORA")]
        public string Eritrosedimentacion1 { get; set; }

        [DisplayName("ERITROSEDIMENTACION  2° HORA")]
        public string Eritrosedimentacion2 { get; set; }
        #endregion

        public string Plaquetas { get; set; }

        public string Glucemia { get; set; }

        public string Uremia { get; set; }

        public string Creatininemia { get; set; }

        [DisplayName("Creatinfosfoquinasa")]
        public string Creatinfosfoquinasa { get; set; }

        [DisplayName("Colesterol Total")]
        public string ColesterolTotal { get; set; }

        [DisplayName("Colesterol Hdl")]
        public string ColesterolHdl { get; set; }

        [DisplayName("Colesterol Ldl")]
        public string ColesterolLdl { get; set; }

        public string Trigliceridos { get; set; }

        public string Uricemia { get; set; }

        #region Coagulograma
        [DisplayName("Tiempo de Coagulacion")]
        public string TiempoCoagulacion { get; set; }

        [DisplayName("Tiempo de Sangria")]
        public string TiempoSangria { get; set; }

        [DisplayName("Tiempo de Protrombina")]
        public string TiempoProtrombina { get; set; }

        [DisplayName("Tiempo de Tromboplastina")]
        public string TiempoTromboplastina { get; set; }
        #endregion

        [DisplayName("Bilirrubina Directa")]
        public string BilirrubinaDirecta { get; set; }

        [DisplayName("Bilirrubina Indirecta")]
        public string BilirrubinaIndirecta { get; set; }

        [DisplayName("Bilirrubina Total")]
        public string BilirrubinaTotal { get; set; }

        public string Amilasemia { get; set; }

        [DisplayName("Glutamil Transpeptidasa")]
        public string GlutamilTranspeptidasa { get; set; }

        [DisplayName("5- Nucleotidasa")]
        public string Nucleotidasa { get; set; }

        [DisplayName("Transaminasa Glutamico Oxalac")]
        public string TransaminasaGlutamicoOxalac { get; set; }

        [DisplayName("Transaminasa Glutamico Piruvic")]
        public string TransaminasaGlutamicoPiruvic { get; set; }

        [DisplayName("Fosfatasa Alcalina")]
        public string FosfatasaAlcalina { get; set; }

        [DisplayName("Tirotrofina Plamatica")]
        public string TirotrofinaPlamatica { get; set; }

        public string Sodio { get; set; }

        public string Potasio { get; set; }

        [DisplayName("Cloro Plasmatico")]
        public string CloroPlasmatico { get; set; }

        [DisplayName("Calcemia Total")]
        public string CalcemiaTotal { get; set; }

        [DisplayName("Magnesio de Sangre")]
        public string MagnesioSangre { get; set; }

        [DisplayName("Proteinas Totales")]
        public string ProteinasTotales { get; set; }

        public string Albumina { get; set; }

        public string Pseudocolinesterasa { get; set; }

        public string Ferremia { get; set; }

        public string Transferrina { get; set; }

        public string Ferritina { get; set; }

        [DisplayName("Tiroxina Efectiva - T4 libre")]
        public string TiroxinaEfectiva { get; set; }

        [DisplayName("Tiroxina Total (T4)")]
        public string TiroxinaTotal { get; set; }

        [DisplayName("Hemoglobina Glicosilada")]
        public string HemoglobinaGlicosilada { get; set; }

        [DisplayName("Antigeno Prostatico Especifico")]
        public string AntigenoProstaticoEspecifico { get; set; }

        [DisplayName("Vitamina D3")]
        public string VitaminaD3 { get; set; }

        [DisplayName("Antigeno Prostatico Especifico Libre")]
        public string AntigenoProstáticoEspecíficoLibre { get; set; }

        [DisplayName("Cociente Albumina/Creatinina en Orina")]
        public string CocienteAlbumina { get; set; }

    }
}
