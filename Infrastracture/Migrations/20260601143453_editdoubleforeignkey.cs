using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Infrastracture.Migrations
{
    /// <inheritdoc />
    public partial class editdoubleforeignkey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BookingSeats_Bookings_BookingId1",
                table: "BookingSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_BookingSeats_Seats_SeatsId1",
                table: "BookingSeats");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Buses_BusId1",
                table: "Trips");

            migrationBuilder.DropForeignKey(
                name: "FK_Trips_Routes_RoutesId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_BusId1",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_Trips_RoutesId",
                table: "Trips");

            migrationBuilder.DropIndex(
                name: "IX_BookingSeats_BookingId1",
                table: "BookingSeats");

            migrationBuilder.DropIndex(
                name: "IX_BookingSeats_SeatsId1",
                table: "BookingSeats");

            migrationBuilder.DropColumn(
                name: "BusId1",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "RoutesId",
                table: "Trips");

            migrationBuilder.DropColumn(
                name: "BookingId1",
                table: "BookingSeats");

            migrationBuilder.DropColumn(
                name: "SeatsId1",
                table: "BookingSeats");

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

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.AddColumn<int>(
                name: "BusId1",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "RoutesId",
                table: "Trips",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "BookingId1",
                table: "BookingSeats",
                type: "int",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "SeatsId1",
                table: "BookingSeats",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Trips_BusId1",
                table: "Trips",
                column: "BusId1");

            migrationBuilder.CreateIndex(
                name: "IX_Trips_RoutesId",
                table: "Trips",
                column: "RoutesId");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_BookingId1",
                table: "BookingSeats",
                column: "BookingId1");

            migrationBuilder.CreateIndex(
                name: "IX_BookingSeats_SeatsId1",
                table: "BookingSeats",
                column: "SeatsId1");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSeats_Bookings_BookingId1",
                table: "BookingSeats",
                column: "BookingId1",
                principalTable: "Bookings",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BookingSeats_Seats_SeatsId1",
                table: "BookingSeats",
                column: "SeatsId1",
                principalTable: "Seats",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Buses_BusId1",
                table: "Trips",
                column: "BusId1",
                principalTable: "Buses",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Trips_Routes_RoutesId",
                table: "Trips",
                column: "RoutesId",
                principalTable: "Routes",
                principalColumn: "Id");
        }
    }
}
