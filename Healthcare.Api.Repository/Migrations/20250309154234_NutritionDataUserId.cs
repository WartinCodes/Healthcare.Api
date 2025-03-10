using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class NutritionDataUserId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionData_Patient_PatientId",
                schema: "Healthcare",
                table: "NutritionData");

            migrationBuilder.Sql(
                @"ALTER TABLE `Healthcare`.`NutritionData` 
                CHANGE COLUMN `PatientId` `UserId` INT NOT NULL;");

            migrationBuilder.Sql(
                @"UPDATE `Healthcare`.`NutritionData` ND
                INNER JOIN `Healthcare`.`Patient` P ON ND.UserId = P.Id
                SET ND.UserId = P.UserId;");

            migrationBuilder.DropIndex(
                name: "IX_NutritionData_PatientId",
                schema: "Healthcare",
                table: "NutritionData");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionData_UserId",
                schema: "Healthcare",
                table: "NutritionData",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionData_AspNetUsers_UserId",
                schema: "Healthcare",
                table: "NutritionData",
                column: "UserId",
                principalSchema: "Healthcare",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_NutritionData_AspNetUsers_UserId",
                schema: "Healthcare",
                table: "NutritionData");

            migrationBuilder.DropIndex(
                name: "IX_NutritionData_UserId",
                schema: "Healthcare",
                table: "NutritionData");

            // Renombramos la columna de vuelta a PatientId usando SQL
            migrationBuilder.Sql(
                @"ALTER TABLE `Healthcare`.`NutritionData` 
                CHANGE COLUMN `UserId` `PatientId` INT NOT NULL;");

            migrationBuilder.CreateIndex(
                name: "IX_NutritionData_PatientId",
                schema: "Healthcare",
                table: "NutritionData",
                column: "PatientId");

            migrationBuilder.AddForeignKey(
                name: "FK_NutritionData_Patient_PatientId",
                schema: "Healthcare",
                table: "NutritionData",
                column: "PatientId",
                principalSchema: "Healthcare",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

    }
}
