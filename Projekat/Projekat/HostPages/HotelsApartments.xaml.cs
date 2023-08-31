using Project.Model;
using Projekat.Controller;
using Projekat.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

namespace Projekat.HostPages
{
    /// <summary>
    /// Interaction logic for HotelsApartments.xaml
    /// </summary>
    public partial class HotelsApartments : Page
    {

        private HotelController hotelController = new HotelController();
        private UserController userController = new UserController();
        private List<Hotel> allHotels = new List<Hotel>();
        private ObservableCollection<Apartment> hotelApartments = new ObservableCollection<Apartment>();

        public HotelsApartments(User user)
        {
            InitializeComponent();
            allHotels = hotelController.GetAllByHostJmbg(user.JMBG);
            ShowHotels();
            DataContext = this;
        }

        private void ShowHotels()
        {
            var hotels = allHotels.Where(h => h.HotelStatus == HotelStatus.Accepted);
            dataGrid.ItemsSource = hotels;
        }

        private void AddButton_Click(object sender, RoutedEventArgs e)
        {

            Hotel selectedHotel = dataGrid.SelectedItem as Hotel;

            AddApartments addAp = new AddApartments(hotelApartments.ToList(), selectedHotel);
            if (addAp.ShowDialog() == true)
            {
                foreach (Apartment apartment in hotelApartments)
                {
                    selectedHotel.Apartments.Add(apartment.Name, apartment);
                }
                hotelController.Update(selectedHotel);
                MessageBox.Show("Apartment(s) added successfully!");
                dataGrid.Items.Refresh();
            }
        }

    }
}
