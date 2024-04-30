using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class LeaderboardWinLossInt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "WinLoss",
                table: "Leaderboards");

            migrationBuilder.AddColumn<int>(
                name: "Loss",
                table: "Leaderboards",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Win",
                table: "Leaderboards",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Loss",
                table: "Leaderboards");

            migrationBuilder.DropColumn(
                name: "Win",
                table: "Leaderboards");

            migrationBuilder.AddColumn<string>(
                name: "WinLoss",
                table: "Leaderboards",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
