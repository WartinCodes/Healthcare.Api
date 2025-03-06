using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class NewTableNutritionData : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaboratoryDetail",
                schema: "Healthcare");

            migrationBuilder.CreateTable(
                name: "NutritionData",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Weight = table.Column<double>(type: "double", nullable: false),
                    Difference = table.Column<double>(type: "double", nullable: false),
                    FatPercentage = table.Column<double>(type: "double", nullable: false),
                    MusclePercentage = table.Column<double>(type: "double", nullable: false),
                    VisceralFat = table.Column<double>(type: "double", nullable: false),
                    IMC = table.Column<double>(type: "double", nullable: false),
                    TargetWeight = table.Column<double>(type: "double", nullable: false),
                    Observations = table.Column<string>(type: "varchar(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_NutritionData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_NutritionData_Patient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Healthcare",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionData_PatientId",
                schema: "Healthcare",
                table: "NutritionData",
                column: "PatientId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "NutritionData",
                schema: "Healthcare");

            migrationBuilder.CreateTable(
                name: "LaboratoryDetail",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdStudy = table.Column<int>(type: "int", nullable: false),
                    Albumina = table.Column<string>(type: "longtext", nullable: true),
                    Amilasemia = table.Column<string>(type: "longtext", nullable: true),
                    AntigenoProstaticoEspecifico = table.Column<string>(type: "longtext", nullable: true),
                    Basofilos = table.Column<string>(type: "longtext", nullable: true),
                    BilirrubinaDirecta = table.Column<string>(type: "longtext", nullable: true),
                    BilirrubinaIndirecta = table.Column<string>(type: "longtext", nullable: true),
                    BilirrubinaTotal = table.Column<string>(type: "longtext", nullable: true),
                    CHCM = table.Column<string>(type: "longtext", nullable: true),
                    CalcemiaTotal = table.Column<string>(type: "longtext", nullable: true),
                    CloroPlasmatico = table.Column<string>(type: "longtext", nullable: true),
                    CocienteAlbumina = table.Column<string>(type: "longtext", nullable: true),
                    ColesterolHdl = table.Column<string>(type: "longtext", nullable: true),
                    ColesterolLdl = table.Column<string>(type: "longtext", nullable: true),
                    ColesterolTotal = table.Column<string>(type: "longtext", nullable: true),
                    Creatinfosfoquinasa = table.Column<string>(type: "longtext", nullable: true),
                    Creatininemia = table.Column<string>(type: "longtext", nullable: true),
                    Eosinofilos = table.Column<string>(type: "longtext", nullable: true),
                    Eritrosedimentacion1 = table.Column<string>(type: "longtext", nullable: true),
                    Eritrosedimentacion2 = table.Column<string>(type: "longtext", nullable: true),
                    Ferremia = table.Column<string>(type: "longtext", nullable: true),
                    Ferritina = table.Column<string>(type: "longtext", nullable: true),
                    FosfatasaAlcalina = table.Column<string>(type: "longtext", nullable: true),
                    GlobulosBlancos = table.Column<string>(type: "longtext", nullable: true),
                    GlobulosRojos = table.Column<string>(type: "longtext", nullable: true),
                    Glucemia = table.Column<string>(type: "longtext", nullable: true),
                    GlutamilTranspeptidasa = table.Column<string>(type: "longtext", nullable: true),
                    HCM = table.Column<string>(type: "longtext", nullable: true),
                    Hematocrito = table.Column<string>(type: "longtext", nullable: true),
                    Hemoglobina = table.Column<string>(type: "longtext", nullable: true),
                    HemoglobinaGlicosilada = table.Column<string>(type: "longtext", nullable: true),
                    Linfocitos = table.Column<string>(type: "longtext", nullable: true),
                    MagnesioSangre = table.Column<string>(type: "longtext", nullable: true),
                    Monocitos = table.Column<string>(type: "longtext", nullable: true),
                    NeutrofilosCayados = table.Column<string>(type: "longtext", nullable: true),
                    NeutrofilosSegmentados = table.Column<string>(type: "longtext", nullable: true),
                    Nucleotidasa = table.Column<string>(type: "longtext", nullable: true),
                    Plaquetas = table.Column<string>(type: "longtext", nullable: true),
                    Potasio = table.Column<string>(type: "longtext", nullable: true),
                    ProteinasTotales = table.Column<string>(type: "longtext", nullable: true),
                    PsaLibre = table.Column<string>(type: "longtext", nullable: true),
                    Pseudocolinesterasa = table.Column<string>(type: "longtext", nullable: true),
                    RelacionPsaLibre = table.Column<string>(type: "longtext", nullable: true),
                    SaturacionTransferrina = table.Column<string>(type: "longtext", nullable: true),
                    Sodio = table.Column<string>(type: "longtext", nullable: true),
                    TiempoCoagulacion = table.Column<string>(type: "longtext", nullable: true),
                    TiempoProtrombina = table.Column<string>(type: "longtext", nullable: true),
                    TiempoSangria = table.Column<string>(type: "longtext", nullable: true),
                    TiempoTromboplastina = table.Column<string>(type: "longtext", nullable: true),
                    TirotrofinaPlamatica = table.Column<string>(type: "longtext", nullable: true),
                    TiroxinaEfectiva = table.Column<string>(type: "longtext", nullable: true),
                    TiroxinaTotal = table.Column<string>(type: "longtext", nullable: true),
                    TransaminasaGlutamicoOxalac = table.Column<string>(type: "longtext", nullable: true),
                    TransaminasaGlutamicoPiruvic = table.Column<string>(type: "longtext", nullable: true),
                    Transferrina = table.Column<string>(type: "longtext", nullable: true),
                    Trigliceridos = table.Column<string>(type: "longtext", nullable: true),
                    Uremia = table.Column<string>(type: "longtext", nullable: true),
                    Uricemia = table.Column<string>(type: "longtext", nullable: true),
                    VCM = table.Column<string>(type: "longtext", nullable: true),
                    VitaminaD3 = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_LaboratoryDetail_Study_IdStudy",
                        column: x => x.IdStudy,
                        principalSchema: "Healthcare",
                        principalTable: "Study",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDetail_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                column: "IdStudy");
        }
    }
}
