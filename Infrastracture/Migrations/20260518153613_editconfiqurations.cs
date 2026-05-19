using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class editconfiqurations : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Driver_LicenceNumber",
                table: "Drivers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Driver_LicenceNumber",
                table: "Drivers",
                sql: "LicenceNumber Like 'DR-[0-9][0-9][0-9][0-9]'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Driver_LicenceNumber",
                table: "Drivers");

            migrationBuilder.AddCheckConstraint(
                name: "CK_Driver_LicenceNumber",
                table: "Drivers",
                sql: "LicenceNumber Like ' DR-[0-9][0-9][0-9][0-9]'");
        }
    }
}
