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
    }
}
