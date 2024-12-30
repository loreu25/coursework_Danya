using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Data
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Flight> Flights { get; set; }
        public DbSet<EconomyFlight> EconomyFlights { get; set; }
        public DbSet<BusinessFlight> BusinessFlights { get; set; }
        public DbSet<Booking> Bookings { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source=ticketbooking.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Настраиваем дискриминатор для различения типов рейсов
            modelBuilder.Entity<Flight>()
                .HasDiscriminator<string>("FlightType")
                .HasValue<EconomyFlight>("Economy")
                .HasValue<BusinessFlight>("Business");

            // Настраиваем отношения
            modelBuilder.Entity<Booking>()
                .HasOne(b => b.Flight)
                .WithMany()
                .HasForeignKey("FlightId")
                .OnDelete(DeleteBehavior.Cascade);

            // Добавляем тестовые данные
            modelBuilder.Entity<EconomyFlight>().HasData(
                new EconomyFlight
                {
                    Id = 1,
                    FlightNumber = "AA101",
                    DepartureCity = "Москва",
                    ArrivalCity = "Санкт-Петербург",
                    DepartureTime = System.DateTime.Now.AddDays(1),
                    ArrivalTime = System.DateTime.Now.AddDays(1).AddHours(2),
                    BasePrice = 5000,
                    TotalSeats = 150,
                    AvailableSeats = 150,
                    FlightType = "Economy"
                },
                new EconomyFlight
                {
                    Id = 3,
                    FlightNumber = "AA102",
                    DepartureCity = "Казань",
                    ArrivalCity = "Москва",
                    DepartureTime = System.DateTime.Now.AddDays(2),
                    ArrivalTime = System.DateTime.Now.AddDays(2).AddHours(2),
                    BasePrice = 4500,
                    TotalSeats = 150,
                    AvailableSeats = 150,
                    FlightType = "Economy"
                }
            );

            modelBuilder.Entity<BusinessFlight>().HasData(
                new BusinessFlight
                {
                    Id = 2,
                    FlightNumber = "BB202",
                    DepartureCity = "Санкт-Петербург",
                    ArrivalCity = "Москва",
                    DepartureTime = System.DateTime.Now.AddDays(1),
                    ArrivalTime = System.DateTime.Now.AddDays(1).AddHours(2),
                    BasePrice = 10000,
                    TotalSeats = 50,
                    AvailableSeats = 50,
                    FlightType = "Business"
                },
                new BusinessFlight
                {
                    Id = 4,
                    FlightNumber = "BB203",
                    DepartureCity = "Москва",
                    ArrivalCity = "Сочи",
                    DepartureTime = System.DateTime.Now.AddDays(3),
                    ArrivalTime = System.DateTime.Now.AddDays(3).AddHours(3),
                    BasePrice = 15000,
                    TotalSeats = 50,
                    AvailableSeats = 50,
                    FlightType = "Business"
                }
            );
        }
    }
}
