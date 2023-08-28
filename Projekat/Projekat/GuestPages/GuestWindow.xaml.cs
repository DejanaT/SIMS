using Project.Model;
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

namespace Projekat.GuestPages
{
    /// <summary>
    /// Interaction logic for GuestWindow.xaml
    /// </summary>
    public partial class GuestWindow : Window
    {

        private User guest = new User();

        public GuestWindow(User user)
        {
            InitializeComponent();
            Name.Text = user.Name;
            Surname.Text = user.Surname;
            guest = user;

        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = Menu.SelectedIndex;
            if (index == -1) return;
            switch (index)
            {

                case 0:
                    mainFrame.Navigate(new CreateReservation(guest));
                    break;
                case 1:
                    mainFrame.Navigate(new MyReservations(guest));
                    break;
                case 2:
                    mainFrame.Navigate(new DisplayAllHotels());
                    break;
            }

            Menu.SelectedIndex = -1;
        }

        private void Logout_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();

        }
    }
}
