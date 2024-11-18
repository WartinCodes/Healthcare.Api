using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class NewTableOtherLaboratoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "OtherLaboratoryDetail",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdLaboratoryDetail = table.Column<int>(type: "int", nullable: false),
                    Name = table.Column<string>(type: "longtext", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: true),
                    Value = table.Column<string>(type: "longtext", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OtherLaboratoryDetail", x => x.Id);
                    table.ForeignKey(
                        name: "FK_OtherLaboratoryDetail_LaboratoryDetail_IdLaboratoryDetail",
                        column: x => x.IdLaboratoryDetail,
                        principalSchema: "Healthcare",
                        principalTable: "LaboratoryDetail",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_OtherLaboratoryDetail_IdLaboratoryDetail",
                schema: "Healthcare",
                table: "OtherLaboratoryDetail",
                column: "IdLaboratoryDetail");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OtherLaboratoryDetail",
                schema: "Healthcare");
        }
    }
}
