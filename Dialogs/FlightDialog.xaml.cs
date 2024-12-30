using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using Microsoft.EntityFrameworkCore;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;

namespace TicketBookingSystem.Dialogs
{
    public partial class FlightDialog : Window
    {
        private readonly ApplicationDbContext _context;
        private readonly Flight? _existingFlight;
        public Flight? Flight { get; private set; }

        // Конструктор для создания нового рейса
        public FlightDialog(ApplicationDbContext context)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _existingFlight = null;
            Title = "Новый рейс";
            cmbFlightType.SelectedIndex = 0;
        }

        // Конструктор для редактирования существующего рейса
        public FlightDialog(ApplicationDbContext context, Flight flight)
        {
            InitializeComponent();
            _context = context ?? throw new ArgumentNullException(nameof(context));
            _existingFlight = flight ?? throw new ArgumentNullException(nameof(flight));
            
            // Заполняем поля существующими данными
            txtFlightNumber.Text = flight.FlightNumber;
            txtDepartureCity.Text = flight.DepartureCity;
            txtArrivalCity.Text = flight.ArrivalCity;
            dpDepartureDate.SelectedDate = flight.DepartureTime.Date;
            txtDepartureTime.Text = flight.DepartureTime.TimeOfDay.ToString(@"hh\:mm");
            dpArrivalDate.SelectedDate = flight.ArrivalTime.Date;
            txtArrivalTime.Text = flight.ArrivalTime.TimeOfDay.ToString(@"hh\:mm");
            txtBasePrice.Text = flight.BasePrice.ToString();
            txtTotalSeats.Text = flight.TotalSeats.ToString();

            // Настраиваем тип рейса
            cmbFlightType.SelectedIndex = flight is BusinessFlight ? 1 : 0;

            Title = $"Редактирование рейса {flight.FlightNumber}";
            txtFlightNumber.IsEnabled = false; // Запрещаем менять номер существующего рейса
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (!ValidateInput())
                    return;

                var selectedItem = (ComboBoxItem)cmbFlightType.SelectedItem;
                var selectedType = selectedItem.Tag.ToString()!;
                var isBusinessFlight = selectedType == "Business";

                if (_existingFlight != null)
                {
                    // Проверяем, нужно ли менять тип рейса
                    bool needToChangeType = (isBusinessFlight && _existingFlight is EconomyFlight) ||
                                         (!isBusinessFlight && _existingFlight is BusinessFlight);

                    if (needToChangeType)
                    {
                        // Получаем количество забронированных мест перед созданием нового объекта
                        int bookedSeatsCount = _existingFlight.TotalSeats - _existingFlight.AvailableSeats;
                        int updatedTotalSeats = int.Parse(txtTotalSeats.Text);

                        // Создаем новый объект нужного типа с инициализацией всех обязательных полей
                        Flight = isBusinessFlight
                            ? new BusinessFlight
                            {
                                FlightNumber = _existingFlight.FlightNumber,
                                DepartureCity = txtDepartureCity.Text,
                                ArrivalCity = txtArrivalCity.Text,
                                FlightType = selectedType,
                                TotalSeats = updatedTotalSeats,
                                AvailableSeats = updatedTotalSeats - bookedSeatsCount,
                                BasePrice = decimal.Parse(txtBasePrice.Text),
                                Id = _existingFlight.Id
                            }
                            : new EconomyFlight
                            {
                                FlightNumber = _existingFlight.FlightNumber,
                                DepartureCity = txtDepartureCity.Text,
                                ArrivalCity = txtArrivalCity.Text,
                                FlightType = selectedType,
                                TotalSeats = updatedTotalSeats,
                                AvailableSeats = updatedTotalSeats - bookedSeatsCount,
                                BasePrice = decimal.Parse(txtBasePrice.Text),
                                Id = _existingFlight.Id
                            };

                        // Копируем существующие бронирования в новый объект
                        var bookings = _context.Bookings.Where(b => b.FlightId == _existingFlight.Id).ToList();
                        foreach (var booking in bookings)
                        {
                            booking.Flight = Flight;
                        }

                        // Удаляем старый рейс и добавляем новый
                        _context.Flights.Remove(_existingFlight);
                        _context.Flights.Add(Flight);
                    }
                    else
                    {
                        Flight = _existingFlight;
                    }

                    // Обновляем свойства рейса
                    Flight.DepartureCity = txtDepartureCity.Text;
                    Flight.ArrivalCity = txtArrivalCity.Text;
                    var departureDate = dpDepartureDate.SelectedDate!.Value;
                    var departureTime = TimeSpan.Parse(txtDepartureTime.Text);
                    Flight.DepartureTime = departureDate.Add(departureTime);
                    var arrivalDate = dpArrivalDate.SelectedDate!.Value;
                    var arrivalTime = TimeSpan.Parse(txtArrivalTime.Text);
                    Flight.ArrivalTime = arrivalDate.Add(arrivalTime);
                    Flight.BasePrice = decimal.Parse(txtBasePrice.Text);
                    Flight.FlightType = selectedType;

                    // Обновляем количество мест с учетом уже существующих бронирований
                    int newTotalSeats = int.Parse(txtTotalSeats.Text);
                    int currentBookedSeats = _context.Bookings
                        .Where(b => b.FlightId == Flight.Id && b.Status != "Отменен")
                        .Sum(b => b.NumberOfSeats);

                    Flight.TotalSeats = newTotalSeats;
                    Flight.AvailableSeats = newTotalSeats - currentBookedSeats;

                    // Явно помечаем объект как измененный
                    _context.Entry(Flight).State = EntityState.Modified;
                }
                else
                {
                    // Создаем новый рейс
                    int totalSeats = int.Parse(txtTotalSeats.Text);
                    Flight = isBusinessFlight
                        ? new BusinessFlight
                        {
                            FlightNumber = txtFlightNumber.Text,
                            DepartureCity = txtDepartureCity.Text,
                            ArrivalCity = txtArrivalCity.Text,
                            FlightType = selectedType,
                            TotalSeats = totalSeats,
                            AvailableSeats = totalSeats,
                            BasePrice = decimal.Parse(txtBasePrice.Text)
                        }
                        : new EconomyFlight
                        {
                            FlightNumber = txtFlightNumber.Text,
                            DepartureCity = txtDepartureCity.Text,
                            ArrivalCity = txtArrivalCity.Text,
                            FlightType = selectedType,
                            TotalSeats = totalSeats,
                            AvailableSeats = totalSeats,
                            BasePrice = decimal.Parse(txtBasePrice.Text)
                        };

                    var departureDate = dpDepartureDate.SelectedDate!.Value;
                    var departureTime = TimeSpan.Parse(txtDepartureTime.Text);
                    Flight.DepartureTime = departureDate.Add(departureTime);
                    var arrivalDate = dpArrivalDate.SelectedDate!.Value;
                    var arrivalTime = TimeSpan.Parse(txtArrivalTime.Text);
                    Flight.ArrivalTime = arrivalDate.Add(arrivalTime);
                }

                DialogResult = true;
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Ошибка при сохранении рейса: {ex.Message}",
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

            if (dpDepartureDate.SelectedDate == null)
            {
                ShowError("Выберите дату отправления");
                return false;
            }

            if (dpArrivalDate.SelectedDate == null)
            {
                ShowError("Выберите дату прибытия");
                return false;
            }

            TimeSpan departureTime, arrivalTime;
            if (!TimeSpan.TryParse(txtDepartureTime.Text, out departureTime))
            {
                ShowError("Введите корректное время отправления");
                return false;
            }

            if (!TimeSpan.TryParse(txtArrivalTime.Text, out arrivalTime))
            {
                ShowError("Введите корректное время прибытия");
                return false;
            }

            var departureDateTime = dpDepartureDate.SelectedDate.Value.Add(departureTime);
            var arrivalDateTime = dpArrivalDate.SelectedDate.Value.Add(arrivalTime);

            if (departureDateTime >= arrivalDateTime)
            {
                ShowError("Время прибытия должно быть позже времени отправления");
                return false;
            }

            if (!decimal.TryParse(txtBasePrice.Text, out decimal basePrice) || basePrice <= 0)
            {
                ShowError("Введите корректную базовую цену");
                return false;
            }

            if (!int.TryParse(txtTotalSeats.Text, out int totalSeats) || totalSeats <= 0)
            {
                ShowError("Введите корректное количество мест");
                return false;
            }

            // При редактировании проверяем, что новое количество мест не меньше количества забронированных
            if (_existingFlight != null)
            {
                int bookedSeats = _existingFlight.TotalSeats - _existingFlight.AvailableSeats;
                if (totalSeats < bookedSeats)
                {
                    ShowError($"Количество мест не может быть меньше количества забронированных мест ({bookedSeats})");
                    return false;
                }
            }

            // Проверяем уникальность номера рейса только при создании нового рейса
            if (_existingFlight == null && _context.Flights.Any(f => f.FlightNumber == txtFlightNumber.Text))
            {
                ShowError("Рейс с таким номером уже существует");
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
