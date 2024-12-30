using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Dialogs
{
    public partial class BookingDialog : Window
    {
        private readonly ApplicationDbContext _context;
        public Booking Booking { get; private set; }
        private Flight _selectedFlight;

        public BookingDialog(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context;
            LoadFlights();
        }

        private void LoadFlights()
        {
            var availableFlights = _context.Flights
                .Where(f => f.AvailableSeats > 0 && f.DepartureTime > DateTime.Now)
                .OrderBy(f => f.DepartureTime)
                .ToList();

            cmbFlights.ItemsSource = availableFlights;
        }

        private void UpdateFlightInfo()
        {
            if (_selectedFlight != null)
            {
                txtFlightInfo.Text = $"Маршрут: {_selectedFlight.DepartureCity} - {_selectedFlight.ArrivalCity}\n" +
                                   $"Отправление: {_selectedFlight.DepartureTime}\n" +
                                   $"Прибытие: {_selectedFlight.ArrivalTime}";
                txtAvailableSeats.Text = $"Доступно мест: {_selectedFlight.AvailableSeats}";
                txtTicketPrice.Text = $"Цена за место: {_selectedFlight.TicketPrice:C}";
                
                UpdateTotalPrice();
            }
        }

        private void UpdateTotalPrice()
        {
            if (_selectedFlight != null && int.TryParse(txtNumberOfSeats.Text, out int seats))
            {
                decimal totalPrice = _selectedFlight.TicketPrice * seats;
                txtTotalPrice.Text = $"Итого к оплате: {totalPrice:C}";
            }
            else
            {
                txtTotalPrice.Text = "Итого к оплате: 0 ₽";
            }
        }

        private void cmbFlights_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            _selectedFlight = cmbFlights.SelectedItem as Flight;
            UpdateFlightInfo();
        }

        private void txtNumberOfSeats_TextChanged(object sender, TextChangedEventArgs e)
        {
            UpdateTotalPrice();
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                int seats = int.Parse(txtNumberOfSeats.Text);
                decimal totalPrice = _selectedFlight.TicketPrice * seats;

                Booking = new Booking
                {
                    FlightId = _selectedFlight.Id,
                    Flight = _selectedFlight,
                    PassengerName = txtPassengerName.Text,
                    PassengerContact = txtPassengerContact.Text,
                    NumberOfSeats = seats,
                    TotalPrice = totalPrice,
                    BookingDate = DateTime.Now,
                    Status = "Подтверждено"
                };

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании бронирования: {ex.Message}",
                    "Ошибка",
                    MessageBoxButton.OK,
                    MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (_selectedFlight == null)
            {
                ShowError("Выберите рейс");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassengerName.Text))
            {
                ShowError("Введите ФИО пассажира");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassengerContact.Text))
            {
                ShowError("Введите контактные данные");
                return false;
            }

            if (!int.TryParse(txtNumberOfSeats.Text, out int seats))
            {
                ShowError("Введите корректное количество мест");
                return false;
            }

            if (seats <= 0)
            {
                ShowError("Количество мест должно быть больше нуля");
                return false;
            }

            if (seats > _selectedFlight.AvailableSeats)
            {
                ShowError($"Доступно только {_selectedFlight.AvailableSeats} мест");
                return false;
            }

            return true;
        }

        private void ShowError(string message)
        {
            MessageBox.Show(message, "Ошибка", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            DialogResult = false;
        }
    }
}
