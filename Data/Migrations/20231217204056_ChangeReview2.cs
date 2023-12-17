using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class ChangeReview2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_Pontuacoes_PontuacaoId",
                table: "Alugueres");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_Pontuacoes_PontuacaoId",
                table: "Alugueres",
                column: "PontuacaoId",
                principalTable: "Pontuacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_Pontuacoes_PontuacaoId",
                table: "Alugueres");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_Pontuacoes_PontuacaoId",
                table: "Alugueres",
                column: "PontuacaoId",
                principalTable: "Pontuacoes",
                principalColumn: "Id");
        }
    }
}
