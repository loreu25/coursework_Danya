using System.Linq;
using System.Windows;
using System.Windows.Controls;
using TicketBookingSystem.Data;
using TicketBookingSystem.Models;
using TicketBookingSystem.Dialogs;

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
            flightsGrid.ItemsSource = _context.Flights.ToList();
        }

        private void btnAddFlight_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new FlightDialog();
            if (dialog.ShowDialog() == true)
            {
                _context.Flights.Add(dialog.Flight);
                _context.SaveChanges();
                LoadFlights();
            }
        }

        private void btnEditFlight_Click(object sender, RoutedEventArgs e)
        {
            var selectedFlight = flightsGrid.SelectedItem as Flight;
            if (selectedFlight == null)
            {
                MessageBox.Show("Пожалуйста, выберите рейс для редактирования");
                return;
            }

            var dialog = new FlightDialog(selectedFlight);
            if (dialog.ShowDialog() == true)
            {
                _context.SaveChanges();
                LoadFlights();
            }
        }

        private void btnDeleteFlight_Click(object sender, RoutedEventArgs e)
        {
            var selectedFlight = flightsGrid.SelectedItem as Flight;
            if (selectedFlight == null)
            {
                MessageBox.Show("Пожалуйста, выберите рейс для удаления");
                return;
            }

            var result = MessageBox.Show("Вы уверены, что хотите удалить этот рейс?", 
                "Подтверждение удаления", 
                MessageBoxButton.YesNo, 
                MessageBoxImage.Question);

            if (result == MessageBoxResult.Yes)
            {
                _context.Flights.Remove(selectedFlight);
                _context.SaveChanges();
                LoadFlights();
            }
        }
    }
}
