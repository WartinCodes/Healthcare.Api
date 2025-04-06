using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class StudySignedByDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SignedDoctorId",
                schema: "Healthcare",
                table: "Study",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Study_SignedDoctorId",
                schema: "Healthcare",
                table: "Study",
                column: "SignedDoctorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_Doctor_SignedDoctorId",
                schema: "Healthcare",
                table: "Study",
                column: "SignedDoctorId",
                principalSchema: "Healthcare",
                principalTable: "Doctor",
                principalColumn: "Id",
                onDelete: ReferentialAction.SetNull);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_Doctor_SignedDoctorId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.DropIndex(
                name: "IX_Study_SignedDoctorId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.DropColumn(
                name: "SignedDoctorId",
                schema: "Healthcare",
                table: "Study");
        }
    }
}
