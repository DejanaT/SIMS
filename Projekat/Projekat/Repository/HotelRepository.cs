using Project.Model;
using Projekat.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Repository
{
    public class HotelRepository
    {
        private List<Hotel> hotels;
        private JsonSerializer<Hotel> hotelSerializer = new JsonSerializer<Hotel>();
        private string path = @"C:\Users\dejana\Desktop\SIMS\Projekat\Projekat\Data\hotels.json";


        public List<Hotel> GetAll()
        {
            hotels = hotelSerializer.LoadDataFromJson(path) as List<Hotel>;
            return hotels;
        }

        public Hotel FindByCode(string code)
        {
            hotels = GetAll();
            return hotels.FirstOrDefault(h => h.HotelCode == code && !h.Deleted);
        }


        public Hotel GetById(string id)
        {
            hotels = GetAll();

            foreach (Hotel h in hotels)
            {
                if (h.Id.Equals(id))
                {
                    return h;
                }
            }
            return null;
        }

        public void SaveChanges(List<Hotel> hotels)
        {
            hotelSerializer.SaveDataToJson(hotels, path);
        }

        public void AddNewHotel(Hotel hotel)
        {
            hotels = GetAll();
            hotels.Add(hotel);
            SaveChanges(hotels);
        }

        public void DeleteHotel(Hotel hotel)
        {
            hotels = GetAll();
            Hotel cancelHotel = hotels.FirstOrDefault(h => h.Id == hotel.Id);

            if (cancelHotel != null)
            {
                SaveChanges(hotels);
            }

        }

        public void UpdateHotel(Hotel hotel)
        {
            hotels = GetAll();

            Hotel existingHotel = hotels.FirstOrDefault(h => h.HotelCode.Equals(hotel.HotelCode));
            if (existingHotel != null)
            {
                int index = hotels.IndexOf(existingHotel);
                hotels[index] = hotel;
                SaveChanges(hotels);
            }
        }

        public List<Hotel> GetAllByHostJmbg(string hostJmbg)
        {
            hotels = GetAll(); 
            return hotels.Where(hotel => hotel.HostJmbg == hostJmbg).ToList();
        }

    }
}
