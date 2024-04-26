using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class MoveDamageClassAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "damageClass",
                table: "Move",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "damageClass",
                table: "Move");
        }
    }
}
