using System;
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
            LoadBookings();
        }

        private void LoadBookings(string? statusFilter = null)
        {
            try
            {
                var query = _context.Bookings
                    .Include(b => b.Flight)
                    .AsQueryable();

                if (!string.IsNullOrEmpty(statusFilter))
                {
                    query = query.Where(b => b.Status == statusFilter);
                }

                var bookings = query
                    .OrderByDescending(b => b.BookingDate)
                    .ToList();

                dgBookings.ItemsSource = bookings;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке бронирований: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnAddBooking_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new BookingDialog(_context);
                if (dialog.ShowDialog() == true)
                {
                    var booking = dialog.Booking;
                    if (booking != null)
                    {
                        _context.Bookings.Add(booking);
                        booking.Flight.AvailableSeats -= booking.NumberOfSeats;
                        _context.SaveChanges();
                        LoadBookings();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании бронирования: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnEditFlight_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooking = dgBookings.SelectedItem as Booking;
            if (selectedBooking == null)
            {
                MessageBox.Show("Выберите бронирование для редактирования рейса",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            try
            {
                var dialog = new FlightDialog(_context, selectedBooking.Flight);
                if (dialog.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadBookings();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при редактировании рейса: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            var selectedBooking = dgBookings.SelectedItem as Booking;
            if (selectedBooking == null)
            {
                MessageBox.Show("Выберите бронирование для отмены",
                    "Предупреждение",
                    MessageBoxButton.OK,
                    MessageBoxImage.Warning);
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите отменить это бронирование?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    using (var newContext = new ApplicationDbContext())
                    {
                        // Получаем актуальные данные из базы
                        var booking = newContext.Bookings
                            .Include(b => b.Flight)
                            .First(b => b.Id == selectedBooking.Id);

                        // Получаем актуальные данные о рейсе
                        var flight = newContext.Flights.First(f => f.Id == booking.FlightId);

                        // Возвращаем места в рейс
                        flight.AvailableSeats += booking.NumberOfSeats;

                        // Отмечаем бронирование как отмененное
                        booking.Status = "Отменен";

                        // Явно говорим EF, что эти сущности изменились
                        newContext.Entry(booking).State = EntityState.Modified;
                        newContext.Entry(flight).State = EntityState.Modified;

                        newContext.SaveChanges();

                        var messageResult = MessageBox.Show("Бронирование успешно отменено",
                            "Информация",
                            MessageBoxButton.OK,
                            MessageBoxImage.Information);

                        // Обновляем данные в интерфейсе только после закрытия окна сообщения
                        if (messageResult == MessageBoxResult.OK)
                        {
                            LoadBookings();
                        }
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show($"Ошибка при отмене бронирования: {ex.Message}",
                        "Ошибка",
                        MessageBoxButton.OK,
                        MessageBoxImage.Error);
                }
            }
        }

        private void btnFilter_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            if (button == null) return;

            string? status = button.Tag?.ToString();
            LoadBookings(status);

            // Обновляем стили кнопок
            foreach (var child in filterButtons.Children)
            {
                if (child is Button filterButton)
                {
                    filterButton.Style = this.Resources[
                        filterButton == button ? "SelectedFilterButtonStyle" : "FilterButtonStyle"
                    ] as Style;
                }
            }
        }
    }
}
