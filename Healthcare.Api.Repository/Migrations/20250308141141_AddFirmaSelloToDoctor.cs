using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class AddFirmaSelloToDoctor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Firma",
                schema: "Healthcare",
                table: "Doctor",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Sello",
                schema: "Healthcare",
                table: "Doctor",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Firma",
                schema: "Healthcare",
                table: "Doctor");

            migrationBuilder.DropColumn(
                name: "Sello",
                schema: "Healthcare",
                table: "Doctor");
        }
    }
}
