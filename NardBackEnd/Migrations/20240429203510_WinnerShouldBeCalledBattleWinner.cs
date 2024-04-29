using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class WinnerShouldBeCalledBattleWinner : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Winner",
                table: "Battles");

            migrationBuilder.AddColumn<int>(
                name: "BattleWinner",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleWinner",
                table: "Battles");

            migrationBuilder.AddColumn<string>(
                name: "Winner",
                table: "Battles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
