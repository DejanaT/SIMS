using Project.Model;
using Projekat.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Projekat.Service
{
    public class ApartmentService
    {
        private ApartmentRepository apartmentRepository = new ApartmentRepository();

        public List<Apartment> GetApartments()
        {
            return apartmentRepository.GetAll();
        }

        public Apartment FindByName(string name)
        {
            return apartmentRepository.FindByName(name);
        }

        public void SaveChanges(List<Apartment> apartments)
        {
            apartmentRepository.SaveChanges(apartments);
        }

        public void Create(Apartment apartment)
        {
            apartment.Id = Guid.NewGuid().ToString();
            bool isApartmentExist = apartmentRepository.GetAll().Any(a => a.Id == apartment.Id || a.Name == apartment.Name);

            if (!isApartmentExist)
            {
                apartmentRepository.AddNewApartment(apartment);
                MessageBox.Show("Apartment created successful!");
            }
            else
            {
                MessageBox.Show("Apartment with the same Name already exists!");
            }
        }

        public void DeleteApartment(Apartment apartment)
        {
            apartmentRepository.DeleteApartment(apartment);
        }

        public bool IsApartmentExist(Apartment a)
        {
            List<Apartment> apartments = GetApartments();
            return apartments.Any(app => a.Name.Equals(app.Name));
        }

        public void Update(Apartment a)
        {
            apartmentRepository.UpdateApartment(a);
        }
    }
}
