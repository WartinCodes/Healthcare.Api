using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class NewStudyParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "VCM",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Uricemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Uremia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Trigliceridos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "TransaminasaGlutamicoPiruvic",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "TransaminasaGlutamicoOxalac",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "TirotrofinaPlamatica",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Plaquetas",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "NeutrofilosSegmentados",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "NeutrofilosCayados",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Monocitos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Linfocitos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Hemoglobina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Hematocrito",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "HCM",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Glucemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "GlobulosRojos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "GlobulosBlancos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "FosfatasaAlcalina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Eritrosedimentacion2",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Eritrosedimentacion1",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Eosinofilos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Creatininemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "ColesterolTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "ColesterolHdl",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "CHCM",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "BilirrubinaTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "BilirrubinaIndirecta",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "BilirrubinaDirecta",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AlterColumn<string>(
                name: "Basofilos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "longtext");

            migrationBuilder.AddColumn<string>(
                name: "Albumina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Amilasemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AntigenoProstaticoEspecifico",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CalcemiaTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CloroPlasmatico",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CocienteAlbumina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ColesterolLdl",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Creatinfosfoquinasa",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ferremia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Ferritina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "GlutamilTranspeptidasa",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "HemoglobinaGlicosilada",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MagnesioSangre",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Nucleotidasa",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Potasio",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ProteinasTotales",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PsaLibre",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pseudocolinesterasa",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RelacionPsaLibre",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SaturacionTransferrina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sodio",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiempoCoagulacion",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiempoProtrombina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiempoSangria",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiempoTromboplastina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiroxinaEfectiva",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "TiroxinaTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Transferrina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "VitaminaD3",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Albumina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Amilasemia",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "AntigenoProstaticoEspecifico",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "CalcemiaTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "CloroPlasmatico",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "CocienteAlbumina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "ColesterolLdl",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Creatinfosfoquinasa",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Ferremia",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Ferritina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "GlutamilTranspeptidasa",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "HemoglobinaGlicosilada",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "MagnesioSangre",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Nucleotidasa",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Potasio",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "ProteinasTotales",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "PsaLibre",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Pseudocolinesterasa",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "RelacionPsaLibre",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "SaturacionTransferrina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Sodio",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "TiempoCoagulacion",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "TiempoProtrombina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "TiempoSangria",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "TiempoTromboplastina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "TiroxinaEfectiva",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "TiroxinaTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "Transferrina",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "VitaminaD3",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.AlterColumn<string>(
                name: "VCM",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Uricemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Uremia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Trigliceridos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransaminasaGlutamicoPiruvic",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TransaminasaGlutamicoOxalac",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "TirotrofinaPlamatica",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Plaquetas",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NeutrofilosSegmentados",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "NeutrofilosCayados",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Monocitos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Linfocitos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hemoglobina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Hematocrito",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "HCM",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Glucemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GlobulosRojos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "GlobulosBlancos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "FosfatasaAlcalina",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Eritrosedimentacion2",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Eritrosedimentacion1",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Eosinofilos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Creatininemia",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColesterolTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ColesterolHdl",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "CHCM",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BilirrubinaTotal",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BilirrubinaIndirecta",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "BilirrubinaDirecta",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Basofilos",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "longtext",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "longtext",
                oldNullable: true);
        }
    }
}
