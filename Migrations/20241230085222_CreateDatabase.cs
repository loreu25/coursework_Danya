using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TicketBookingSystem.Migrations
{
    /// <inheritdoc />
    public partial class CreateDatabase : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 1,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 52, 21, 888, DateTimeKind.Local).AddTicks(546), new DateTime(2024, 12, 31, 11, 52, 21, 886, DateTimeKind.Local).AddTicks(4023) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 2,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2024, 12, 31, 13, 52, 21, 888, DateTimeKind.Local).AddTicks(8099), new DateTime(2024, 12, 31, 11, 52, 21, 888, DateTimeKind.Local).AddTicks(8092) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 3,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 1, 13, 52, 21, 888, DateTimeKind.Local).AddTicks(1895), new DateTime(2025, 1, 1, 11, 52, 21, 888, DateTimeKind.Local).AddTicks(1889) });

            migrationBuilder.UpdateData(
                table: "Flights",
                keyColumn: "Id",
                keyValue: 4,
                columns: new[] { "ArrivalTime", "DepartureTime" },
                values: new object[] { new DateTime(2025, 1, 2, 14, 52, 21, 888, DateTimeKind.Local).AddTicks(8104), new DateTime(2025, 1, 2, 11, 52, 21, 888, DateTimeKind.Local).AddTicks(8103) });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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
    }
}
