using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class editconfiquration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Driver_LicenceNumber1",
                table: "DriverApplications");

            migrationBuilder.AlterColumn<string>(
                name: "LicenceNumber",
                table: "Drivers",
                type: "nvarchar(10)",
                maxLength: 10,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(5)",
                oldMaxLength: 5);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Driver_LicenceNumber1",
                table: "DriverApplications",
                sql: "LicenceNumber Like 'DR-[0-9][0-9][0-9][0-9]'");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropCheckConstraint(
                name: "CK_Driver_LicenceNumber1",
                table: "DriverApplications");

            migrationBuilder.AlterColumn<string>(
                name: "LicenceNumber",
                table: "Drivers",
                type: "nvarchar(5)",
                maxLength: 5,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(10)",
                oldMaxLength: 10);

            migrationBuilder.AddCheckConstraint(
                name: "CK_Driver_LicenceNumber1",
                table: "DriverApplications",
                sql: "LicenceNumber Like ' DR-[0-9][0-9][0-9][0-9]'");
        }
    }
}
