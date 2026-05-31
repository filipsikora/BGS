using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace BGS.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RemoveMaxPlayers2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "MaxPlayers",
                table: "Games");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "MaxPlayers",
                table: "Games",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
