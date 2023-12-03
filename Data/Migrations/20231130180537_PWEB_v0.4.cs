using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class PWEB_v04 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Aluguer",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataDeEntrada = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DataDeSaida = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Confirmado = table.Column<bool>(type: "bit", nullable: false),
                    HabitacaoId = table.Column<int>(type: "int", nullable: true),
                    LocadorId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Aluguer", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Aluguer_Habitacoes_HabitacaoId",
                        column: x => x.HabitacaoId,
                        principalTable: "Habitacoes",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Aluguer_Locador_LocadorId",
                        column: x => x.LocadorId,
                        principalTable: "Locador",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Aluguer_HabitacaoId",
                table: "Aluguer",
                column: "HabitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Aluguer_LocadorId",
                table: "Aluguer",
                column: "LocadorId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Aluguer");
        }
    }
}
