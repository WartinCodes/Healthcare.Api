using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class AddColumnInLaboratoryDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_LaboratoryDetail_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                column: "IdStudy");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDetail_Study_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                column: "IdStudy",
                principalSchema: "Healthcare",
                principalTable: "Study",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDetail_Study_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropIndex(
                name: "IX_LaboratoryDetail_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.DropColumn(
                name: "IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail");
        }
    }
}
