using Project.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Repository
{
    public class ApartmentRepository
    {
        private List<Apartment> apartments;
        private JsonSerializer<Apartment> apartmentSerializer = new JsonSerializer<Apartment>();
        private string path = @"C:\Users\dejana\Desktop\SIMS\Projekat\Projekat\Data\apartments.json";

        public List<Apartment> GetAll()
        {
            apartments = apartmentSerializer.LoadDataFromJson(path) as List<Apartment>;
            return apartments;
        }

        public Apartment FindByName(string name)
        {
            apartments = GetAll();
            foreach (Apartment ap in apartments)
            {
                if (ap.Name.Equals(name))
                {
                    return ap;
                }
            }
            return null;
        }

        public Apartment GetById(string id)
        {
            apartments = GetAll();

            foreach (Apartment ap in apartments)
            {
                if (ap.Id.Equals(id))
                {
                    return ap;
                }
            }
            return null;
        }

        public void SaveChanges(List<Apartment> apartments)
        {
            apartmentSerializer.SaveDataToJson(apartments, path);
        }

        public void AddNewApartment(Apartment apartment)
        {
            apartments = GetAll();
            apartments.Add(apartment);
            SaveChanges(apartments);
        }

        public void DeleteHotel(Apartment apartment)
        {
            apartments = GetAll();
            apartments.Remove(apartment);
            SaveChanges(apartments);

        }

    }
}
