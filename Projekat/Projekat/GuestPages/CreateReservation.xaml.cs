using Project.Model;
using Projekat.Controller;
using Projekat.Model;
using Projekat.Model.Enums;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;

namespace Projekat.GuestPages
{
    /// <summary>
    /// Interaction logic for Reservation.xaml
    /// </summary>
    public partial class CreateReservation : Page
    {
        private HotelController hotelController = new HotelController();
        private ApartmentController apartmentController = new ApartmentController();
        private ReservationController reservationController = new ReservationController();
        private UserController userController = new UserController();
        private string jmbg;

        public CreateReservation(User user)
        {
            InitializeComponent();
            LoadHotelsName();
            jmbg = user.JMBG;

        }

        private void LoadHotelsName()
        {
            List<Hotel> hotels = hotelController.GetApprovedHotels();
            foreach (var hotel in hotels)
            {
                if (hotel.Apartments.Any())
                {
                    HotelComboBox.Items.Add($"{hotel.HotelCode} - {hotel.HotelName}");
                }
            }
        }

        private void Hotel_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            ApartmentComboBox.IsEnabled = false;
            ApartmentComboBox.Items.Clear();

            string selectedHotelInfo = HotelComboBox.SelectedItem as string;
            if (!string.IsNullOrEmpty(selectedHotelInfo))
            {
                string selectedHotelCode = selectedHotelInfo.Split('-')[0].Trim();
                Hotel selectedHotel = hotelController.FindByCode(selectedHotelCode);
                if (selectedHotel != null)
                {
                    ApartmentComboBox.IsEnabled = true;

                    foreach (var apartmentName in selectedHotel.Apartments.Values.Select(apartment => apartment.Name))
                    {
                        ApartmentComboBox.Items.Add(apartmentName);
                    }

                }
            }
        }
        private void Apartment_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            string apartmentName = ApartmentComboBox.SelectedItem as string;
            LoadApartmentDetails(apartmentName);
        }

        private void LoadApartmentDetails(string apartmentName)
        {
            if (!string.IsNullOrEmpty(apartmentName))
            {
                Apartment selectedApartment = apartmentController.FindByName(apartmentName);

                if (selectedApartment != null)
                {
                    Rooms.Text = selectedApartment.RoomsQuantity.ToString();
                    Guests.Text = selectedApartment.MaxNumberOfGuests.ToString();
                }
            }
            else
            {
                Rooms.Text = "";
                Guests.Text = "";
            }
        }

        private void Create_Click(object sender, RoutedEventArgs e)
        {
            string hotel = HotelComboBox.SelectedItem as string;
            string apartment = ApartmentComboBox.SelectedItem as string;
            string date = Date.SelectedDate?.ToString("yyyy-MM-dd");

            string selectedCode = hotel.Split('-')[0].Trim();


            Apartment selectedApartment = apartmentController.FindByName(apartment);
            Hotel selectedHotel = hotelController.FindByCode(selectedCode);


            if (string.IsNullOrEmpty(hotel) || string.IsNullOrEmpty(apartment) || string.IsNullOrEmpty(date))
            {
                MessageBox.Show("You must fill all fields!");
                return;
            }

            Reservation newReservation = new Reservation
            {
                GuestJmbg = jmbg,
                HostJmbg = userController.GetHostJmbgByApartmentAndHotel(selectedHotel.HotelCode, selectedApartment.Name),
                ApartmentName = selectedApartment.Name,
                ReservationDate = date,
                Status = ReservationStatus.Pending,
                ReasonReject = ""
            };

            if (selectedApartment.Reservations == null)
            {
                selectedApartment.Reservations = new List<Reservation>();
            }
            else
            {

                if (selectedApartment.Reservations.Any(r => r.ReservationDate == newReservation.ReservationDate))
                {
                    string error = "Reservation already exists for:" + newReservation.ReservationDate;
                    MessageBox.Show(error);
                    return;
                }
            }

            reservationController.Create(newReservation);
            if (!reservationController.IsReservationExist(newReservation))
            {
                return;
            }

            selectedApartment.Reservations.Add(newReservation);
            apartmentController.Update(selectedApartment);

            if (selectedHotel.Apartments.ContainsKey(selectedApartment.Name))
            {
                selectedHotel.Apartments[selectedApartment.Name] = selectedApartment;
                hotelController.UpdateApartments(selectedHotel);
            }

        }
    }
}
