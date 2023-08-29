using Project.Model;
using Projekat.Controller;
using Projekat.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for DisplayAllHotels.xaml
    /// </summary>
    public partial class DisplayAllHotels : Page
    {
        private HotelController hotelController = new HotelController();
        private UserController userController = new UserController();
        private List<Hotel> allHotels = new List<Hotel>();
        private User host = new User();

        public DisplayAllHotels(User user)
        {
            InitializeComponent();
            allHotels = hotelController.GetAllByHostJmbg(user.JMBG);
            ShowHotels();
            host = user;
        }

        private void ShowHotels()
        {
            var hotels = allHotels.Where(h => h.HotelStatus == HotelStatus.Accepted || h.HotelStatus == HotelStatus.Pending);
            dataGrid.ItemsSource = hotels;
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

        private void ApproveButton_Click(object sender, RoutedEventArgs e)
        {
            Hotel selected = (sender as Button).DataContext as Hotel;

            if (selected != null)
            {
                hotelController.AcceptHotel(selected);
                MessageBox.Show("Hotel is successfully accepted.");
                ShowHotels();

            }

        }

        private void CancelButton_Click(object sender, RoutedEventArgs e)
        {
            Hotel selected = (sender as Button).DataContext as Hotel;

            if (selected != null)
            {
                hotelController.DeleteHotel(selected);
                allHotels.Remove(selected);
                MessageBox.Show("Hotel is successfully rejected.");
                ShowHotels();

            }

        }

        private void Search_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search.Text = string.Empty;
            ShowHotels();

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
            filteredHotels = filteredHotels.Where(h => (h.HotelStatus == HotelStatus.Accepted || h.HotelStatus == HotelStatus.Pending) && h.HostJmbg == host.JMBG);
            ApplySorting(filteredHotels);
        }

        private void Apartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            Search.Text = string.Empty;
            ShowHotels();
        }

        private void ViewReservations_Click(object sender, RoutedEventArgs e)
        {
            Hotel selectedHotel = (sender as Button)?.DataContext as Hotel;

            if (selectedHotel != null)
            {
                HotelReservationDisplay hrd = new HotelReservationDisplay(selectedHotel, host);
                hrd.ShowDialog();
            }
        }

    }
}
