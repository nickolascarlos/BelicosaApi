using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddTerritoryTerritoryRelation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Territories_TerritoryId",
                table: "Territories");

            migrationBuilder.DropIndex(
                name: "IX_Territories_TerritoryId",
                table: "Territories");

            migrationBuilder.DropColumn(
                name: "TerritoryId",
                table: "Territories");

            migrationBuilder.CreateTable(
                name: "TerritoryTerritory",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    TerritoryFromId = table.Column<int>(type: "integer", nullable: false),
                    TerritoryToId = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TerritoryTerritory", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TerritoryTerritory_Territories_TerritoryFromId",
                        column: x => x.TerritoryFromId,
                        principalTable: "Territories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_TerritoryTerritory_Territories_TerritoryToId",
                        column: x => x.TerritoryToId,
                        principalTable: "Territories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryTerritory_TerritoryFromId",
                table: "TerritoryTerritory",
                column: "TerritoryFromId");

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryTerritory_TerritoryToId",
                table: "TerritoryTerritory",
                column: "TerritoryToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "TerritoryTerritory");

            migrationBuilder.AddColumn<int>(
                name: "TerritoryId",
                table: "Territories",
                type: "integer",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Territories_TerritoryId",
                table: "Territories",
                column: "TerritoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Territories_TerritoryId",
                table: "Territories",
                column: "TerritoryId",
                principalTable: "Territories",
                principalColumn: "Id");
        }
    }
}
