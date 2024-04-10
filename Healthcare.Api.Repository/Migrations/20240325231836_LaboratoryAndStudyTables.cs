using System;
using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class LaboratoryAndStudyTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Hemograma",
                schema: "Healthcare");

            migrationBuilder.CreateTable(
                name: "LaboratoryDetail",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    GlobulosRojos = table.Column<string>(type: "longtext", nullable: false),
                    GlobulosBlancos = table.Column<string>(type: "longtext", nullable: false),
                    Hemoglobina = table.Column<string>(type: "longtext", nullable: false),
                    Hematocrito = table.Column<string>(type: "longtext", nullable: false),
                    VCM = table.Column<string>(type: "longtext", nullable: false),
                    HCM = table.Column<string>(type: "longtext", nullable: false),
                    CHCM = table.Column<string>(type: "longtext", nullable: false),
                    NeutrofilosCayados = table.Column<string>(type: "longtext", nullable: false),
                    NeutrofilosSegmentados = table.Column<string>(type: "longtext", nullable: false),
                    Eosinofilos = table.Column<string>(type: "longtext", nullable: false),
                    Basofilos = table.Column<string>(type: "longtext", nullable: false),
                    Linfocitos = table.Column<string>(type: "longtext", nullable: false),
                    Monocitos = table.Column<string>(type: "longtext", nullable: false),
                    Eritrosedimentacion1 = table.Column<string>(type: "longtext", nullable: false),
                    Eritrosedimentacion2 = table.Column<string>(type: "longtext", nullable: false),
                    Plaquetas = table.Column<string>(type: "longtext", nullable: false),
                    Glucemia = table.Column<string>(type: "longtext", nullable: false),
                    Uremia = table.Column<string>(type: "longtext", nullable: false),
                    Creatininemia = table.Column<string>(type: "longtext", nullable: false),
                    ColesterolTotal = table.Column<string>(type: "longtext", nullable: false),
                    ColesterolHdl = table.Column<string>(type: "longtext", nullable: false),
                    Trigliceridos = table.Column<string>(type: "longtext", nullable: false),
                    Uricemia = table.Column<string>(type: "longtext", nullable: false),
                    BilirrubinaDirecta = table.Column<string>(type: "longtext", nullable: false),
                    BilirrubinaIndirecta = table.Column<string>(type: "longtext", nullable: false),
                    BilirrubinaTotal = table.Column<string>(type: "longtext", nullable: false),
                    TransaminasaGlutamicoOxalac = table.Column<string>(type: "longtext", nullable: false),
                    TransaminasaGlutamicoPiruvic = table.Column<string>(type: "longtext", nullable: false),
                    FosfatasaAlcalina = table.Column<string>(type: "longtext", nullable: false),
                    TirotrofinaPlamatica = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryDetail", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "StudyType",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StudyType", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Study",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    LocationS3 = table.Column<string>(type: "longtext", nullable: false),
                    PatientId = table.Column<int>(type: "int", nullable: false),
                    StudyTypeId = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    Note = table.Column<string>(type: "longtext", nullable: true),
                    StudyTypeId1 = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Study", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Study_Patient_PatientId",
                        column: x => x.PatientId,
                        principalSchema: "Healthcare",
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Study_StudyType_StudyTypeId",
                        column: x => x.StudyTypeId,
                        principalSchema: "Healthcare",
                        principalTable: "StudyType",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Study_StudyType_StudyTypeId1",
                        column: x => x.StudyTypeId1,
                        principalSchema: "Healthcare",
                        principalTable: "StudyType",
                        principalColumn: "Id");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Study_PatientId",
                schema: "Healthcare",
                table: "Study",
                column: "PatientId");

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyTypeId",
                schema: "Healthcare",
                table: "Study",
                column: "StudyTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyTypeId1",
                schema: "Healthcare",
                table: "Study",
                column: "StudyTypeId1");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaboratoryDetail",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Study",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "StudyType",
                schema: "Healthcare");

            migrationBuilder.CreateTable(
                name: "Hemograma",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Basofilos = table.Column<string>(type: "longtext", nullable: false),
                    CHCM = table.Column<string>(type: "longtext", nullable: false),
                    Eosinofilos = table.Column<string>(type: "longtext", nullable: false),
                    GlobulosBlancos = table.Column<string>(type: "longtext", nullable: false),
                    GlobulosRojos = table.Column<string>(type: "longtext", nullable: false),
                    HCM = table.Column<string>(type: "longtext", nullable: false),
                    Hematocrito = table.Column<string>(type: "longtext", nullable: false),
                    Hemoglobina = table.Column<string>(type: "longtext", nullable: false),
                    Linfocitos = table.Column<string>(type: "longtext", nullable: false),
                    Metodo = table.Column<string>(type: "longtext", nullable: false),
                    Monocitos = table.Column<string>(type: "longtext", nullable: false),
                    NeutrofilosCayados = table.Column<string>(type: "longtext", nullable: false),
                    NeutrofilosSegmentados = table.Column<string>(type: "longtext", nullable: false),
                    VCM = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Hemograma", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }
    }
}
