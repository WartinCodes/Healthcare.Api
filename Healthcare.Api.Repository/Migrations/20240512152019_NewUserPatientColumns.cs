using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class NewUserPatientColumns : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CUIL",
                schema: "Healthcare",
                table: "Patient");

            migrationBuilder.AddColumn<string>(
                name: "Observations",
                schema: "Healthcare",
                table: "Patient",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "AffiliationNumber",
                schema: "Healthcare",
                table: "Patient",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Died",
                schema: "Healthcare",
                table: "Patient",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "BloodType",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CUIL",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "CUIT",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Gender",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "MaritalStatus",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "PhoneNumber2",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "RhFactor",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "longtext",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CUIL",
                schema: "Healthcare",
                table: "Patient",
                type: "longtext",
                nullable: true);

            migrationBuilder.DropColumn(
                name: "AffiliationNumber",
                schema: "Healthcare",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Died",
                schema: "Healthcare",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "Observations",
                schema: "Healthcare",
                table: "Patient");

            migrationBuilder.DropColumn(
                name: "BloodType",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CUIL",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "CUIT",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "Gender",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "MaritalStatus",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "PhoneNumber2",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "RhFactor",
                schema: "Healthcare",
                table: "AspNetUsers");
        }
    }
}
