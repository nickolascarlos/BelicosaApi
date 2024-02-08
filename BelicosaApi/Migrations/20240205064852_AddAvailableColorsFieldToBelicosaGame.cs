using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BelicosaApi.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailableColorsFieldToBelicosaGame : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int[]>(
                name: "AvailableColors",
                table: "Games",
                type: "integer[]",
                nullable: false,
                defaultValue: new int[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "AvailableColors",
                table: "Games");
        }
    }
}
