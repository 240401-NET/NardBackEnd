using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NardBackEnd.Migrations
{
    /// <inheritdoc />
    public partial class TypeModelAdd : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Types",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Fire = table.Column<float>(type: "real", nullable: false),
                    Water = table.Column<float>(type: "real", nullable: false),
                    Grass = table.Column<float>(type: "real", nullable: false),
                    Electric = table.Column<float>(type: "real", nullable: false),
                    Psychic = table.Column<float>(type: "real", nullable: false),
                    Dark = table.Column<float>(type: "real", nullable: false),
                    Fighting = table.Column<float>(type: "real", nullable: false),
                    Flying = table.Column<float>(type: "real", nullable: false),
                    Ground = table.Column<float>(type: "real", nullable: false),
                    Rock = table.Column<float>(type: "real", nullable: false),
                    Steel = table.Column<float>(type: "real", nullable: false),
                    Ice = table.Column<float>(type: "real", nullable: false),
                    Bug = table.Column<float>(type: "real", nullable: false),
                    Poison = table.Column<float>(type: "real", nullable: false),
                    Ghost = table.Column<float>(type: "real", nullable: false),
                    Dragon = table.Column<float>(type: "real", nullable: false),
                    Fairy = table.Column<float>(type: "real", nullable: false),
                    Normal = table.Column<float>(type: "real", nullable: false),
                    Unknown = table.Column<float>(type: "real", nullable: false),
                    Shadow = table.Column<float>(type: "real", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Types", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Types");
        }
    }
}
