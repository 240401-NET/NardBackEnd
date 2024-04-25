using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Moves_Pokemon_PokemonId",
                table: "Moves");

            migrationBuilder.DropIndex(
                name: "IX_Moves_PokemonId",
                table: "Moves");

            migrationBuilder.DropColumn(
                name: "PokemonId",
                table: "Moves");

            migrationBuilder.AddColumn<string>(
                name: "MovePool",
                table: "Pokemon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");

            migrationBuilder.AddColumn<string>(
                name: "Types",
                table: "Pokemon",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "[]");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MovePool",
                table: "Pokemon");

            migrationBuilder.DropColumn(
                name: "Types",
                table: "Pokemon");

            migrationBuilder.AddColumn<int>(
                name: "PokemonId",
                table: "Moves",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Moves_PokemonId",
                table: "Moves",
                column: "PokemonId");

            migrationBuilder.AddForeignKey(
                name: "FK_Moves_Pokemon_PokemonId",
                table: "Moves",
                column: "PokemonId",
                principalTable: "Pokemon",
                principalColumn: "Id");
        }
    }
}
