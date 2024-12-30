using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;
using TicketBookingSystem.Dialogs;

namespace TicketBookingSystem.Pages
{
    public partial class BookingsPage : Page
    {
        private readonly ApplicationDbContext _context;

        public BookingsPage()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            cmbStatusFilter.SelectedIndex = 0;
            LoadBookings();
        }

        private void LoadBookings(string statusFilter = null)
        {
            var query = _context.Bookings
                .Include(b => b.Flight)
                .AsQueryable();

            if (!string.IsNullOrEmpty(statusFilter))
            {
                query = query.Where(b => b.Status == statusFilter);
            }

            bookingsGrid.ItemsSource = query.ToList();
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new BookingDialog(_context);
            if (dialog.ShowDialog() == true)
            {
                _context.Bookings.Add(dialog.Booking);
                
                // Обновляем количество свободных мест
                var flight = dialog.Booking.Flight;
                flight.AvailableSeats -= dialog.Booking.NumberOfSeats;
                
                _context.SaveChanges();
                LoadBookings();
            }
        }

        private void btnCancelBooking_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooking = bookingsGrid.SelectedItem as Booking;
            if (selectedBooking == null)
            {
                MessageBox.Show("Пожалуйста, выберите бронирование для отмены");
                return;
            }

            if (selectedBooking.Status == "Отменено")
            {
                MessageBox.Show("Это бронирование уже отменено");
                return;
            }

            var result = MessageBox.Show(
                "Вы уверены, что хотите отменить это бронирование?",
                "Подтверждение отмены",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                // Возвращаем места в доступные
                var flight = _context.Flights.Find(selectedBooking.FlightId);
                flight.AvailableSeats += selectedBooking.NumberOfSeats;

                selectedBooking.Status = "Отменено";
                _context.SaveChanges();
                LoadBookings();
            }
        }

        private void cmbStatusFilter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string statusFilter = null;
            switch (cmbStatusFilter.SelectedIndex)
            {
                case 1: // Активные
                    statusFilter = "Подтверждено";
                    break;
                case 2: // Отмененные
                    statusFilter = "Отменено";
                    break;
            }
            LoadBookings(statusFilter);
        }
    }
}
