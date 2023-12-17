using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class Remove_TipoLocador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TipoLocador");

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

        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.CreateTable(
                name: "TipoLocador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TipoLocador", x => x.Id);
                });
        }
    }
}
