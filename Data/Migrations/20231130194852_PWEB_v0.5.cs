using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class PWEB_v05 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CheckInId",
                table: "Aluguer",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "CheckOutId",
                table: "Aluguer",
                type: "int",
                nullable: true);

            migrationBuilder.CreateTable(
                name: "CheckIn",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncionarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AluguerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckIn", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckIn_Aluguer_AluguerId",
                        column: x => x.AluguerId,
                        principalTable: "Aluguer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "CheckOut",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Observacoes = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    FuncionarioId = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    AluguerId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CheckOut", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CheckOut_Aluguer_AluguerId",
                        column: x => x.AluguerId,
                        principalTable: "Aluguer",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_CheckIn_AluguerId",
                table: "CheckIn",
                column: "AluguerId",
                unique: true,
                filter: "[AluguerId] IS NOT NULL");

            migrationBuilder.CreateIndex(
                name: "IX_CheckOut_AluguerId",
                table: "CheckOut",
                column: "AluguerId",
                unique: true,
                filter: "[AluguerId] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "CheckIn");

            migrationBuilder.DropTable(
                name: "CheckOut");

            migrationBuilder.DropColumn(
                name: "CheckInId",
                table: "Aluguer");

            migrationBuilder.DropColumn(
                name: "CheckOutId",
                table: "Aluguer");
        }
    }
}
