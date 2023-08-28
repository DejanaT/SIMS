using Project.Model;
using Projekat.Controller;
using Projekat.Model;
using Projekat.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.GuestPages
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page
    {
        private ReservationController reservationController = new ReservationController();
        private User guest = new User();
        private HotelController hotelController = new HotelController();
        private ApartmentController apartmentController = new ApartmentController();

        public MyReservations(User user)
        {
            InitializeComponent();
            guest = user;
            dataGrid.ItemsSource = reservationController.GetAllByGuestJmbg(guest.JMBG);
        }

        private void Filter_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string selectedStatus = (filterComboBox.SelectedItem as ComboBoxItem)?.Tag.ToString();

            if (string.IsNullOrEmpty(selectedStatus))
            {
                dataGrid.ItemsSource = reservationController.GetAllByGuestJmbg(guest.JMBG);
            }
            else
            {
                List<Reservation> allReservations = reservationController.GetAllByGuestJmbg(guest.JMBG);
                List<Reservation> filtered = allReservations.Where(r => r.Status.ToString() == selectedStatus).ToList();

                dataGrid.ItemsSource = filtered;
            }
        }

        private void View_Click(object sender, RoutedEventArgs e)
        {
            var button = sender as Button;
            var reservation = button?.DataContext as Reservation;

            if (reservation != null && reservation.Status.ToString() == "Rejected")
            {

                RejectReason rejectReason = new RejectReason(reservation.ReasonReject);
                rejectReason.ShowDialog();
            }
        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {
            Reservation selectedReservation = (sender as Button)?.DataContext as Reservation;

            if (selectedReservation != null && (selectedReservation.Status == ReservationStatus.Pending || selectedReservation.Status == ReservationStatus.Accepted))
            {
                Apartment apartment = apartmentController.FindApartmentByReservation(selectedReservation);

                if (apartment != null)
                {
                    reservationController.CancelReservation(selectedReservation);
                    MessageBox.Show("Reservation has been successfully canceled.");
                    RefreshDataGrid();
                }
            }
        }



        private void RefreshDataGrid()
        {
            dataGrid.ItemsSource = reservationController.GetAllByGuestJmbg(guest.JMBG);
        }
    }


}
