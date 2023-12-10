using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class TipoLocador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tipo",
                table: "Locadores");

            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "AspNetUsers");

            migrationBuilder.AddColumn<int>(
                name: "TipoLocadorId",
                table: "Locadores",
                type: "int",
                nullable: true);

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

            migrationBuilder.CreateIndex(
                name: "IX_Locadores_TipoLocadorId",
                table: "Locadores",
                column: "TipoLocadorId");

            migrationBuilder.AddForeignKey(
                name: "FK_Locadores_TipoLocador_TipoLocadorId",
                table: "Locadores",
                column: "TipoLocadorId",
                principalTable: "TipoLocador",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locadores_TipoLocador_TipoLocadorId",
                table: "Locadores");

            migrationBuilder.DropTable(
                name: "TipoLocador");

            migrationBuilder.DropIndex(
                name: "IX_Locadores_TipoLocadorId",
                table: "Locadores");

            migrationBuilder.DropColumn(
                name: "TipoLocadorId",
                table: "Locadores");

            migrationBuilder.AddColumn<string>(
                name: "Tipo",
                table: "Locadores",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }
    }
}
