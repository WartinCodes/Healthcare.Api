using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class LaboratoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LaboratoryDetail",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Metodo = table.Column<string>(type: "longtext", nullable: false),
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
                    Monocitos = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LaboratoryDetail", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "LaboratoryDetail",
                schema: "Healthcare");
        }
    }
}
