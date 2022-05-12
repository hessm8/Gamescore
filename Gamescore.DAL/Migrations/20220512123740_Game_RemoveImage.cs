using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamescore.DAL.Migrations
{
    public partial class Game_RemoveImage : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Image",
                table: "Games");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<byte[]>(
                name: "Image",
                table: "Games",
                type: "varbinary(max)",
                nullable: true);
        }
    }
}
