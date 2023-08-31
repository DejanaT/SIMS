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
        private ApartmentService apartmentService = new ApartmentService();
        private HotelService hotelService = new HotelService();

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
            reservation.Status = ReservationStatus.Pending;

            bool isExists = reservationRepository.GetAll().Any(r => r.ReservationDate == reservation.ReservationDate &&
                                                                    r.ApartmentName == reservation.ApartmentName &&
                                                                    r.Status != ReservationStatus.Canceled);

            if (isExists)
            {
                MessageBox.Show("A reservation already exists for the selected date!");
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
            return reservations.Any(res => res.ReservationDate == r.ReservationDate &&
                                           res.ApartmentName == r.ApartmentName &&
                                           res.Id == r.Id);
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


        public List<Reservation> GetReservationsByHostJmbg(string hostJmbg, List<Hotel> hotels)
        {
            return hotels.SelectMany(hotel => hotel.Apartments.Values).Where(apartment => apartment.Reservations != null)
                         .SelectMany(apartment => apartment.Reservations).Where(reservation => reservation.Status == ReservationStatus.Pending
                                    || reservation.Status == ReservationStatus.Accepted).ToList();
        }

        public List<Reservation> GetAllByHostJmbg(string hostJmbg)
        {
            List<Reservation> reservations = reservationRepository.GetAll();
            return reservations.Where(r => r.HostJmbg == hostJmbg && r.Status != ReservationStatus.Canceled && r.Status != ReservationStatus.Rejected).ToList();

        }

        public void CancelReservation(Reservation reservation)
        {
            Apartment apartment = apartmentService.FindApartmentByReservation(reservation);
            Hotel hotel = hotelService.FindHotelByApartment(apartment);

            if (apartment != null)
            {
                reservation.Status = ReservationStatus.Canceled;
                reservation.Deleted = true;
                Update(reservation);
                apartmentService.UpdateApartmentReservationCancel(reservation, apartment);

                if (hotel != null)
                {
                    hotelService.UpdateHotelReservationCancel(reservation, hotel);
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
                Update(reservation);
                apartmentService.UpdateApartmentReservationCancel(reservation, apartment);

                if (hotel != null)
                {
                    hotelService.UpdateHotelReservationCancel(reservation, hotel);
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
                Update(reservation);
                apartmentService.UpdateApartmentReservationApprove(reservation, apartment);

                if (hotel != null)
                {
                    hotelService.UpdateHotelReservationApprove(reservation, hotel);
                }
            }
        }


    }
}
