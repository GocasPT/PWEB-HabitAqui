using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class AddFotoItems : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_CheckIns_CheckInId",
                table: "Fotografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropIndex(
                name: "IX_Fotografias_CheckInId",
                table: "Fotografias");

            migrationBuilder.DropColumn(
                name: "CheckInId",
                table: "Fotografias");

            migrationBuilder.AlterColumn<int>(
                name: "LocadorId",
                table: "Habitacoes",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddColumn<int>(
                name: "CheckOutId",
                table: "Fotografias",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "HabitacaoId",
                table: "Fotografias",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckInItems",
                columns: table => new
                {
                    CheckInId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckInItems", x => new { x.CheckInId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CheckInItems_CheckIns_CheckInId",
                        column: x => x.CheckInId,
                        principalTable: "CheckIns",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckInItems_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "CheckOutItems",
                columns: table => new
                {
                    CheckOutId = table.Column<int>(type: "int", nullable: false),
                    ItemId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOutItems", x => new { x.CheckOutId, x.ItemId });
                    table.ForeignKey(
                        name: "FK_CheckOutItems_CheckOuts_CheckOutId",
                        column: x => x.CheckOutId,
                        principalTable: "CheckOuts",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_CheckOutItems_Itens_ItemId",
                        column: x => x.ItemId,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_CheckOutId",
                table: "Fotografias",
                column: "CheckOutId");

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_HabitacaoId",
                table: "Fotografias",
                column: "HabitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckInItems_ItemId",
                table: "CheckInItems",
                column: "ItemId");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOutItems_ItemId",
                table: "CheckOutItems",
                column: "ItemId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_CheckOuts_CheckOutId",
                table: "Fotografias",
                column: "CheckOutId",
                principalTable: "CheckOuts",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_Habitacoes_HabitacaoId",
                table: "Fotografias",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
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
                name: "FK_Fotografias_CheckOuts_CheckOutId",
                table: "Fotografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Fotografias_Habitacoes_HabitacaoId",
                table: "Fotografias");

            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes");

            migrationBuilder.DropTable(
                name: "CheckInItems");

            migrationBuilder.DropTable(
                name: "CheckOutItems");

            migrationBuilder.DropIndex(
                name: "IX_Fotografias_CheckOutId",
                table: "Fotografias");

            migrationBuilder.DropIndex(
                name: "IX_Fotografias_HabitacaoId",
                table: "Fotografias");

            migrationBuilder.DropColumn(
                name: "CheckOutId",
                table: "Fotografias");

            migrationBuilder.DropColumn(
                name: "HabitacaoId",
                table: "Fotografias");

            migrationBuilder.AlterColumn<int>(
                name: "LocadorId",
                table: "Habitacoes",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckInId",
                table: "Fotografias",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Fotografias_CheckInId",
                table: "Fotografias",
                column: "CheckInId");

            migrationBuilder.AddForeignKey(
                name: "FK_Fotografias_CheckIns_CheckInId",
                table: "Fotografias",
                column: "CheckInId",
                principalTable: "CheckIns",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Locadores_LocadorId",
                table: "Habitacoes",
                column: "LocadorId",
                principalTable: "Locadores",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
