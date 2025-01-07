using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Healthcare.Api.Repository.Migrations
{
    public partial class RenameBloodTestParameters : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                            name: "ParsedName",
                            schema: "Healthcare",
                            table: "BloodTest",
                            type: "longtext",
                            unicode: false,
                            nullable: true);

            // 2. Copiar valores de Name a ParsedName
            migrationBuilder.Sql(
                @"UPDATE `Healthcare`.`BloodTest` 
                  SET `ParsedName` = `Name`");

            // 3. Crear nueva columna OriginalName
            migrationBuilder.AddColumn<string>(
                name: "OriginalName",
                schema: "Healthcare",
                table: "BloodTest",
                type: "longtext",
                unicode: false,
                nullable: true);

            // 4. Copiar valores de Name a OriginalName
            migrationBuilder.Sql(
                @"UPDATE `Healthcare`.`BloodTest` 
                  SET `OriginalName` = `Name`");

            // 5. Eliminar columna Name
            migrationBuilder.DropColumn(
                name: "Name",
                schema: "Healthcare",
                table: "BloodTest");

            // 6. Hacer que las nuevas columnas no permitan valores nulos
            migrationBuilder.AlterColumn<string>(
                name: "ParsedName",
                schema: "Healthcare",
                table: "BloodTest",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "OriginalName",
                schema: "Healthcare",
                table: "BloodTest",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Name",
                schema: "Healthcare",
                table: "BloodTest",
                type: "longtext",
                unicode: false,
                nullable: true);

            // 2. Copiar valores de ParsedName a Name
            migrationBuilder.Sql(
                @"UPDATE `Healthcare`.`BloodTest` 
                  SET `Name` = `ParsedName`");

            // 3. Eliminar columnas ParsedName y OriginalName
            migrationBuilder.DropColumn(
                name: "ParsedName",
                schema: "Healthcare",
                table: "BloodTest");

            migrationBuilder.DropColumn(
                name: "OriginalName",
                schema: "Healthcare",
                table: "BloodTest");

            // 4. Hacer que Name no permita valores nulos
            migrationBuilder.AlterColumn<string>(
                name: "Name",
                schema: "Healthcare",
                table: "BloodTest",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);
        }
    }
}
