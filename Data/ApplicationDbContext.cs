using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ticketbooking.db");
        }
    }
}
