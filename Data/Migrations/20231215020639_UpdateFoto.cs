using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HabitAqui.Migrations
{
    public partial class UpdateFoto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Data",
                table: "Fotografias");

            migrationBuilder.AddColumn<string>(
                name: "CaminhoArquivo",
                table: "Fotografias",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CaminhoArquivo",
                table: "Fotografias");

            migrationBuilder.AddColumn<byte[]>(
                name: "Data",
                table: "Fotografias",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0]);
        }
    }
}
