using Project.Model;
using Projekat.Controller;
using Projekat.Model;
using Projekat.Model.Enums;
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
using System.Windows.Shapes;

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for RejectReason.xaml
    /// </summary>
    public partial class RejectReason : Window
    {
        private Reservation reservation = new Reservation();
        private ReservationController reservationController = new ReservationController();
        private ApartmentController apartmentController = new ApartmentController();

        public RejectReason(Reservation selected)
        {
            InitializeComponent();
            reservation = selected;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {

            Apartment apartment = apartmentController.FindApartmentByReservation(reservation);

            if (apartment != null)
            {
                reservation.ReasonReject = reasonTextBox.Text;
                reservationController.RejectReservation(reservation);
                MessageBox.Show("Reservation has been successfully rejected.");
            }

            DialogResult = true;
            Close();

            
        }
    }
}
