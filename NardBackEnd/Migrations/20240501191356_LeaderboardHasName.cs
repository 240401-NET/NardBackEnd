using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class LeaderboardHasName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PokemonName",
                table: "Leaderboards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PokemonName",
                table: "Leaderboards");
        }
    }
}
