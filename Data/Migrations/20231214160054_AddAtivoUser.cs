using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class AddAtivoUser : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "Ativo",
                table: "AspNetUsers",
                type: "bit",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Ativo",
                table: "AspNetUsers");
        }
    }
}
