using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpdateBattleEntity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "P1Moves",
                table: "Battles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "P1StatBlock",
                table: "Battles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "P2Moves",
                table: "Battles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "P2StatBlock",
                table: "Battles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<int>(
                name: "PokemonId1",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "PokemonId2",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "P1Moves",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "P1StatBlock",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "P2Moves",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "P2StatBlock",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "PokemonId1",
                table: "Battles");

            migrationBuilder.DropColumn(
                name: "PokemonId2",
                table: "Battles");
        }
    }
}
