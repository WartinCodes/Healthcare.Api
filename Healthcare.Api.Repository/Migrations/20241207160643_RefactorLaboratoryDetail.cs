using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class RefactorLaboratoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Unit",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    ShortName = table.Column<string>(type: "longtext", unicode: false, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Unit", x => x.Id);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodTest",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    ReferenceValue = table.Column<string>(type: "longtext", unicode: false, nullable: true),
                    IdUnit = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTest", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodTest_Unit_IdUnit",
                        column: x => x.IdUnit,
                        principalSchema: "Healthcare",
                        principalTable: "Unit",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "BloodTestData",
                schema: "Healthcare",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    Value = table.Column<string>(type: "longtext", unicode: false, nullable: false),
                    IdBloodTest = table.Column<int>(type: "int", nullable: false),
                    IdStudy = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BloodTestData", x => x.Id);
                    table.ForeignKey(
                        name: "FK_BloodTestData_BloodTest_IdBloodTest",
                        column: x => x.IdBloodTest,
                        principalSchema: "Healthcare",
                        principalTable: "BloodTest",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_BloodTestData_Study_IdStudy",
                        column: x => x.IdStudy,
                        principalSchema: "Healthcare",
                        principalTable: "Study",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_BloodTest_IdUnit",
                schema: "Healthcare",
                table: "BloodTest",
                column: "IdUnit");

            migrationBuilder.CreateIndex(
                name: "IX_BloodTestData_IdBloodTest",
                schema: "Healthcare",
                table: "BloodTestData",
                column: "IdBloodTest");

            migrationBuilder.CreateIndex(
                name: "IX_BloodTestData_IdStudy",
                schema: "Healthcare",
                table: "BloodTestData",
                column: "IdStudy");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "BloodTestData",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "BloodTest",
                schema: "Healthcare");

            migrationBuilder.DropTable(
                name: "Unit",
                schema: "Healthcare");
        }
    }
}
