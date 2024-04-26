using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class UpperCaseDamageClass : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "damageClass",
                table: "Move",
                newName: "DamageClass");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "DamageClass",
                table: "Move",
                newName: "damageClass");
        }
    }
}
