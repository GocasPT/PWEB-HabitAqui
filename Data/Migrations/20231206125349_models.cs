using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_AspNetUsers_ApplicationUserId",
                table: "Alugueres");

            migrationBuilder.RenameColumn(
                name: "ApplicationUserId",
                table: "Alugueres",
                newName: "ClienteId1");

            migrationBuilder.RenameIndex(
                name: "IX_Alugueres_ApplicationUserId",
                table: "Alugueres",
                newName: "IX_Alugueres_ClienteId1");

            migrationBuilder.AddColumn<int>(
                name: "ClienteId",
                table: "Alugueres",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ModelTest",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ModelTest", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId1",
                table: "Alugueres",
                column: "ClienteId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Alugueres_AspNetUsers_ClienteId1",
                table: "Alugueres");

            migrationBuilder.DropTable(
                name: "ModelTest");

            migrationBuilder.DropColumn(
                name: "ClienteId",
                table: "Alugueres");

            migrationBuilder.RenameColumn(
                name: "ClienteId1",
                table: "Alugueres",
                newName: "ApplicationUserId");

            migrationBuilder.RenameIndex(
                name: "IX_Alugueres_ClienteId1",
                table: "Alugueres",
                newName: "IX_Alugueres_ApplicationUserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Alugueres_AspNetUsers_ApplicationUserId",
                table: "Alugueres",
                column: "ApplicationUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");
        }
    }
}
