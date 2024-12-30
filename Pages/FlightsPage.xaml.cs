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
                using (var newContext = new ApplicationDbContext())
                {
                    // Получаем актуальную версию рейса из базы
                    var currentFlight = newContext.Flights.Find(flight.Id);
                    if (currentFlight == null)
                    {
                        MessageBox.Show("Рейс не найден в базе данных", "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
                        return;
                    }

                    var dialog = new FlightDialog(newContext, currentFlight);
                    if (dialog.ShowDialog() == true)
                    {
                        newContext.SaveChanges();
                        LoadFlights();
                    }
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
                var selectedFlight = dgFlights.SelectedItem as Flight;
                if (selectedFlight == null)
                {
                    MessageBox.Show("Выберите рейс для удаления", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    return;
                }

                var result = MessageBox.Show($"Вы уверены, что хотите удалить рейс {selectedFlight.FlightNumber}?",
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
                            .Any(b => b.FlightId == selectedFlight.Id && b.Status == "Активен");

                        if (hasActiveBookings)
                        {
                            MessageBox.Show("Невозможно удалить рейс, так как для него существуют активные бронирования.",
                                "Ошибка",
                                MessageBoxButton.OK,
                                MessageBoxImage.Warning);
                            return;
                        }

                        // Получаем актуальный объект рейса из базы данных
                        var flightToDelete = newContext.Flights.Find(selectedFlight.Id);
                        if (flightToDelete != null)
                        {
                            // Удаляем все бронирования для этого рейса (и отмененные, и активные)
                            var bookings = newContext.Bookings
                                .Where(b => b.FlightId == selectedFlight.Id)
                                .ToList();

                            if (bookings.Any())
                            {
                                newContext.Bookings.RemoveRange(bookings);
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
