using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class teste : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId1",
                table: "Alugueres");

            migrationBuilder.DropIndex(
                name: "IX_Alugueres_ClienteId1",
                table: "Alugueres");

            migrationBuilder.DropColumn(
                name: "ClienteId1",
                table: "Alugueres");

            migrationBuilder.AlterColumn<string>(
                name: "ClienteId",
                table: "Alugueres",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alugueres_ClienteId",
                table: "Alugueres",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId",
                table: "Alugueres",
                column: "ClienteId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId",
                table: "Alugueres");

            migrationBuilder.DropIndex(
                name: "IX_Alugueres_ClienteId",
                table: "Alugueres");

            migrationBuilder.AlterColumn<int>(
                name: "ClienteId",
                table: "Alugueres",
                type: "int",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ClienteId1",
                table: "Alugueres",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Alugueres_ClienteId1",
                table: "Alugueres",
                column: "ClienteId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId1",
                table: "Alugueres",
                column: "ClienteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
