using Project.Model;
using Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Controller
{
    public class ApartmentController
    {
        private ApartmentService apartmentService = new ApartmentService();

        public List<Apartment> GetApartments()
        {
            return apartmentService.GetApartments();
        }
        public Apartment FindByName(string name)
        {
            return apartmentService.FindByName(name);
        }

        public void Create(Apartment a)
        {
            apartmentService.Create(a);
        }

        public void DeleteUser(Apartment a)
        {
            apartmentService.DeleteApartment(a);
        }

        public bool IsApartmentExist(Apartment a)
        {
            return apartmentService.IsApartmentExist(a);
        }
    }
}
