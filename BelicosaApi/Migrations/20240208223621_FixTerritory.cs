using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class FixTerritory : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories");

            migrationBuilder.AlterColumn<int>(
                name: "OccupyingPlayerId",
                table: "Territories",
                type: "integer",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "integer");

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories",
                column: "OccupyingPlayerId",
                principalTable: "Players",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories");

            migrationBuilder.AlterColumn<int>(
                name: "OccupyingPlayerId",
                table: "Territories",
                type: "integer",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "integer",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Territories_Players_OccupyingPlayerId",
                table: "Territories",
                column: "OccupyingPlayerId",
                principalTable: "Players",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
