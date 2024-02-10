using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class HotFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryFromId",
                table: "TerritoryTerritory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory");

            migrationBuilder.DropIndex(
                name: "IX_TerritoryTerritory_TerritoryToId",
                table: "TerritoryTerritory");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "TerritoryTerritory");

            migrationBuilder.RenameColumn(
                name: "TerritoryFromId",
                table: "TerritoryTerritory",
                newName: "TerritoryId");

            migrationBuilder.RenameIndex(
                name: "IX_TerritoryTerritory_TerritoryFromId",
                table: "TerritoryTerritory",
                newName: "IX_TerritoryTerritory_TerritoryId");

            migrationBuilder.AddColumn<int>(
                name: "TerritoryId1",
                table: "TerritoryTerritory",
                type: "integer",
                nullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory",
                columns: new[] { "TerritoryToId", "TerritoryId" });

            migrationBuilder.CreateIndex(
                name: "IX_TerritoryTerritory_TerritoryId1",
                table: "TerritoryTerritory",
                column: "TerritoryId1");

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
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryId",
                table: "TerritoryTerritory");

            migrationBuilder.DropForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryId1",
                table: "TerritoryTerritory");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TerritoryTerritory",
                table: "TerritoryTerritory");

            migrationBuilder.DropIndex(
                name: "IX_TerritoryTerritory_TerritoryId1",
                table: "TerritoryTerritory");

            migrationBuilder.DropColumn(
                name: "TerritoryId1",
                table: "TerritoryTerritory");

            migrationBuilder.RenameColumn(
                name: "TerritoryId",
                table: "TerritoryTerritory",
                newName: "TerritoryFromId");

            migrationBuilder.RenameIndex(
                name: "IX_TerritoryTerritory_TerritoryId",
                table: "TerritoryTerritory",
                newName: "IX_TerritoryTerritory_TerritoryFromId");

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

            migrationBuilder.AddForeignKey(
                name: "FK_TerritoryTerritory_Territories_TerritoryFromId",
                table: "TerritoryTerritory",
                column: "TerritoryFromId",
                principalTable: "Territories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
