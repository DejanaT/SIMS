using Project.Model;
using Projekat.Controller;
using Projekat.Model.Enums;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text.RegularExpressions;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.AdministratorPages
{
    /// <summary>
    /// Interaction logic for CreateHotel.xaml
    /// </summary>
    public partial class CreateHotel : Page
    {
        private ObservableCollection<Apartment> hotelApartments = new ObservableCollection<Apartment>();
        private HotelController hotelController = new HotelController();
        private UserController userController = new UserController();
        private List<string> hostJmbgs = new List<string>();

        public CreateHotel()
        {
            InitializeComponent();
            hostJmbgs = userController.GetHostsJmbgs();
            HostJmbgDropdown.ItemsSource = hostJmbgs;
           
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {

            if (AreFieldsValid())
            {
                Hotel newHotel = new Hotel
                {
                    HotelCode = HotelCode.Text,
                    HotelName = HotelName.Text,
                    YearOfConstruction = int.Parse(YearOfConstruction.Text),
                    Apartments = new Dictionary<string, Apartment>(),
                    NumberOfStars = int.Parse(NumberOfStars.Text),
                    HostJmbg = HostJmbgDropdown.Text
                };
                //prozor za dodavanje apartmana
                AddApartments addAp = new AddApartments(hotelApartments.ToList(), newHotel);
                if (addAp.ShowDialog() == true)
                {
                    foreach (Apartment apartment in hotelApartments)
                    {
                        newHotel.Apartments.Add(apartment.Name, apartment);
                    }
                    newHotel.HotelStatus = HotelStatus.Pending;
                    hotelController.Create(newHotel);
                }
            }


        }
        private bool AreFieldsValid()
        {
            if (String.IsNullOrEmpty(HotelCode.Text) || String.IsNullOrEmpty(HotelName.Text) ||
                String.IsNullOrEmpty(YearOfConstruction.Text) || String.IsNullOrEmpty(NumberOfStars.Text) ||
                String.IsNullOrEmpty(HostJmbgDropdown.Text))
            {
                MessageBox.Show("Error: you must fill in all fields");
                return false;
            }

            if (hotelController.FindByCode(HotelCode.Text) != null)
            {
                string errorMessage = "Error: Hotel with the same HotelCode already exists!";
                MessageBox.Show(errorMessage);
                return false;
            }

            if (!int.TryParse(NumberOfStars.Text, out int stars) || stars < 1 || stars > 5)
            {
                MessageBox.Show("Error: Number of stars must be between 1 and 5!");
                return false;
            }

            if (!int.TryParse(YearOfConstruction.Text, out int year) || year <= 0 || !Regex.IsMatch(YearOfConstruction.Text, @"^\d{4}$"))
            {
                MessageBox.Show("Error: Year of construction must be a positive four-digit integer!");
                return false;
            }

            return true;
        }

        private void Host_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (HostJmbgDropdown.SelectedItem != null)
            {
                string selectedJmbg = HostJmbgDropdown.SelectedItem.ToString();
                User selectedUser = userController.FindByJmbg(selectedJmbg);

                if (selectedUser != null)
                {
                    NameSurname.Text = $"{selectedUser.Name} {selectedUser.Surname}";
                }
            }
        }

    }
}
