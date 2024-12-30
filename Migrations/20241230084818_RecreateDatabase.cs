using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class RecreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 48, 18, 231, DateTimeKind.Local).AddTicks(1887), new DateTime(2024, 12, 31, 11, 48, 18, 229, DateTimeKind.Local).AddTicks(4430) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 48, 18, 231, DateTimeKind.Local).AddTicks(9616), new DateTime(2024, 12, 31, 11, 48, 18, 231, DateTimeKind.Local).AddTicks(9610) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 1, 13, 48, 18, 231, DateTimeKind.Local).AddTicks(2965), new DateTime(2025, 1, 1, 11, 48, 18, 231, DateTimeKind.Local).AddTicks(2962) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 2, 14, 48, 18, 231, DateTimeKind.Local).AddTicks(9621), new DateTime(2025, 1, 2, 11, 48, 18, 231, DateTimeKind.Local).AddTicks(9621) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
