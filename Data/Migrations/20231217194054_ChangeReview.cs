using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class ChangeReview : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes");

            migrationBuilder.AddColumn<int>(
                name: "AluguerId",
                table: "Pontuacoes",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "PontuacaoId",
                table: "Alugueres",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alugueres_PontuacaoId",
                table: "Alugueres",
                column: "PontuacaoId",
                unique: true,
                filter: "[PontuacaoId] IS NOT NULL");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_Pontuacoes_PontuacaoId",
                table: "Alugueres",
                column: "PontuacaoId",
                principalTable: "Pontuacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_Pontuacoes_PontuacaoId",
                table: "Alugueres");

            migrationBuilder.DropForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes");

            migrationBuilder.DropIndex(
                name: "IX_Alugueres_PontuacaoId",
                table: "Alugueres");

            migrationBuilder.DropColumn(
                name: "AluguerId",
                table: "Pontuacoes");

            migrationBuilder.DropColumn(
                name: "PontuacaoId",
                table: "Alugueres");

            migrationBuilder.AddForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
