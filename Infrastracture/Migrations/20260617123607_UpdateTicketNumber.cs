using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class UpdateTicketNumber : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Number",
                table: "Tickets",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<int>(
                name: "Number",
                table: "Tickets",
                type: "int",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");
        }
    }
}
