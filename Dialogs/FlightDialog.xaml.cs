using System;
using System.Windows;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Dialogs
{
    public partial class FlightDialog : Window
    {
        public Flight Flight { get; private set; }
        private bool _isEditMode;

        public FlightDialog(Flight flight = null)
        {
            InitializeComponent();
            _isEditMode = flight != null;
            Flight = flight ?? new Flight();

            if (_isEditMode)
            {
                LoadFlightData();
            }
        }

        private void LoadFlightData()
        {
            txtFlightNumber.Text = Flight.FlightNumber;
            txtDepartureCity.Text = Flight.DepartureCity;
            txtArrivalCity.Text = Flight.ArrivalCity;
            dpDepartureDate.SelectedDate = Flight.DepartureTime.Date;
            txtDepartureTime.Text = Flight.DepartureTime.ToString("HH:mm");
            dpArrivalDate.SelectedDate = Flight.ArrivalTime.Date;
            txtArrivalTime.Text = Flight.ArrivalTime.ToString("HH:mm");
            txtTicketPrice.Text = Flight.TicketPrice.ToString();
            txtTotalSeats.Text = Flight.TotalSeats.ToString();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            if (!ValidateInput())
            {
                return;
            }

            try
            {
                Flight.FlightNumber = txtFlightNumber.Text;
                Flight.DepartureCity = txtDepartureCity.Text;
                Flight.ArrivalCity = txtArrivalCity.Text;
                
                // Парсинг даты и времени отправления
                var departureDate = dpDepartureDate.SelectedDate.Value;
                var departureTime = TimeSpan.Parse(txtDepartureTime.Text);
                Flight.DepartureTime = departureDate.Add(departureTime);

                // Парсинг даты и времени прибытия
                var arrivalDate = dpArrivalDate.SelectedDate.Value;
                var arrivalTime = TimeSpan.Parse(txtArrivalTime.Text);
                Flight.ArrivalTime = arrivalDate.Add(arrivalTime);

                Flight.TicketPrice = decimal.Parse(txtTicketPrice.Text);
                Flight.TotalSeats = int.Parse(txtTotalSeats.Text);
                
                if (!_isEditMode)
                {
                    Flight.AvailableSeats = Flight.TotalSeats;
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении данных: {ex.Message}", 
                    "Ошибка", 
                    MessageBoxButton.OK, 
                    MessageBoxImage.Error);
            }
        }

        private bool ValidateInput()
        {
            if (string.IsNullOrWhiteSpace(txtFlightNumber.Text))
            {
                ShowError("Введите номер рейса");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtDepartureCity.Text))
            {
                ShowError("Введите город отправления");
                return false;
            }

            if (string.IsNullOrWhiteSpace(txtArrivalCity.Text))
            {
                ShowError("Введите город прибытия");
                return false;
            }

            if (!dpDepartureDate.SelectedDate.HasValue)
            {
                ShowError("Выберите дату отправления");
                return false;
            }

            if (!dpArrivalDate.SelectedDate.HasValue)
            {
                ShowError("Выберите дату прибытия");
                return false;
            }

            if (!TimeSpan.TryParse(txtDepartureTime.Text, out _))
            {
                ShowError("Введите корректное время отправления (HH:mm)");
                return false;
            }

            if (!TimeSpan.TryParse(txtArrivalTime.Text, out _))
            {
                ShowError("Введите корректное время прибытия (HH:mm)");
                return false;
            }

            if (!decimal.TryParse(txtTicketPrice.Text, out decimal price) || price <= 0)
            {
                ShowError("Введите корректную цену билета");
                return false;
            }

            if (!int.TryParse(txtTotalSeats.Text, out int seats) || seats <= 0)
            {
                ShowError("Введите корректное количество мест");
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
