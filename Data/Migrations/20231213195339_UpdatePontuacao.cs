using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class UpdatePontuacao : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                table: "Pontuacoes",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Pontuacoes_ApplicationUserId",
                table: "Pontuacoes",
                column: "ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Pontuacoes_AspNetUsers_ApplicationUserId",
                table: "Pontuacoes",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Pontuacoes_AspNetUsers_ApplicationUserId",
                table: "Pontuacoes");

            migrationBuilder.DropIndex(
                name: "IX_Pontuacoes_ApplicationUserId",
                table: "Pontuacoes");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                table: "Pontuacoes");
        }
    }
}
