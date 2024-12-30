using System;

namespace TicketBookingSystem.Models
{
    public class Booking
    {
        public int Id { get; set; }
        public int FlightId { get; set; }
        public string PassengerName { get; set; }
        public string PassengerContact { get; set; }
        public int NumberOfSeats { get; set; }
        public decimal TotalPrice { get; set; }
        public DateTime BookingDate { get; set; }
        public string Status { get; set; } // "Pending", "Confirmed", "Cancelled"
        
        // Навигационное свойство
        public Flight Flight { get; set; }
    }
}
