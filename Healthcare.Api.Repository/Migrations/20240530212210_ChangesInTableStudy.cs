using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class ChangesInTableStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_Patient_PatientId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "Healthcare",
                table: "Study",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                @"
            UPDATE `Healthcare`.`Study` s
            JOIN `Healthcare`.`Patient` p ON s.PatientId = p.Id
            SET s.UserId = p.UserId
            WHERE p.UserId IS NOT NULL;
            ");

            migrationBuilder.Sql(
                @"
            UPDATE `Healthcare`.`Study` s
            LEFT JOIN `Healthcare`.`Patient` p ON s.PatientId = p.Id
            SET s.UserId = 1
            WHERE p.UserId IS NULL;
            ");

            migrationBuilder.DropColumn(
                name: "PatientId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.CreateIndex(
                name: "IX_Study_UserId",
                schema: "Healthcare",
                table: "Study",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_AspNetUsers_UserId",
                schema: "Healthcare",
                table: "Study",
                column: "UserId",
                principalSchema: "Healthcare",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_AspNetUsers_UserId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.DropIndex(
                name: "IX_Study_UserId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.AddColumn<int>(
                name: "PatientId",
                schema: "Healthcare",
                table: "Study",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.Sql(
                "UPDATE `Healthcare`.`Study` SET `PatientId` = `UserId`");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.CreateIndex(
                name: "IX_Study_PatientId",
                schema: "Healthcare",
                table: "Study",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_Patient_PatientId",
                schema: "Healthcare",
                table: "Study",
                column: "PatientId",
                principalSchema: "Healthcare",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
