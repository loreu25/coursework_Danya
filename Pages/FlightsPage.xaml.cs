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
                var flights = _context.Flights
                    .OrderBy(f => f.DepartureTime)
                    .ToList();
                dgFlights.ItemsSource = flights;
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
            var button = (Button)sender;
            var flight = (Flight)button.DataContext;

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот рейс?",
                "Подтверждение",
                MessageBoxButton.YesNo,
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    _context.Flights.Remove(flight);
                    _context.SaveChanges();
                    LoadFlights();
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
}
