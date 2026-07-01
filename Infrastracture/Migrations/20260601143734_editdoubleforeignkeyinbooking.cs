using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class editdoubleforeignkeyinbooking : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Bookings_Trips_TripId1",
                table: "Bookings");

            migrationBuilder.DropIndex(
                name: "IX_Bookings_TripId1",
                table: "Bookings");

            migrationBuilder.DropColumn(
                name: "TripId1",
                table: "Bookings");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "TripId1",
                table: "Bookings",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_TripId1",
                table: "Bookings",
                column: "TripId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Bookings_Trips_TripId1",
                table: "Bookings",
                column: "TripId1",
                principalTable: "Trips",
                principalColumn: "Id");
        }
    }
}
