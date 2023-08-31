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
            bool isApartmentExist = apartmentRepository.GetAll().Any(a => a.Name == apartment.Name && !a.Deleted);

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

        public Apartment FindApartmentByReservation(Reservation reservation)
        {
            List<Apartment> apartments = apartmentRepository.GetAll();
            foreach (Apartment apartment in apartments)
            {
                if (apartment.Reservations != null && apartment.Reservations.Any(r => r.Id == reservation.Id))
                {
                    return apartment;
                }
            }
            return null;
        }

        public void UpdateApartmentReservationCancel(Reservation reservation, Apartment apartment)
        {
            if (apartment != null)
            {
                var reservationInApartment = apartment.Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInApartment != null)
                {
                    apartment.Reservations.Remove(reservationInApartment);
                    apartmentRepository.UpdateApartment(apartment);
                }
            }
        }

        public void UpdateApartmentReservationApprove(Reservation reservation, Apartment apartment)
        {
            if (apartment != null)
            {
                var reservationInApartment = apartment.Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInApartment != null)
                {
                    reservationInApartment.Status = ReservationStatus.Accepted;
                    apartmentRepository.UpdateApartment(apartment);
                }
            }
        }
    }
}
