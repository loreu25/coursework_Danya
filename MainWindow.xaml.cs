using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using TicketBookingSystem.Pages;

namespace TicketBookingSystem;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
        // Инициализация базы данных при запуске
        using (var context = new Data.ApplicationDbContext())
        {
            context.Database.EnsureCreated();
        }

        // Показываем страницу рейсов при запуске
        MainFrame.Navigate(new FlightsPage());
    }

    private void btnFlights_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new FlightsPage());
    }

    private void btnBookings_Click(object sender, RoutedEventArgs e)
    {
        MainFrame.Navigate(new BookingsPage());
    }
}