using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class Tipologia : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipologia",
                table: "Habitacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<int>(
                name: "TipologiaId",
                table: "Habitacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Tipologia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tipologia", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitacoes_TipologiaId",
                table: "Habitacoes",
                column: "TipologiaId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Tipologia_TipologiaId",
                table: "Habitacoes",
                column: "TipologiaId",
                principalTable: "Tipologia",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Tipologia_TipologiaId",
                table: "Habitacoes");

            migrationBuilder.DropTable(
                name: "Tipologia");

            migrationBuilder.DropIndex(
                name: "IX_Habitacoes_TipologiaId",
                table: "Habitacoes");

            migrationBuilder.DropColumn(
                name: "TipologiaId",
                table: "Habitacoes");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Tipologia",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
