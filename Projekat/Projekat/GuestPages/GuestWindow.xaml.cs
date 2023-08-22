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
        public GuestWindow(User user)
        {
            InitializeComponent();
            Name.Text = user.Name;
            Surname.Text = user.Surname;
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
                   //dodati
                    break;
                case 2:
                    mainFrame.Navigate(new DisplayAllHotels());
                    break;
                case 3:
                    //dodati
                    break;
            }

            Menu.SelectedIndex = -1;
        }
    }
}
