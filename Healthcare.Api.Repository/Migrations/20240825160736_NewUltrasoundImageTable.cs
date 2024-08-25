using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class NewUltrasoundImageTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "UltrasoundImage",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    IdStudy = table.Column<int>(type: "int", nullable: false),
                    LocationS3 = table.Column<string>(type: "longtext", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UltrasoundImage", x => x.Id);
                    table.ForeignKey(
                        name: "FK_UltrasoundImage_Study_IdStudy",
                        column: x => x.IdStudy,
                        principalSchema: "Healthcare",
                        principalTable: "Study",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_UltrasoundImage_IdStudy",
                schema: "Healthcare",
                table: "UltrasoundImage",
                column: "IdStudy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UltrasoundImage",
                schema: "Healthcare");
        }
    }
}
