using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamescore.DAL.Migrations
{
    public partial class Game_RemoveUnique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Games_Alias",
                table: "Games");

            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Alias",
                table: "Games",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.CreateIndex(
                name: "IX_Games_Alias",
                table: "Games",
                column: "Alias",
                unique: true);
        }
    }
}
