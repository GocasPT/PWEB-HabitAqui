using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class Locafor_Fix : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumFuncionarios",
                table: "Locadores");

            migrationBuilder.DropColumn(
                name: "NumGestores",
                table: "Locadores");

            migrationBuilder.DropColumn(
                name: "NumHabitacoes",
                table: "Locadores");

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "Locadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Contacto",
                table: "Locadores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "NumFuncionarios",
                table: "Locadores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumGestores",
                table: "Locadores",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "NumHabitacoes",
                table: "Locadores",
                type: "int",
                nullable: true);
        }
    }
}
