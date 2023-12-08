using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class addHItens : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Itens_Habitacoes_HabitacaoId",
                table: "Itens");

            migrationBuilder.DropIndex(
                name: "IX_Itens_HabitacaoId",
                table: "Itens");

            migrationBuilder.DropColumn(
                name: "HabitacaoId",
                table: "Itens");

            migrationBuilder.CreateTable(
                name: "Habitacoes_Itens",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    HabitacaoId = table.Column<int>(type: "int", nullable: false),
                    ItemID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Habitacoes_Itens", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Habitacoes_Itens_Habitacoes_HabitacaoId",
                        column: x => x.HabitacaoId,
                        principalTable: "Habitacoes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Habitacoes_Itens_Itens_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Itens",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Habitacoes_Itens_HabitacaoId",
                table: "Habitacoes_Itens",
                column: "HabitacaoId");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacoes_Itens_ItemID",
                table: "Habitacoes_Itens",
                column: "ItemID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Habitacoes_Itens");

            migrationBuilder.AddColumn<int>(
                name: "HabitacaoId",
                table: "Itens",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Itens_HabitacaoId",
                table: "Itens",
                column: "HabitacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Itens_Habitacoes_HabitacaoId",
                table: "Itens",
                column: "HabitacaoId",
                principalTable: "Habitacoes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
