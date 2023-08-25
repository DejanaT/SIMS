using Project.Model;
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
            bool isHotelExist = hotelRepository.GetAll().Any(h => h.Id == hotel.Id || h.HotelCode == hotel.HotelCode);

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
            hotelRepository.DeleteHotel(hotel);
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
                hotelRepository.AddApartmentsToHotel(code, apartments);
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
            return hotelRepository.GetHotelsByCode(query);
        }

        public IEnumerable<Hotel> GetHotelsByName(string query)
        {
            return hotelRepository.GetHotelsByName(query);
        }

        public IEnumerable<Hotel> GetHotelsByStars(string query)
        {
            return hotelRepository.GetHotelsByStars(query);
        }

        public IEnumerable<Hotel> GetHotelsByNumberOfRooms(IEnumerable<Hotel> hotels, int numberOfRooms)
        {
            return hotelRepository.GetHotelsByNumberOfRooms(hotels, numberOfRooms);
        }

        public IEnumerable<Hotel> GetHotelsByNumberOfGuests(IEnumerable<Hotel> hotels, int numberOfGuests)
        {
            return hotelRepository.GetHotelsByNumberOfGuests(hotels, numberOfGuests);
        }

        public IEnumerable<Hotel> RoomsAndGuestsAnd(IEnumerable<Hotel> hotels, int numberOfRooms, int numberOfGuests)
        {
            return hotelRepository.RoomsAndGuestsAnd(hotels, numberOfRooms, numberOfGuests);
        }

        public IEnumerable<Hotel> RoomsAndGuestsOr(IEnumerable<Hotel> hotels, int numberOfRooms, int numberOfGuests)
        {
            return hotelRepository.RoomsAndGuestsOr(hotels, numberOfRooms, numberOfGuests);
        }

        public void UpdateApartments(Hotel hotel)
        {
            hotelRepository.UpdateApartments(hotel);
        }
    }
}
