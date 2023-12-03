using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class PWEB_v03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "LocadorId",
                table: "Habitacoes",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "Locador",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Contacto = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Locador", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitacoes_LocadorId",
                table: "Habitacoes",
                column: "LocadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Locador_LocadorId",
                table: "Habitacoes",
                column: "LocadorId",
                principalTable: "Locador",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locador_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropTable(
                name: "Locador");

            migrationBuilder.DropIndex(
                name: "IX_Habitacoes_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropColumn(
                name: "LocadorId",
                table: "Habitacoes");
        }
    }
}
