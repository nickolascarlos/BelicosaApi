using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddStartAndFinishTimeInBelicosaGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Continent_ContinentId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Games_GameId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Players_OccupyingPlayerId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_Territory_Territory_TerritoryId",
                table: "Territory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Territory_TerritoryId",
                table: "TerritoryCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Territory",
                table: "Territory");

            migrationBuilder.RenameTable(
                name: "Territory",
                newName: "Territories");

            migrationBuilder.RenameIndex(
                name: "IX_Territory_TerritoryId",
                table: "Territories",
                newName: "IX_Territories_TerritoryId");

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

            migrationBuilder.AddColumn<DateTime>(
                name: "FinishTime",
                table: "Games",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<DateTime>(
                name: "StartTime",
                table: "Games",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Territories",
                table: "Territories",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Continent_ContinentId",
                table: "Territories",
                column: "ContinentId",
                principalTable: "Continent",
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
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Territories_TerritoryId",
                table: "Territories",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Territories_TerritoryId",
                table: "TerritoryCard",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Continent_ContinentId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Games_GameId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Territories_TerritoryId",
                table: "Territories");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryCard_Territories_TerritoryId",
                table: "TerritoryCard");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Territories",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "FinishTime",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "StartTime",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Territories",
                newName: "Territory");

            migrationBuilder.RenameIndex(
                name: "IX_Territories_TerritoryId",
                table: "Territory",
                newName: "IX_Territory_TerritoryId");

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

            migrationBuilder.AddPrimaryKey(
                name: "PK_Territory",
                table: "Territory",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Continent_ContinentId",
                table: "Territory",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Games_GameId",
                table: "Territory",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Players_OccupyingPlayerId",
                table: "Territory",
                column: "OccupyingPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territory_Territory_TerritoryId",
                table: "Territory",
                column: "TerritoryId",
                principalTable: "Territory",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryCard_Territory_TerritoryId",
                table: "TerritoryCard",
                column: "TerritoryId",
                principalTable: "Territory",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
