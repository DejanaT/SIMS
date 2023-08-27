using Project.Model;
using Projekat.Controller;
using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

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
            reservations = reservationController.GetReservationsByHostJmbg(host.JMBG, hotels);
            dataGrid.ItemsSource = reservations;
        }
    }
}
