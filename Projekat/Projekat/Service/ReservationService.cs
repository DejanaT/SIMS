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
    public class ReservationService
    {
        private ReservationRepository reservationRepository = new ReservationRepository();
        private ApartmentRepository apartmentRepository = new ApartmentRepository();    
        private ApartmentService apartmentService = new ApartmentService();

        public List<Reservation> GetReservations()
        {
            return reservationRepository.GetAll();
        }

        public Reservation GetById(string id)
        {
            return reservationRepository.GetById(id);
        }

        public void SaveChanges(List<Reservation> reservations)
        {
            reservationRepository.SaveChanges(reservations);
        }

        public void Create(Reservation reservation)
        {
            reservation.Id = Guid.NewGuid().ToString();
            bool isReservationExist = reservationRepository.GetAll().Any(r => r.ReservationDate == reservation.ReservationDate && r.ApartmentName == reservation.ApartmentName);

            if (isReservationExist)
            {
                MessageBox.Show("Reservation already exists!");
            }
            else
            {
                reservationRepository.AddNewReservation(reservation);
                MessageBox.Show("Reservation created successfully!");
            }
        }

        public bool IsReservationExist(Reservation r)
        {
            List<Reservation> reservations = GetReservations();
            return reservations.Any(res => res.ReservationDate == r.ReservationDate && res.ApartmentName == r.ApartmentName);
        }

        public List<Reservation> GetAllByGuestJmbg(string guestJmbg)
        {
            List<Reservation> reservations = reservationRepository.GetAllByGuestJmbg(guestJmbg);
            List<Reservation> notCanceledReservations = new List<Reservation>();

            foreach (Reservation reservation in reservations)
            {
                if (!reservation.Deleted)
                {
                    notCanceledReservations.Add(reservation);
                }
            }
            return notCanceledReservations;
        }

        public List<Reservation> GetByStatus(string status)
        {
            return reservationRepository.GetByStatus(status);
        }

        public void Update(Reservation r)
        {
            reservationRepository.Update(r);
        }

        public void UpdateApartmentReservationCancel(Reservation reservation)
        {
            Apartment apartment = apartmentRepository.FindByName(reservation.ApartmentName);

            if (apartment != null)
            {
                var reservationInApartment = apartment.Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInApartment != null)
                {
                    apartment.Reservations.Remove(reservationInApartment);
                    apartmentService.Update(apartment);
                }
            }
        }


    }
}
