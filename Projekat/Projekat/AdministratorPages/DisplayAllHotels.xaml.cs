using Project.Model;
using Projekat.Controller;
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
            dataGrid.Items.Clear();
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

            Sort(sortedHotels, sortOption);
        }

        private void Sort(IEnumerable<Hotel> hotels, string sortOption = null)
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

        private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search.Text = string.Empty;
            hotelController.GetHotels();

            if (searchComboBox.SelectedItem != null)
            {
                ComboBoxItem selectedSearchItem = (ComboBoxItem)searchComboBox.SelectedItem;
                string parameter = selectedSearchItem.Tag.ToString();

                if (parameter == "Apartments")
                {
                    apartmentSearchComboBox.Visibility = Visibility.Visible;
                    apartmentSearchComboBox.IsEnabled = true;
                }
                else
                {
                    apartmentSearchComboBox.Visibility = Visibility.Collapsed;
                    apartmentSearchComboBox.IsEnabled = false;
                }

                Search.TextChanged -= Search_TextChanged;
                Search.TextChanged += Search_TextChanged;
            }
        }

        private void Search_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (dataGrid.ItemsSource == null || searchComboBox.SelectedItem == null)
            {
                return;
            }

            ComboBoxItem searchSelectedItem = (ComboBoxItem)searchComboBox.SelectedItem;
            string parameter = searchSelectedItem.Tag.ToString();
            string query = Search.Text.ToLower();

            IEnumerable<Hotel> filteredHotels = allHotels;


            if (parameter == "HotelCode")
            {
                filteredHotels = hotelController.GetHotelsByCode(query);
            }
            else if (parameter == "HotelName")
            {
                filteredHotels = hotelController.GetHotelsByName(query);
            }
            else if (parameter == "Stars")
            {
                filteredHotels = hotelController.GetHotelsByStars(query);
            }
            else if (parameter == "Year of constr.")
            {
                filteredHotels = hotelController.GetHotelsByYear(query);
            }
            else if (parameter == "Apartments")
            {

                ComboBoxItem apartmentSelectedItem = (ComboBoxItem)apartmentSearchComboBox.SelectedItem;

                if (apartmentSelectedItem == null)
                {
                    MessageBox.Show("Please select a search criteria.");
                    return;
                }

                string apartmentParameter = apartmentSelectedItem.Tag.ToString();

                if (apartmentParameter == "NumberOfRooms")
                {
                    int.TryParse(query, out int numberOfRooms);
                    filteredHotels = hotelController.GetHotelsByNumberOfRooms(filteredHotels, numberOfRooms);
                }
                else if (apartmentParameter == "NumberOfGuests")
                {
                    int.TryParse(query, out int numberOfGuests);
                    filteredHotels = hotelController.GetHotelsByNumberOfGuests(filteredHotels, numberOfGuests);
                }
                else if (apartmentParameter == "NumberOfRoomsAndGuests")
                {
                    string[] parts = query.Split(new char[] { '&', '|' });
                    parts = parts.Where(part => !string.IsNullOrWhiteSpace(part)).ToArray();

                    if (parts.Length == 2 && int.TryParse(parts[0], out int rooms) && int.TryParse(parts[1], out int guests))
                    {
                        if (query.Contains("&"))
                        {
                            filteredHotels = hotelController.RoomsAndGuestsAnd(filteredHotels, rooms, guests);
                        }
                        else if (query.Contains("|"))
                        {
                            filteredHotels = hotelController.RoomsAndGuestsOr(filteredHotels, rooms, guests);
                        }
                    }
                    else
                    {
                        filteredHotels = new List<Hotel>();
                    }
                }

            }
            Sort(filteredHotels);
        }

        private void Apartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search.Text = string.Empty;
            hotelController.GetHotels();
        }


    }
}
