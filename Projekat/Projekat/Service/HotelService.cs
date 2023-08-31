using Project.Model;
using Projekat.Model;
using Projekat.Model.Enums;
using Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat.Service
{
    public class HotelService
    {
        private HotelRepository hotelRepository = new HotelRepository();
        private ApartmentRepository apartmentRepository = new ApartmentRepository();

        public List<Hotel> GetHotels()
        {
            return hotelRepository.GetAll();
        }

        public Hotel FindByCode(string code)
        {
            return hotelRepository.FindByCode(code);
        }

        public void SaveChanges(List<Hotel> hotels)
        {
            hotelRepository.SaveChanges(hotels);
        }

        public void Create(Hotel hotel)
        {
            hotel.Id = Guid.NewGuid().ToString();
            bool isHotelExist = hotelRepository.GetAll().Any(h => (h.Id == hotel.Id || h.HotelCode == hotel.HotelCode) && !h.Deleted);

            if (!isHotelExist)
            {
                hotelRepository.AddNewHotel(hotel);
                MessageBox.Show("Hotel created successful!");
            }
            else
            {
                MessageBox.Show("Hotel with the same HotelCode already exists!");
            }
        }

        public void DeleteHotel(Hotel hotel)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            Hotel cancelHotel = hotels.FirstOrDefault(h => h.Id == hotel.Id);

            if (cancelHotel != null)
            {
                cancelHotel.HotelStatus = HotelStatus.Rejected;
                cancelHotel.Deleted = true;

                foreach (var apartment in cancelHotel.Apartments.Values)
                {
                    apartment.Reservations = new List<Reservation>();
                    apartment.Deleted = true;
                    apartmentRepository.UpdateApartment(apartment);

                }

                cancelHotel.Apartments = new Dictionary<string, Apartment>();
                hotelRepository.SaveChanges(hotels);
            }
        }

        internal void Update(Hotel selectedHotel)
        {
            hotelRepository.UpdateHotel(selectedHotel);
        }

        public bool IsHotelExist(Hotel hotel)
        {
            List<Hotel> hotels = GetHotels();
            return hotels.Any(h => h.HotelCode.Equals(hotel.HotelCode));
        }

        public void AddApartmentsToHotel(string code, List<Apartment> apartments)
        {
            Hotel hotel = FindByCode(code);
            if (hotel != null)
            {
                foreach (var apartment in apartments)
                {
                    hotel.Apartments.Add(apartment.Name, apartment);
                }
                hotelRepository.UpdateHotel(hotel);
            }
            else
            {
                MessageBox.Show("Hotel not found!");
            }
        }

        public List<Hotel> GetAllByHostJmbg(string hostJmbg)
        {
            return hotelRepository.GetAllByHostJmbg(hostJmbg);
        }

        public void AcceptHotel(Hotel hotel)
        {
            hotel.HotelStatus = HotelStatus.Accepted;
            hotelRepository.UpdateHotel(hotel);
        }
        public IEnumerable<Hotel> GetHotelsByCode(string query)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            return hotels.Where(h => h.HotelCode.ToLower().Contains(query));
        }

        public IEnumerable<Hotel> GetHotelsByName(string query)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            return hotels.Where(h => h.HotelName.ToLower().Contains(query));
        }

        public IEnumerable<Hotel> GetHotelsByStars(string query)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            return hotels.Where(h => h.NumberOfStars.ToString().Contains(query));
        }

        public IEnumerable<Hotel> GetHotelsByYear(string query)
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            return hotels.Where(h => h.YearOfConstruction.ToString().Contains(query));
        }

        public IEnumerable<Hotel> GetHotelsByNumberOfRooms(IEnumerable<Hotel> hotels, int numberOfRooms)
        {
            return hotels.Where(hotel => hotel.Apartments.Values.Any(apartment => apartment.RoomsQuantity == numberOfRooms));
        }

        public IEnumerable<Hotel> GetHotelsByNumberOfGuests(IEnumerable<Hotel> hotels, int numberOfGuests)
        {
            return hotels.Where(hotel => hotel.Apartments.Values.Any(apartment => apartment.MaxNumberOfGuests == numberOfGuests));
        }

        public IEnumerable<Hotel> RoomsAndGuestsAnd(IEnumerable<Hotel> hotels, int numberOfRooms, int numberOfGuests)
        {
            return hotels.Where(hotel => hotel.Apartments.Values.Any(apartment => apartment.RoomsQuantity == numberOfRooms && apartment.MaxNumberOfGuests == numberOfGuests));
        }

        public IEnumerable<Hotel> RoomsAndGuestsOr(IEnumerable<Hotel> hotels, int numberOfRooms, int numberOfGuests)
        {
            return hotels.Where(hotel => hotel.Apartments.Values.Any(apartment => apartment.RoomsQuantity == numberOfRooms || apartment.MaxNumberOfGuests == numberOfGuests));
        }

        public void UpdateApartments(Hotel hotel)
        {
            List<Hotel> hotels = hotelRepository.GetAll();

            var hotel1 = hotels.FirstOrDefault(h => h.Id == hotel.Id);

            if (hotel1 != null)
            {
                foreach (var apartment in hotel.Apartments.Values)
                {
                    if (hotel1.Apartments.ContainsKey(apartment.Name))
                    {
                        var existingApartment = hotel1.Apartments[apartment.Name];
                        existingApartment.Reservations = apartment.Reservations;
                    }
                }

                SaveChanges(hotels);
            }
        }


        public List<Hotel> GetApprovedHotels()
        {
            List<Hotel> hotels = hotelRepository.GetAll();
            List<Hotel> approvedHotels = hotels.Where(h => h.HotelStatus == HotelStatus.Accepted).ToList();
            return approvedHotels;
        }

        public Hotel FindHotelByApartment(Apartment apartment)
        {
            List<Hotel>hotels = hotelRepository.GetAll();
            foreach (Hotel hotel in hotels)
            {
                if (hotel.Apartments != null && hotel.Apartments.Values.Any(a => a.Id == apartment.Id))
                {
                    return hotel;
                }
            }
            return null;
        }

        public void UpdateHotelReservationCancel(Reservation reservation, Hotel hotel)
        {
            Apartment apartment = apartmentRepository.FindByName(reservation.ApartmentName);
            if (hotel != null)
            {
                var reservationInHotel = hotel.Apartments[apartment.Name].Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInHotel != null)
                {
                    hotel.Apartments[apartment.Name].Reservations.Remove(reservationInHotel);
                    hotelRepository.UpdateHotel(hotel);
                }
            }
        }

        public void UpdateHotelReservationApprove(Reservation reservation, Hotel hotel)
        {
            Apartment apartment = apartmentRepository.FindByName(reservation.ApartmentName);
            if (hotel != null && apartment != null)
            {
                var reservationInHotel = hotel.Apartments[apartment.Name].Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInHotel != null)
                {
                    reservationInHotel.Status = ReservationStatus.Accepted;
                    hotelRepository.UpdateHotel(hotel);
                }
            }
        }
    }
}
