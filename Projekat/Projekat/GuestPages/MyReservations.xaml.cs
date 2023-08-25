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
    
    }


}
