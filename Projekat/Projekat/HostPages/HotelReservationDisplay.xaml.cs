using Project.Model;
using Projekat.Controller;
using Projekat.Model;
using Projekat.Model.Enums;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for HotelReservationDisplay.xaml
    /// </summary>
    public partial class HotelReservationDisplay : Window
    {
        private Hotel selected;
        private ReservationController reservationController = new ReservationController();
        private HotelController hotelController = new HotelController();
        private ApartmentController apartmentController = new ApartmentController();
        private User host = new User();

        public HotelReservationDisplay(Hotel hotel, User user)
        {
            InitializeComponent();
            selected = hotel;
            LoadReservations();
            host = user;
        }

        private void LoadReservations()
        {
            List<Hotel> hotelList = new List<Hotel> { selected };
            List<Reservation> reservations = reservationController.GetReservationsByHostJmbg(host.JMBG, hotelList);
            reservationListView.ItemsSource = reservations;
        }

        private void Accept_Click(object sender, RoutedEventArgs e)
        {
            Button button = sender as Button;
            Reservation reservation = button.DataContext as Reservation;

            if (reservation != null && reservation.Status == ReservationStatus.Pending)
            {
                Apartment apartment = apartmentController.FindApartmentByReservation(reservation);

                if (apartment != null)
                {
                    reservationController.ApproveReservation(reservation);
                    MessageBox.Show("Reservation has been successfully approved!");
                    reservationListView.Items.Refresh();
                }
            }

        }

        private void Reject_Click(object sender, RoutedEventArgs e)
        {
            var selectedReservation = ((Button)sender).DataContext as Reservation;
            List<Hotel> hotelList = new List<Hotel> { selected };
            List<Reservation> reservations = reservationController.GetReservationsByHostJmbg(host.JMBG, hotelList);

            if (selectedReservation != null)
            {
                var rejectReasonDialog = new RejectReason(selectedReservation);
                var result = rejectReasonDialog.ShowDialog();

                reservationListView.ItemsSource = null;
                reservationListView.ItemsSource = reservations;

            }

        }
    }
}
