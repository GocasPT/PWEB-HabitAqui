using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class UpdateFoto3 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoArquivo",
                table: "Fotografias");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CaminhoArquivo",
                table: "Fotografias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
