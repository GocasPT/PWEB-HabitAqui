using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class Update_Locador : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Locadores_TipoLocador_TipoLocadorId",
                table: "Locadores");

            migrationBuilder.DropIndex(
                name: "IX_Locadores_TipoLocadorId",
                table: "Locadores");

            migrationBuilder.DropColumn(
                name: "TipoLocadorId",
                table: "Locadores");

            migrationBuilder.AlterColumn<int>(
                name: "Contacto",
                table: "Locadores",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<string>(
                name: "Email",
                table: "Locadores",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "IsEmpresa",
                table: "Locadores",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Email",
                table: "Locadores");

            migrationBuilder.DropColumn(
                name: "IsEmpresa",
                table: "Locadores");

            migrationBuilder.AlterColumn<string>(
                name: "Contacto",
                table: "Locadores",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "TipoLocadorId",
                table: "Locadores",
                type: "int",
                nullable: true);

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
    }
}
