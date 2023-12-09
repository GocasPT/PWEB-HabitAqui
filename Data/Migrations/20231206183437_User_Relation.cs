using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class User_Relation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "CheckOuts",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "CheckIns",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOuts_FuncionarioId",
                table: "CheckOuts",
                column: "FuncionarioId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckIns_FuncionarioId",
                table: "CheckIns",
                column: "FuncionarioId");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_AspNetUsers_FuncionarioId",
                table: "CheckIns",
                column: "FuncionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_AspNetUsers_FuncionarioId",
                table: "CheckOuts",
                column: "FuncionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId",
                table: "Alugueres");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_AspNetUsers_FuncionarioId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_AspNetUsers_FuncionarioId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckOuts_FuncionarioId",
                table: "CheckOuts");

            migrationBuilder.DropIndex(
                name: "IX_CheckIns_FuncionarioId",
                table: "CheckIns");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Alugueres",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Alugueres_ClienteId",
                table: "Alugueres",
                newName: "IX_Alugueres_ApplicationUserId");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "CheckOuts",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "CheckIns",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_AspNetUsers_ApplicationUserId",
                table: "Alugueres",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
