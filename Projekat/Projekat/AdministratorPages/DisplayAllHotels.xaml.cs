using Project.Model;
using Projekat.Controller;
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

namespace Projekat.AdministratorPages
{
    /// <summary>
    /// Interaction logic for DisplayAllHotels.xaml
    /// </summary>
    public partial class DisplayAllHotels : Page
    {
        private HotelController hotelController = new HotelController();
        private List<Hotel> allHotels = new List<Hotel>();

        public DisplayAllHotels()
        {
            InitializeComponent();
            allHotels = hotelController.GetHotels();
            dataGrid.ItemsSource = allHotels;
        }

        private void Sort_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (dataGrid.ItemsSource == null)
            {
                return;
            }

            ComboBoxItem sortSelectedItem = (ComboBoxItem)sortComboBox.SelectedItem;
            string sortOption = sortSelectedItem.Tag.ToString();

            IEnumerable<Hotel> sortedHotels = dataGrid.ItemsSource as IEnumerable<Hotel>;

            ApplySorting(sortedHotels, sortOption);
        }

        private void ApplySorting(IEnumerable<Hotel> hotels, string sortOption = null)
        {
            if (sortOption == null)
            {
                dataGrid.ItemsSource = hotels;
                return;
            }

            IEnumerable<Hotel> sortedHotels = null;

            if (sortOption == "Name Asc")
            {
                sortedHotels = hotels.OrderBy(h => h.HotelName);
            }
            else if (sortOption == "Name Desc")
            {
                sortedHotels = hotels.OrderByDescending(h => h.HotelName);
            }
            else if (sortOption == "Stars 1-5")
            {
                sortedHotels = hotels.OrderBy(h => h.NumberOfStars);
            }
            else if (sortOption == "Stars 5-1")
            {
                sortedHotels = hotels.OrderByDescending(h => h.NumberOfStars);
            }

            dataGrid.ItemsSource = sortedHotels;
        }

    }
}
