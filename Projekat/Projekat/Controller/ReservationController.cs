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
        private ApartmentService apartmentService = new ApartmentService();
        private HotelService hotelService = new HotelService();

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

        /* public void UpdateApartmentReservationCancel(Reservation reservation)
         {
             reservationService.UpdateApartmentReservationCancel(reservation);
         }

         public void UpdateHotelReservationCancel(Reservation reservation)
         {
             reservationService.UpdateHotelReservationCancel(reservation);
         }*/

        public void CancelReservation(Reservation reservation)
        {
            Apartment apartment = apartmentService.FindApartmentByReservation(reservation);
            Hotel hotel = hotelService.FindHotelByApartment(apartment);

            if (apartment != null)
            {
                reservation.Status = ReservationStatus.Canceled;
                reservation.Deleted = true;
                reservationService.Update(reservation);
                reservationService.UpdateApartmentReservationCancel(reservation, apartment);

                if (hotel != null)
                {
                    reservationService.UpdateHotelReservationCancel(reservation, hotel);
                }
            }
        }

        public void RejectReservation(Reservation reservation)
        {
            Apartment apartment = apartmentService.FindApartmentByReservation(reservation);
            Hotel hotel = hotelService.FindHotelByApartment(apartment);

            if (apartment != null)
            {
                reservation.Status = ReservationStatus.Rejected;
                reservationService.Update(reservation);
                reservationService.UpdateApartmentReservationCancel(reservation, apartment);

                if (hotel != null)
                {
                    reservationService.UpdateHotelReservationCancel(reservation, hotel);
                }
            }
        }

        public void ApproveReservation(Reservation reservation)
        {
            Apartment apartment = apartmentService.FindApartmentByReservation(reservation);
            Hotel hotel = hotelService.FindHotelByApartment(apartment);

            if (apartment != null)
            {
                reservation.Status = ReservationStatus.Accepted;
                reservationService.Update(reservation);
                reservationService.UpdateApartmentReservationApprove(reservation, apartment);

                if (hotel != null)
                {
                    reservationService.UpdateHotelReservationApprove(reservation, hotel);
                }
            }
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
