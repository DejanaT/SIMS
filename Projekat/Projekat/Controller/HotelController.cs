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
    }
}
