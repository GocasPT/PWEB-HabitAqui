using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class PWEB_v1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aluguer_Habitacoes_HabitacaoId",
                table: "Aluguer");

            migrationBuilder.DropForeignKey(
                name: "FK_Aluguer_Locador_LocadorId",
                table: "Aluguer");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIn_Aluguer_AluguerId",
                table: "CheckIn");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOut_Aluguer_AluguerId",
                table: "CheckOut");

            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locador_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locador",
                table: "Locador");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckOut",
                table: "CheckOut");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckIn",
                table: "CheckIn");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Aluguer",
                table: "Aluguer");

            migrationBuilder.RenameTable(
                name: "Locador",
                newName: "Locadores");

            migrationBuilder.RenameTable(
                name: "CheckOut",
                newName: "CheckOuts");

            migrationBuilder.RenameTable(
                name: "CheckIn",
                newName: "CheckIns");

            migrationBuilder.RenameTable(
                name: "Aluguer",
                newName: "Alugueres");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOut_AluguerId",
                table: "CheckOuts",
                newName: "IX_CheckOuts_AluguerId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckIn_AluguerId",
                table: "CheckIns",
                newName: "IX_CheckIns_AluguerId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluguer_LocadorId",
                table: "Alugueres",
                newName: "IX_Alugueres_LocadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Aluguer_HabitacaoId",
                table: "Alugueres",
                newName: "IX_Alugueres_HabitacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locadores",
                table: "Locadores",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckOuts",
                table: "CheckOuts",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckIns",
                table: "CheckIns",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Alugueres",
                table: "Alugueres",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_Habitacoes_HabitacaoId",
                table: "Alugueres",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_Locadores_LocadorId",
                table: "Alugueres",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIns_Alugueres_AluguerId",
                table: "CheckIns",
                column: "AluguerId",
                principalTable: "Alugueres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOuts_Alugueres_AluguerId",
                table: "CheckOuts",
                column: "AluguerId",
                principalTable: "Alugueres",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_Habitacoes_HabitacaoId",
                table: "Alugueres");

            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_Locadores_LocadorId",
                table: "Alugueres");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckIns_Alugueres_AluguerId",
                table: "CheckIns");

            migrationBuilder.DropForeignKey(
                name: "FK_CheckOuts_Alugueres_AluguerId",
                table: "CheckOuts");

            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Locadores",
                table: "Locadores");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckOuts",
                table: "CheckOuts");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CheckIns",
                table: "CheckIns");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Alugueres",
                table: "Alugueres");

            migrationBuilder.RenameTable(
                name: "Locadores",
                newName: "Locador");

            migrationBuilder.RenameTable(
                name: "CheckOuts",
                newName: "CheckOut");

            migrationBuilder.RenameTable(
                name: "CheckIns",
                newName: "CheckIn");

            migrationBuilder.RenameTable(
                name: "Alugueres",
                newName: "Aluguer");

            migrationBuilder.RenameIndex(
                name: "IX_CheckOuts_AluguerId",
                table: "CheckOut",
                newName: "IX_CheckOut_AluguerId");

            migrationBuilder.RenameIndex(
                name: "IX_CheckIns_AluguerId",
                table: "CheckIn",
                newName: "IX_CheckIn_AluguerId");

            migrationBuilder.RenameIndex(
                name: "IX_Alugueres_LocadorId",
                table: "Aluguer",
                newName: "IX_Aluguer_LocadorId");

            migrationBuilder.RenameIndex(
                name: "IX_Alugueres_HabitacaoId",
                table: "Aluguer",
                newName: "IX_Aluguer_HabitacaoId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Locador",
                table: "Locador",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckOut",
                table: "CheckOut",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CheckIn",
                table: "CheckIn",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Aluguer",
                table: "Aluguer",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguer_Habitacoes_HabitacaoId",
                table: "Aluguer",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Aluguer_Locador_LocadorId",
                table: "Aluguer",
                column: "LocadorId",
                principalTable: "Locador",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckIn_Aluguer_AluguerId",
                table: "CheckIn",
                column: "AluguerId",
                principalTable: "Aluguer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_CheckOut_Aluguer_AluguerId",
                table: "CheckOut",
                column: "AluguerId",
                principalTable: "Aluguer",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Locador_LocadorId",
                table: "Habitacoes",
                column: "LocadorId",
                principalTable: "Locador",
                principalColumn: "Id");
        }
    }
}
