using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Data;
using TicketBookingSystem.Dialogs;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Pages
{
    public partial class FlightsPage : Page
    {
        private readonly ApplicationDbContext _context;

        public FlightsPage()
        {
            InitializeComponent();
            _context = new ApplicationDbContext();
            LoadFlights();
        }

        private void LoadFlights()
        {
            try
            {
                using (var context = new ApplicationDbContext())
                {
                    var flights = context.Flights
                        .OrderBy(f => f.DepartureTime)
                        .ToList();
                    dgFlights.ItemsSource = flights;
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке рейсов: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnAdd_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                var dialog = new FlightDialog(_context);
                if (dialog.ShowDialog() == true)
                {
                    var flight = dialog.Flight;
                    if (flight != null)
                    {
                        _context.Flights.Add(flight);
                        _context.SaveChanges();
                        LoadFlights();
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании рейса: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private void btnEdit_Click(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var flight = (Flight)button.DataContext;

            try
            {
                var dialog = new FlightDialog(_context, flight);
                if (dialog.ShowDialog() == true)
                {
                    _context.SaveChanges();
                    LoadFlights();
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

        private void btnDelete_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                Flight flight;
                
                if (sender is Button button)
                {
                    if (button.DataContext is Flight f)
                    {
                        flight = f;
                    }
                    else if (dgFlights.SelectedItem is Flight selectedFlight)
                    {
                        flight = selectedFlight;
                    }
                    else
                    {
                        MessageBox.Show("Выберите рейс для удаления", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        return;
                    }
                }
                else
                {
                    MessageBox.Show("Ошибка при получении данных рейса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                if (flight.Id == 0)
                {
                    MessageBox.Show("Некорректный ID рейса", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                    return;
                }

                var result = MessageBox.Show($"Вы уверены, что хотите удалить рейс {flight.FlightNumber}?",
                    "Подтверждение",
                    MessageBoxButton.YesNo,
                    MessageBoxImage.Question);

                if (result == MessageBoxResult.Yes)
                {
                    using (var newContext = new ApplicationDbContext())
                    {
                        // Проверяем только активные бронирования
                        var hasActiveBookings = newContext.Bookings
                            .AsNoTracking()
                            .Any(b => b.FlightId == flight.Id && b.Status != "Отменено");

                        if (hasActiveBookings)
                        {
                            MessageBox.Show("Невозможно удалить рейс, так как для него существуют активные бронирования.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }

                        // Получаем актуальный объект рейса из базы данных
                        var flightToDelete = newContext.Flights.Find(flight.Id);
                        if (flightToDelete != null)
                        {
                            // Удаляем все отмененные бронирования для этого рейса
                            var cancelledBookings = newContext.Bookings
                                .Where(b => b.FlightId == flight.Id && b.Status == "Отменено")
                                .ToList();

                            if (cancelledBookings.Any())
                            {
                                newContext.Bookings.RemoveRange(cancelledBookings);
                            }

                            // Удаляем сам рейс
                            newContext.Flights.Remove(flightToDelete);
                            newContext.SaveChanges();
                            LoadFlights();
                            MessageBox.Show("Рейс успешно удален", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                        }
                        else
                        {
                            MessageBox.Show("Рейс не найден в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при удалении рейса: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }
    }
}
