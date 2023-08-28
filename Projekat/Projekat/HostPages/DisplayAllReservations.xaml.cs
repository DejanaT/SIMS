using Project.Model;
using Projekat.Controller;
using Projekat.Model;
using Projekat.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Controls;

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for DisplayAllReservations.xaml
    /// </summary>
    public partial class DisplayAllReservations : Page
    {
        private User host = new User();
        private ReservationController reservationController = new ReservationController();
        private HotelController hotelController = new HotelController();
        private List<Hotel> hotels = new List<Hotel>();
        List<Reservation> reservations = new List<Reservation>();

        public DisplayAllReservations(User user)
        {
            InitializeComponent();
            host = user;
            hotels = hotelController.GetAllByHostJmbg(host.JMBG);
            reservations = reservationController.GetAllByHostJmbg(host.JMBG);
            dataGrid.ItemsSource = reservations;
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ComboBoxItem filterSelectedItem = (ComboBoxItem)filterComboBox.SelectedItem;
            string reservationFilter = filterSelectedItem.Tag.ToString();

            List<Reservation> reservations = reservationController.GetAllByHostJmbg(host.JMBG);
            List<Reservation> filtered = new List<Reservation>();

            if (reservationFilter == "Pending")
            {
                filtered = reservations.Where(r => r.Status == ReservationStatus.Pending).ToList();
            }
            else if (reservationFilter == "Accepted")
            {
                filtered = reservations.Where(r => r.Status == ReservationStatus.Accepted).ToList();
            }
            else if (reservationFilter == "Show all")
            {
                filtered = reservations;
            }

            dataGrid.ItemsSource = filtered;
        }

    }
}
