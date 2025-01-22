using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class AddNewColumnToStudy : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "Created",
                schema: "Healthcare",
                table: "Study",
                type: "datetime(6)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Created",
                schema: "Healthcare",
                table: "Study");
        }
    }
}
