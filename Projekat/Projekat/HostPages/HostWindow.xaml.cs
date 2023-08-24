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

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for HostWindow.xaml
    /// </summary>
    public partial class HostWindow : Window
    {
        private User host = new User();

        public HostWindow(User user)
        {
            InitializeComponent();
            Name.Text = user.Name;
            Surname.Text = user.Surname;
            host = user;
        }

        private void Menu_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            int index = Menu.SelectedIndex;
            if (index == -1) return;
            switch (index)
            {

                case 0:
                    //dodati
                    break;
                case 1:
                    mainFrame.Navigate(new DisplayAllHotels(host));
                    break;
                case 2:
                    mainFrame.Navigate(new HotelsApartments(host));
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
