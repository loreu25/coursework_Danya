using System;

namespace TicketBookingSystem.Models
{
    public abstract class Flight
    {
        public int Id { get; set; }
        public required string FlightNumber { get; set; }
        public required string DepartureCity { get; set; }
        public required string ArrivalCity { get; set; }
        public DateTime DepartureTime { get; set; }
        public DateTime ArrivalTime { get; set; }
        public decimal BasePrice { get; set; }
        public int TotalSeats { get; set; }
        public int AvailableSeats { get; set; }
        public required string FlightType { get; set; }

        public abstract decimal CalculatePrice();
    }
}
