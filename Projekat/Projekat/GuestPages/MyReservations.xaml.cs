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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Projekat.GuestPages
{
    /// <summary>
    /// Interaction logic for MyReservations.xaml
    /// </summary>
    public partial class MyReservations : Page
    {
        private ReservationController reservationController = new ReservationController();
        private User guest = new User();

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
                List<Reservation> filteredReservations = reservationController.GetByStatus(selectedStatus);
                dataGrid.ItemsSource = filteredReservations;
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
                selectedReservation.Status = ReservationStatus.Canceled;
                reservationController.Update(selectedReservation);
                reservationController.UpdateApartmentReservationCancel(selectedReservation);
                RefreshDataGrid();
            }
        }

        private void RefreshDataGrid()
        {
            dataGrid.ItemsSource = reservationController.GetAllByGuestJmbg(guest.JMBG);
        }
    }


}
