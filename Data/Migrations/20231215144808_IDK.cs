using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class IDK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_AspNetUsers_FuncionarioId",
                table: "CheckIns");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "CheckIns",
                type: "nvarchar(450)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_AspNetUsers_FuncionarioId",
                table: "CheckIns",
                column: "FuncionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_AspNetUsers_FuncionarioId",
                table: "CheckIns");

            migrationBuilder.AlterColumn<string>(
                name: "FuncionarioId",
                table: "CheckIns",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(450)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_AspNetUsers_FuncionarioId",
                table: "CheckIns",
                column: "FuncionarioId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
