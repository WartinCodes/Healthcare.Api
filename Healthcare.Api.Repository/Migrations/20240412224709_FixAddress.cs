using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class FixAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_IdAddress",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_IdAddress",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Healthcare",
                table: "Address");

            migrationBuilder.AddColumn<int>(
                name: "AddressId",
                schema: "Healthcare",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_AddressId",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "AddressId");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "AddressId",
                principalSchema: "Healthcare",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Address_AddressId",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropIndex(
                name: "IX_AspNetUsers_AddressId",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "AddressId",
                schema: "Healthcare",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "UserId",
                schema: "Healthcare",
                table: "Address",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_AspNetUsers_IdAddress",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "IdAddress",
                unique: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Address_IdAddress",
                schema: "Healthcare",
                table: "AspNetUsers",
                column: "IdAddress",
                principalSchema: "Healthcare",
                principalTable: "Address",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
