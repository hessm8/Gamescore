using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Gamescore.Data.Migrations
{
    public partial class properentities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Profiles_SentById",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequest_Profiles_SentToId",
                table: "FriendRequest");

            migrationBuilder.DropForeignKey(
                name: "FK_GameUserProfile_Games_GamesId",
                table: "GameUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_GameUserProfile_Profiles_UsersId",
                table: "GameUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Sessions_SessionId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "Sessions");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest");

            migrationBuilder.DropColumn(
                name: "IsWinner",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Points",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "Team",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "FriendRequest",
                newName: "FriendRequests");

            migrationBuilder.RenameColumn(
                name: "SessionId",
                table: "Players",
                newName: "UserPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_SessionId",
                table: "Players",
                newName: "IX_Players_UserPlayerId");

            migrationBuilder.RenameColumn(
                name: "UsersId",
                table: "GameUserProfile",
                newName: "GamesFavoritedId");

            migrationBuilder.RenameColumn(
                name: "GamesId",
                table: "GameUserProfile",
                newName: "FavoritedById");

            migrationBuilder.RenameIndex(
                name: "IX_GameUserProfile_UsersId",
                table: "GameUserProfile",
                newName: "IX_GameUserProfile_GamesFavoritedId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequest_SentToId",
                table: "FriendRequests",
                newName: "IX_FriendRequests_SentToId");

            migrationBuilder.AddColumn<string>(
                name: "Alias",
                table: "Players",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId",
                table: "Players",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AlterColumn<string>(
                name: "NameLocalized",
                table: "Games",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Games",
                type: "varbinary(max)",
                nullable: true,
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(128)",
                oldMaxLength: 128);

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests",
                columns: new[] { "SentById", "SentToId" });

            migrationBuilder.CreateTable(
                name: "Matches",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matches", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Matches_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Ratings",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    UserId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    RatingGameplay = table.Column<float>(type: "real", nullable: false),
                    RatingOriginality = table.Column<float>(type: "real", nullable: false),
                    RatingImplementation = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Ratings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Ratings_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Ratings_Profiles_UserId",
                        column: x => x.UserId,
                        principalTable: "Profiles",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "MatchPlayer",
                columns: table => new
                {
                    MatchId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    PlayerId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Team = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Points = table.Column<int>(type: "int", nullable: false),
                    IsWinner = table.Column<bool>(type: "bit", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MatchPlayer", x => new { x.MatchId, x.PlayerId });
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Matches_MatchId",
                        column: x => x.MatchId,
                        principalTable: "Matches",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MatchPlayer_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Players_OwnerId",
                table: "Players",
                column: "OwnerId");

            migrationBuilder.CreateIndex(
                name: "IX_Matches_GameId",
                table: "Matches",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_MatchPlayer_PlayerId",
                table: "MatchPlayer",
                column: "PlayerId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_GameId",
                table: "Ratings",
                column: "GameId");

            migrationBuilder.CreateIndex(
                name: "IX_Ratings_UserId",
                table: "Ratings",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Profiles_SentById",
                table: "FriendRequests",
                column: "SentById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequests_Profiles_SentToId",
                table: "FriendRequests",
                column: "SentToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameUserProfile_Games_GamesFavoritedId",
                table: "GameUserProfile",
                column: "GamesFavoritedId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameUserProfile_Profiles_FavoritedById",
                table: "GameUserProfile",
                column: "FavoritedById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Profiles_OwnerId",
                table: "Players",
                column: "OwnerId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Profiles_UserPlayerId",
                table: "Players",
                column: "UserPlayerId",
                principalTable: "Profiles",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Profiles_SentById",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_FriendRequests_Profiles_SentToId",
                table: "FriendRequests");

            migrationBuilder.DropForeignKey(
                name: "FK_GameUserProfile_Games_GamesFavoritedId",
                table: "GameUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_GameUserProfile_Profiles_FavoritedById",
                table: "GameUserProfile");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Profiles_OwnerId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Profiles_UserPlayerId",
                table: "Players");

            migrationBuilder.DropTable(
                name: "MatchPlayer");

            migrationBuilder.DropTable(
                name: "Ratings");

            migrationBuilder.DropTable(
                name: "Matches");

            migrationBuilder.DropIndex(
                name: "IX_Players_OwnerId",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_FriendRequests",
                table: "FriendRequests");

            migrationBuilder.DropColumn(
                name: "Alias",
                table: "Players");

            migrationBuilder.DropColumn(
                name: "OwnerId",
                table: "Players");

            migrationBuilder.RenameTable(
                name: "FriendRequests",
                newName: "FriendRequest");

            migrationBuilder.RenameColumn(
                name: "UserPlayerId",
                table: "Players",
                newName: "SessionId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_UserPlayerId",
                table: "Players",
                newName: "IX_Players_SessionId");

            migrationBuilder.RenameColumn(
                name: "GamesFavoritedId",
                table: "GameUserProfile",
                newName: "UsersId");

            migrationBuilder.RenameColumn(
                name: "FavoritedById",
                table: "GameUserProfile",
                newName: "GamesId");

            migrationBuilder.RenameIndex(
                name: "IX_GameUserProfile_GamesFavoritedId",
                table: "GameUserProfile",
                newName: "IX_GameUserProfile_UsersId");

            migrationBuilder.RenameIndex(
                name: "IX_FriendRequests_SentToId",
                table: "FriendRequest",
                newName: "IX_FriendRequest_SentToId");

            migrationBuilder.AddColumn<bool>(
                name: "IsWinner",
                table: "Players",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "Points",
                table: "Players",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Team",
                table: "Players",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "NameLocalized",
                table: "Games",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<byte[]>(
                name: "Image",
                table: "Games",
                type: "varbinary(max)",
                nullable: false,
                defaultValue: new byte[0],
                oldClrType: typeof(byte[]),
                oldType: "varbinary(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserTokens",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "ProviderKey",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AlterColumn<string>(
                name: "LoginProvider",
                table: "AspNetUserLogins",
                type: "nvarchar(128)",
                maxLength: 128,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddPrimaryKey(
                name: "PK_FriendRequest",
                table: "FriendRequest",
                columns: new[] { "SentById", "SentToId" });

            migrationBuilder.CreateTable(
                name: "Sessions",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    GameId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Comment = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Duration = table.Column<int>(type: "int", nullable: false),
                    Place = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sessions", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Sessions_Games_GameId",
                        column: x => x.GameId,
                        principalTable: "Games",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sessions_GameId",
                table: "Sessions",
                column: "GameId");

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Profiles_SentById",
                table: "FriendRequest",
                column: "SentById",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_FriendRequest_Profiles_SentToId",
                table: "FriendRequest",
                column: "SentToId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameUserProfile_Games_GamesId",
                table: "GameUserProfile",
                column: "GamesId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_GameUserProfile_Profiles_UsersId",
                table: "GameUserProfile",
                column: "UsersId",
                principalTable: "Profiles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Sessions_SessionId",
                table: "Players",
                column: "SessionId",
                principalTable: "Sessions",
                principalColumn: "Id");
        }
    }
}
