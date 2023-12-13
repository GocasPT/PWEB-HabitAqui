using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class UpdateTables : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Itens_Itens_ItemID",
                table: "Habitacoes_Itens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Habitacoes_Itens",
                table: "Habitacoes_Itens");

            migrationBuilder.DropIndex(
                name: "IX_Habitacoes_Itens_HabitacaoId",
                table: "Habitacoes_Itens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Habitacoes_Itens");

            migrationBuilder.RenameColumn(
                name: "ItemID",
                table: "Habitacoes_Itens",
                newName: "ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Habitacoes_Itens_ItemID",
                table: "Habitacoes_Itens",
                newName: "IX_Habitacoes_Itens_ItemId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Habitacoes_Itens",
                table: "Habitacoes_Itens",
                columns: new[] { "HabitacaoId", "ItemId" });

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Itens_Itens_ItemId",
                table: "Habitacoes_Itens",
                column: "ItemId",
                principalTable: "Itens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Habitacoes_Itens_Itens_ItemId",
                table: "Habitacoes_Itens");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Habitacoes_Itens",
                table: "Habitacoes_Itens");

            migrationBuilder.RenameColumn(
                name: "ItemId",
                table: "Habitacoes_Itens",
                newName: "ItemID");

            migrationBuilder.RenameIndex(
                name: "IX_Habitacoes_Itens_ItemId",
                table: "Habitacoes_Itens",
                newName: "IX_Habitacoes_Itens_ItemID");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Habitacoes_Itens",
                type: "int",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:Identity", "1, 1");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Habitacoes_Itens",
                table: "Habitacoes_Itens",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_Habitacoes_Itens_HabitacaoId",
                table: "Habitacoes_Itens",
                column: "HabitacaoId");

            migrationBuilder.AddForeignKey(
                name: "FK_Habitacoes_Itens_Itens_ItemID",
                table: "Habitacoes_Itens",
                column: "ItemID",
                principalTable: "Itens",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
