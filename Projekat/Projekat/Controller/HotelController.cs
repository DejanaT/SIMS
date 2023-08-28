using Project.Model;
using Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Controller
{
    public class HotelController
    {
        private HotelService hotelService = new HotelService();

        public List<Hotel> GetHotels()
        {
            return hotelService.GetHotels();
        }
        public Hotel FindByCode(string code)
        {
            return hotelService.FindByCode(code);
        }

        public void Create(Hotel h)
        {
            hotelService.Create(h);
        }

        public void DeleteHotel(Hotel h)
        {
            hotelService.DeleteHotel(h);
        }

        public bool IsApartmentExist(Hotel h)
        {
            return hotelService.IsHotelExist(h);
        }

        public void AddApartmentsToHotel(string code, List<Apartment> apartments)
        {
            hotelService.AddApartmentsToHotel(code, apartments);
        }

        public List<Hotel> GetAllByHostJmbg(string hostJmbg)
        {
            return hotelService.GetAllByHostJmbg(hostJmbg);
        }

        public void AcceptHotel(Hotel hotel)
        {
            hotelService.AcceptHotel(hotel);
        }

        public void Update(Hotel selectedHotel)
        {
            hotelService.Update(selectedHotel);
        }

        public IEnumerable<Hotel> GetHotelsByCode(string query)
        {
            return hotelService.GetHotelsByCode(query);
        }

        public IEnumerable<Hotel> GetHotelsByName(string query)
        {
            return hotelService.GetHotelsByName(query);
        }

        public IEnumerable<Hotel> GetHotelsByStars(string query)
        {
            return hotelService.GetHotelsByStars(query);
        }

        public IEnumerable<Hotel> GetHotelsByNumberOfRooms(IEnumerable<Hotel> hotels, int numberOfRooms)
        {
            return hotelService.GetHotelsByNumberOfRooms(hotels, numberOfRooms);
        }

        public IEnumerable<Hotel> GetHotelsByNumberOfGuests(IEnumerable<Hotel> hotels, int numberOfGuests)
        {
            return hotelService.GetHotelsByNumberOfGuests(hotels, numberOfGuests);
        }

        public IEnumerable<Hotel> RoomsAndGuestsAnd(IEnumerable<Hotel> hotels, int numberOfRooms, int numberOfGuests)
        {
            return hotelService.RoomsAndGuestsAnd(hotels, numberOfRooms, numberOfGuests);
        }

        public IEnumerable<Hotel> RoomsAndGuestsOr(IEnumerable<Hotel> hotels, int numberOfRooms, int numberOfGuests)
        {
            return hotelService.RoomsAndGuestsOr(hotels, numberOfRooms, numberOfGuests);
        }

        public void UpdateApartments(Hotel hotel)
        {
            hotelService.UpdateApartments(hotel);
        }

        public List<Hotel> GetApprovedHotels()
        {
            return hotelService.GetApprovedHotels();
        }

        public Hotel FindHotelByApartment(Apartment apartment)
        {
            return hotelService.FindHotelByApartment(apartment);
        }
    }
}
