using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class StatusEnum : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Battles");

            migrationBuilder.AddColumn<int>(
                name: "BattleStatus",
                table: "Battles",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "BattleStatus",
                table: "Battles");

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "Battles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
