using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class FixEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Study_StudyType_StudyTypeId1",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.DropIndex(
                name: "IX_Study_StudyTypeId1",
                schema: "Healthcare",
                table: "Study");

            migrationBuilder.DropColumn(
                name: "StudyTypeId1",
                schema: "Healthcare",
                table: "Study");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "StudyTypeId1",
                schema: "Healthcare",
                table: "Study",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Study_StudyTypeId1",
                schema: "Healthcare",
                table: "Study",
                column: "StudyTypeId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Study_StudyType_StudyTypeId1",
                schema: "Healthcare",
                table: "Study",
                column: "StudyTypeId1",
                principalSchema: "Healthcare",
                principalTable: "StudyType",
                principalColumn: "Id");
        }
    }
}
