using System;

namespace TicketBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public required string PassengerName { get; set; }
        public required string PassengerContact { get; set; }
        public DateTime BookingDate { get; set; }
        public decimal TotalPrice { get; set; }
        public int NumberOfSeats { get; set; }
        public required string Status { get; set; }
        public int FlightId { get; set; }
        public required Flight Flight { get; set; }
    }
}
