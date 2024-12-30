using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class UpdateFlightInheritance : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 35, 1, 359, DateTimeKind.Local).AddTicks(3961), new DateTime(2024, 12, 31, 11, 35, 1, 357, DateTimeKind.Local).AddTicks(4104) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 35, 1, 360, DateTimeKind.Local).AddTicks(3246), new DateTime(2024, 12, 31, 11, 35, 1, 360, DateTimeKind.Local).AddTicks(3240) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 1, 13, 35, 1, 359, DateTimeKind.Local).AddTicks(5284), new DateTime(2025, 1, 1, 11, 35, 1, 359, DateTimeKind.Local).AddTicks(5279) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 2, 14, 35, 1, 360, DateTimeKind.Local).AddTicks(3253), new DateTime(2025, 1, 2, 11, 35, 1, 360, DateTimeKind.Local).AddTicks(3252) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 32, 25, 438, DateTimeKind.Local).AddTicks(9313), new DateTime(2024, 12, 31, 11, 32, 25, 437, DateTimeKind.Local).AddTicks(3570) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 32, 25, 439, DateTimeKind.Local).AddTicks(6555), new DateTime(2024, 12, 31, 11, 32, 25, 439, DateTimeKind.Local).AddTicks(6550) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 1, 13, 32, 25, 439, DateTimeKind.Local).AddTicks(406), new DateTime(2025, 1, 1, 11, 32, 25, 439, DateTimeKind.Local).AddTicks(403) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 2, 14, 32, 25, 439, DateTimeKind.Local).AddTicks(6560), new DateTime(2025, 1, 2, 11, 32, 25, 439, DateTimeKind.Local).AddTicks(6559) });
        }
    }
}
