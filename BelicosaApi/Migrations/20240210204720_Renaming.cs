using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class Renaming : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ContinentalTroopsAvailability_Continents_ContinentId",
                table: "ContinentalTroopsAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_ContinentalTroopsAvailability_Players_PlayerId",
                table: "ContinentalTroopsAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_Continents_Games_GameId",
                table: "Continents");

            migrationBuilder.DropForeignKey(
                name: "FK_Games_AspNetUsers_OwnerId",
                table: "Games");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_AspNetUsers_UserId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Continents_ContinentId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Games_GameId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Games_GameId",
                table: "TerritoryCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Players_HolderId",
                table: "TerritoryCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Territories_TerritoryId",
                table: "TerritoryCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryId",
                table: "TerritoryTerritory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryId1",
                table: "TerritoryTerritory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryToId",
                table: "TerritoryTerritory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Territories",
                table: "Territories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Players",
                table: "Players");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Games",
                table: "Games");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continents",
                table: "Continents");

            migrationBuilder.RenameTable(
                name: "Territories",
                newName: "Territory");

            migrationBuilder.RenameTable(
                name: "Players",
                newName: "Player");

            migrationBuilder.RenameTable(
                name: "Games",
                newName: "Game");

            migrationBuilder.RenameTable(
                name: "Continents",
                newName: "Continent");

            migrationBuilder.RenameIndex(
                name: "IX_Territories_OccupyingPlayerId",
                table: "Territory",
                newName: "IX_Territory_OccupyingPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Territories_GameId",
                table: "Territory",
                newName: "IX_Territory_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Territories_ContinentId",
                table: "Territory",
                newName: "IX_Territory_ContinentId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_UserId",
                table: "Player",
                newName: "IX_Player_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Players_GameId",
                table: "Player",
                newName: "IX_Player_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Games_OwnerId",
                table: "Game",
                newName: "IX_Game_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Continents_GameId",
                table: "Continent",
                newName: "IX_Continent_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Territory",
                table: "Territory",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Player",
                table: "Player",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Game",
                table: "Game",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continent",
                table: "Continent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Continent_Game_GameId",
                table: "Continent",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContinentalTroopsAvailability_Continent_ContinentId",
                table: "ContinentalTroopsAvailability",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContinentalTroopsAvailability_Player_PlayerId",
                table: "ContinentalTroopsAvailability",
                column: "PlayerId",
                principalTable: "Player",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Game_AspNetUsers_OwnerId",
                table: "Game",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_AspNetUsers_UserId",
                table: "Player",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Continent_ContinentId",
                table: "Territory",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Game_GameId",
                table: "Territory",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Player_OccupyingPlayerId",
                table: "Territory",
                column: "OccupyingPlayerId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Game_GameId",
                table: "TerritoryCard",
                column: "GameId",
                principalTable: "Game",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Player_HolderId",
                table: "TerritoryCard",
                column: "HolderId",
                principalTable: "Player",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Territory_TerritoryId",
                table: "TerritoryCard",
                column: "TerritoryId",
                principalTable: "Territory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territory_TerritoryId",
                table: "TerritoryTerritory",
                column: "TerritoryId",
                principalTable: "Territory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territory_TerritoryId1",
                table: "TerritoryTerritory",
                column: "TerritoryId1",
                principalTable: "Territory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territory_TerritoryToId",
                table: "TerritoryTerritory",
                column: "TerritoryToId",
                principalTable: "Territory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continent_Game_GameId",
                table: "Continent");

            migrationBuilder.DropForeignKey(
                name: "FK_ContinentalTroopsAvailability_Continent_ContinentId",
                table: "ContinentalTroopsAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_ContinentalTroopsAvailability_Player_PlayerId",
                table: "ContinentalTroopsAvailability");

            migrationBuilder.DropForeignKey(
                name: "FK_Game_AspNetUsers_OwnerId",
                table: "Game");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_AspNetUsers_UserId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Player_Game_GameId",
                table: "Player");

            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Continent_ContinentId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Game_GameId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Player_OccupyingPlayerId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Game_GameId",
                table: "TerritoryCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Player_HolderId",
                table: "TerritoryCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Territory_TerritoryId",
                table: "TerritoryCard");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territory_TerritoryId",
                table: "TerritoryTerritory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territory_TerritoryId1",
                table: "TerritoryTerritory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territory_TerritoryToId",
                table: "TerritoryTerritory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Territory",
                table: "Territory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Player",
                table: "Player");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Game",
                table: "Game");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continent",
                table: "Continent");

            migrationBuilder.RenameTable(
                name: "Territory",
                newName: "Territories");

            migrationBuilder.RenameTable(
                name: "Player",
                newName: "Players");

            migrationBuilder.RenameTable(
                name: "Game",
                newName: "Games");

            migrationBuilder.RenameTable(
                name: "Continent",
                newName: "Continents");

            migrationBuilder.RenameIndex(
                name: "IX_Territory_OccupyingPlayerId",
                table: "Territories",
                newName: "IX_Territories_OccupyingPlayerId");

            migrationBuilder.RenameIndex(
                name: "IX_Territory_GameId",
                table: "Territories",
                newName: "IX_Territories_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Territory_ContinentId",
                table: "Territories",
                newName: "IX_Territories_ContinentId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_UserId",
                table: "Players",
                newName: "IX_Players_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Player_GameId",
                table: "Players",
                newName: "IX_Players_GameId");

            migrationBuilder.RenameIndex(
                name: "IX_Game_OwnerId",
                table: "Games",
                newName: "IX_Games_OwnerId");

            migrationBuilder.RenameIndex(
                name: "IX_Continent_GameId",
                table: "Continents",
                newName: "IX_Continents_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Territories",
                table: "Territories",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Players",
                table: "Players",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Games",
                table: "Games",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continents",
                table: "Continents",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_ContinentalTroopsAvailability_Continents_ContinentId",
                table: "ContinentalTroopsAvailability",
                column: "ContinentId",
                principalTable: "Continents",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ContinentalTroopsAvailability_Players_PlayerId",
                table: "ContinentalTroopsAvailability",
                column: "PlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Continents_Games_GameId",
                table: "Continents",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Games_AspNetUsers_OwnerId",
                table: "Games",
                column: "OwnerId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_AspNetUsers_UserId",
                table: "Players",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Players_Games_GameId",
                table: "Players",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Continents_ContinentId",
                table: "Territories",
                column: "ContinentId",
                principalTable: "Continents",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Games_GameId",
                table: "Territories",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories",
                column: "OccupyingPlayerId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Games_GameId",
                table: "TerritoryCard",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Players_HolderId",
                table: "TerritoryCard",
                column: "HolderId",
                principalTable: "Players",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Territories_TerritoryId",
                table: "TerritoryCard",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryId",
                table: "TerritoryTerritory",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryId1",
                table: "TerritoryTerritory",
                column: "TerritoryId1",
                principalTable: "Territories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryToId",
                table: "TerritoryTerritory",
                column: "TerritoryToId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
