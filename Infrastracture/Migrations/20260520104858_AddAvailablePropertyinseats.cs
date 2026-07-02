using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class AddAvailablePropertyinseats : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsAvailable",
                table: "Seats",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsAvailable",
                table: "Seats");
        }
    }
}
