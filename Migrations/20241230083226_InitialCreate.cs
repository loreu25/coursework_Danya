using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Flights",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightNumber = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureCity = table.Column<string>(type: "TEXT", nullable: false),
                    ArrivalCity = table.Column<string>(type: "TEXT", nullable: false),
                    DepartureTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    ArrivalTime = table.Column<DateTime>(type: "TEXT", nullable: false),
                    BasePrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    TotalSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    AvailableSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    FlightType = table.Column<string>(type: "TEXT", maxLength: 8, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Flights", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Bookings",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    FlightId = table.Column<int>(type: "INTEGER", nullable: false),
                    PassengerName = table.Column<string>(type: "TEXT", nullable: false),
                    PassengerContact = table.Column<string>(type: "TEXT", nullable: false),
                    NumberOfSeats = table.Column<int>(type: "INTEGER", nullable: false),
                    TotalPrice = table.Column<decimal>(type: "TEXT", nullable: false),
                    BookingDate = table.Column<DateTime>(type: "TEXT", nullable: false),
                    Status = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Bookings", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Bookings_Flights_FlightId",
                        column: x => x.FlightId,
                        principalTable: "Flights",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Flights",
                columns: new[] { "Id", "ArrivalCity", "ArrivalTime", "AvailableSeats", "BasePrice", "DepartureCity", "DepartureTime", "FlightNumber", "FlightType", "TotalSeats" },
                values: new object[,]
                {
                    { 1, "Санкт-Петербург", new DateTime(2024, 12, 31, 13, 32, 25, 438, DateTimeKind.Local).AddTicks(9313), 150, 5000m, "Москва", new DateTime(2024, 12, 31, 11, 32, 25, 437, DateTimeKind.Local).AddTicks(3570), "AA101", "Economy", 150 },
                    { 2, "Москва", new DateTime(2024, 12, 31, 13, 32, 25, 439, DateTimeKind.Local).AddTicks(6555), 50, 10000m, "Санкт-Петербург", new DateTime(2024, 12, 31, 11, 32, 25, 439, DateTimeKind.Local).AddTicks(6550), "BB202", "Business", 50 },
                    { 3, "Москва", new DateTime(2025, 1, 1, 13, 32, 25, 439, DateTimeKind.Local).AddTicks(406), 150, 4500m, "Казань", new DateTime(2025, 1, 1, 11, 32, 25, 439, DateTimeKind.Local).AddTicks(403), "AA102", "Economy", 150 },
                    { 4, "Сочи", new DateTime(2025, 1, 2, 14, 32, 25, 439, DateTimeKind.Local).AddTicks(6560), 50, 15000m, "Москва", new DateTime(2025, 1, 2, 11, 32, 25, 439, DateTimeKind.Local).AddTicks(6559), "BB203", "Business", 50 }
                });

            migrationBuilder.CreateIndex(
                name: "IX_Bookings_FlightId",
                table: "Bookings",
                column: "FlightId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Bookings");

            migrationBuilder.DropTable(
                name: "Flights");
        }
    }
}
