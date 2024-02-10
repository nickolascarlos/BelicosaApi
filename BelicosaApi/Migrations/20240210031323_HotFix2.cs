using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class HotFix2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "TerritoryTerritory",
                type: "integer",
                nullable: false,
                defaultValue: 0)
                .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryTerritory_TerritoryToId",
                table: "TerritoryTerritory",
                column: "TerritoryToId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory");

            migrationBuilder.DropIndex(
                name: "IX_TerritoryTerritory_TerritoryToId",
                table: "TerritoryTerritory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TerritoryTerritory");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory",
                columns: new[] { "TerritoryToId", "TerritoryId" });
        }
    }
}
