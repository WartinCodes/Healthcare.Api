using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class CascadeDeleteInLabDetail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDetail_Study_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDetail_Study_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                column: "IdStudy",
                principalSchema: "Healthcare",
                principalTable: "Study",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_LaboratoryDetail_Study_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail");

            migrationBuilder.AddForeignKey(
                name: "FK_LaboratoryDetail_Study_IdStudy",
                schema: "Healthcare",
                table: "LaboratoryDetail",
                column: "IdStudy",
                principalSchema: "Healthcare",
                principalTable: "Study",
                principalColumn: "Id");
        }
    }
}
