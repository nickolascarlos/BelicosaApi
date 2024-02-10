using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class ChangeAvailableContinentalDistributionToListOfTuples : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continent_Games_GameId",
                table: "Continent");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Continent_ContinentId",
                table: "Territories");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continent",
                table: "Continent");

            migrationBuilder.RenameTable(
                name: "Continent",
                newName: "Continents");

            migrationBuilder.RenameIndex(
                name: "IX_Continent_GameId",
                table: "Continents",
                newName: "IX_Continents_GameId");

            migrationBuilder.AddColumn<int>(
                name: "CurrentPlayerIndex",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "currentPlayerTurnStage",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continents",
                table: "Continents",
                column: "Id");

            migrationBuilder.CreateTable(
                name: "ContinentalTroopsAvailability",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    PlayerId = table.Column<int>(type: "integer", nullable: false),
                    ContinentId = table.Column<int>(type: "integer", nullable: false),
                    AvailableTroops = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ContinentalTroopsAvailability", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ContinentalTroopsAvailability_Continents_ContinentId",
                        column: x => x.ContinentId,
                        principalTable: "Continents",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ContinentalTroopsAvailability_Players_PlayerId",
                        column: x => x.PlayerId,
                        principalTable: "Players",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ContinentalTroopsAvailability_ContinentId",
                table: "ContinentalTroopsAvailability",
                column: "ContinentId");

            migrationBuilder.CreateIndex(
                name: "IX_ContinentalTroopsAvailability_PlayerId",
                table: "ContinentalTroopsAvailability",
                column: "PlayerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Continents_Games_GameId",
                table: "Continents",
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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Continents_Games_GameId",
                table: "Continents");

            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Continents_ContinentId",
                table: "Territories");

            migrationBuilder.DropTable(
                name: "ContinentalTroopsAvailability");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Continents",
                table: "Continents");

            migrationBuilder.DropColumn(
                name: "CurrentPlayerIndex",
                table: "Games");

            migrationBuilder.DropColumn(
                name: "currentPlayerTurnStage",
                table: "Games");

            migrationBuilder.RenameTable(
                name: "Continents",
                newName: "Continent");

            migrationBuilder.RenameIndex(
                name: "IX_Continents_GameId",
                table: "Continent",
                newName: "IX_Continent_GameId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Continent",
                table: "Continent",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Continent_Games_GameId",
                table: "Continent",
                column: "GameId",
                principalTable: "Games",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Continent_ContinentId",
                table: "Territories",
                column: "ContinentId",
                principalTable: "Continent",
                principalColumn: "Id");
        }
    }
}
