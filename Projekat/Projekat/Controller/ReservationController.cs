using Project.Model;
using Projekat.Model;
using Projekat.Model.Enums;
using Projekat.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Controller
{
    public class ReservationController
    {
        private ReservationService reservationService = new ReservationService();

        public List<Reservation> GetReservations()
        {
            return reservationService.GetReservations();
        }
        public Reservation GetById(string id)
        {
            return reservationService.GetById(id);
        }

        public void Create(Reservation r)
        {
            reservationService.Create(r);
        }

        public bool IsReservationExist(Reservation r)
        {
            return reservationService.IsReservationExist(r);
        }

        public List<Reservation> GetAllByGuestJmbg(string guestJmbg)
        {
            return reservationService.GetAllByGuestJmbg(guestJmbg);
        }

        public List<Reservation> GetByStatus(string status)
        {
            return reservationService.GetByStatus(status);
        }

        public void Update(Reservation selectedReservation)
        {
            reservationService.Update(selectedReservation);
        }

        public void CancelReservation(Reservation reservation)
        {
            reservationService.CancelReservation(reservation);
        }

        public void RejectReservation(Reservation reservation)
        {
            reservationService.RejectReservation(reservation);
        }

        public void ApproveReservation(Reservation reservation)
        {
            reservationService.ApproveReservation(reservation);
        }

        public List<Reservation> GetReservationsByHostJmbg(string hostJmbg, List<Hotel> hotels)
         {
             return reservationService.GetReservationsByHostJmbg(hostJmbg, hotels);
         }

        public List<Reservation> GetAllByHostJmbg(string hostJmbg)
        {
            return reservationService.GetAllByHostJmbg(hostJmbg);
        }
    }
}
