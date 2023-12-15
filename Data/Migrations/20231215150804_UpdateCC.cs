using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class UpdateCC : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "DataCheckIn",
                table: "CheckIns",
                type: "datetime2",
                nullable: true,
                defaultValue: DateTime.UtcNow);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataCheckOut",
                table: "CheckOuts",
                type: "datetime2",
                nullable: true,
                defaultValue: DateTime.UtcNow);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DataCheckOut",
                table: "CheckOuts");

            migrationBuilder.DropColumn(
                name: "DataCheckIn",
                table: "CheckIns");
        }
    }
}
