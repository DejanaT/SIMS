using Projekat.Model;
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


    }
}
