using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class UpdateBloodTestDataBehaviour : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodTestData_Study_IdStudy",
                schema: "Healthcare",
                table: "BloodTestData");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodTestData_Study_IdStudy",
                schema: "Healthcare",
                table: "BloodTestData",
                column: "IdStudy",
                principalSchema: "Healthcare",
                principalTable: "Study",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BloodTestData_Study_IdStudy",
                schema: "Healthcare",
                table: "BloodTestData");

            migrationBuilder.AddForeignKey(
                name: "FK_BloodTestData_Study_IdStudy",
                schema: "Healthcare",
                table: "BloodTestData",
                column: "IdStudy",
                principalSchema: "Healthcare",
                principalTable: "Study",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
