using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class migration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Concelho",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Distrito",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Name",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Pais",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipologia",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Concelho",
                table: "Habitacoes");

            migrationBuilder.DropColumn(
                name: "Distrito",
                table: "Habitacoes");

            migrationBuilder.DropColumn(
                name: "Name",
                table: "Habitacoes");

            migrationBuilder.DropColumn(
                name: "Pais",
                table: "Habitacoes");

            migrationBuilder.DropColumn(
                name: "Tipologia",
                table: "Habitacoes");
        }
    }
}
