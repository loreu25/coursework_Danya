using System;
using System.Linq;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Dialogs
{
    public partial class BookingDialog : Window
    {
        private readonly ApplicationDbContext _context;
        private Flight _selectedFlight = null!;
        public Booking? Booking { get; private set; }

        public BookingDialog(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            LoadFlights();
        }

        private void LoadFlights()
        {
            try
            {
                var flights = _context.Flights
                    .Where(f => f.AvailableSeats > 0 && f.DepartureTime > DateTime.Now)
                    .ToList();

                if (!flights.Any())
                {
                    MessageBox.Show("Нет доступных рейсов.", "Информация", MessageBoxButton.OK, MessageBoxImage.Information);
                    DialogResult = false;
                    Close();
                    return;
                }

                cmbFlights.ItemsSource = flights;
                cmbFlights.DisplayMemberPath = "FlightNumber";
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при загрузке рейсов: {ex.Message}");
                DialogResult = false;
                Close();
            }
        }

        private void UpdateFlightInfo()
        {
            if (_selectedFlight != null)
            {
                txtDepartureCity.Text = _selectedFlight.DepartureCity;
                txtArrivalCity.Text = _selectedFlight.ArrivalCity;
                txtDepartureTime.Text = _selectedFlight.DepartureTime.ToString("g");
                txtArrivalTime.Text = _selectedFlight.ArrivalTime.ToString("g");
                txtFlightType.Text = _selectedFlight.FlightType;
                txtBasePrice.Text = _selectedFlight.BasePrice.ToString("C");
                txtAvailableSeats.Text = _selectedFlight.AvailableSeats.ToString();

                // Обновляем общую стоимость
                UpdateTotalPrice();
            }
        }

        private void cmbFlights_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            if (cmbFlights.SelectedItem is Flight flight)
            {
                _selectedFlight = flight;
                UpdateFlightInfo();
            }
        }

        private void txtNumberOfSeats_TextChanged(object sender, System.Windows.Controls.TextChangedEventArgs e)
        {
            UpdateTotalPrice();
        }

        private void UpdateTotalPrice()
        {
            if (_selectedFlight != null && int.TryParse(txtNumberOfSeats.Text, out int numberOfSeats))
            {
                decimal totalPrice = _selectedFlight.CalculatePrice() * numberOfSeats;
                txtTotalPrice.Text = totalPrice.ToString("C");
            }
            else
            {
                txtTotalPrice.Text = "0.00";
            }
        }

        private void btnBook_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (_selectedFlight == null)
                {
                    MessageBox.Show("Пожалуйста, выберите рейс");
                    return;
                }

                if (!ValidateInput())
                    return;

                Booking = new Booking
                {
                    PassengerName = txtPassengerName.Text,
                    PassengerContact = txtPassengerContact.Text,
                    BookingDate = DateTime.Now,
                    NumberOfSeats = int.Parse(txtNumberOfSeats.Text),
                    TotalPrice = _selectedFlight.CalculatePrice() * int.Parse(txtNumberOfSeats.Text),
                    Status = "Подтверждено",
                    Flight = _selectedFlight,
                    FlightId = _selectedFlight.Id
                };

                DialogResult = true;
                Close();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при создании бронирования: {ex.Message}");
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtPassengerName.Text))
            {
                ShowError("Введите имя пассажира");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtPassengerContact.Text))
            {
                ShowError("Введите контактные данные");
                return false;
            }

            int numberOfSeats;
            if (!int.TryParse(txtNumberOfSeats.Text, out numberOfSeats) || numberOfSeats <= 0)
            {
                ShowError("Введите корректное количество мест");
                return false;
            }

            if (numberOfSeats > _selectedFlight.AvailableSeats)
            {
                ShowError($"Недостаточно свободных мест. Доступно: {_selectedFlight.AvailableSeats}");
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
            Close();
        }
    }
}
