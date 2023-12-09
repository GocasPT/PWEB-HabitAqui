using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class HabitacaoCascade : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes");

            migrationBuilder.AlterColumn<int>(
                name: "HabitacaoId",
                table: "Pontuacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Tipologia",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<int>(
                name: "LocadorId",
                table: "Habitacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Distrito",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Concelho",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes");

            migrationBuilder.AlterColumn<int>(
                name: "HabitacaoId",
                table: "Pontuacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Tipologia",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Pais",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<int>(
                name: "LocadorId",
                table: "Habitacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AlterColumn<string>(
                name: "Distrito",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Concelho",
                table: "Habitacoes",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Pontuacoes_Habitacoes_HabitacaoId",
                table: "Pontuacoes",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id");
        }
    }
}
