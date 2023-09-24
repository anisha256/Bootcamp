using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Bootcamp.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class add_isavailable_defaultvalue : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Items",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Items");
        }
    }
}
