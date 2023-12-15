using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class UpdateFoto2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_Habitacoes_HabitacaoId",
                table: "Fotografias");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_Habitacoes_HabitacaoId",
                table: "Fotografias",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_Habitacoes_HabitacaoId",
                table: "Fotografias");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_Habitacoes_HabitacaoId",
                table: "Fotografias",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id");
        }
    }
}
