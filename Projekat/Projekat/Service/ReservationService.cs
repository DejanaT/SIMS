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
        private HotelRepository hotelRepository = new HotelRepository();
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

        public void UpdateApartmentReservationCancel(Reservation reservation, Apartment apartment)
        {
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

        public void UpdateHotelReservationCancel(Reservation reservation, Hotel hotel)
        {
            Apartment apartment = apartmentRepository.FindByName(reservation.ApartmentName);
            if (hotel != null)
            {
                var reservationInHotel = hotel.Apartments[apartment.Name].Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInHotel != null)
                {
                    hotel.Apartments[apartment.Name].Reservations.Remove(reservationInHotel);
                    hotelRepository.UpdateHotel(hotel);
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
                    apartmentService.Update(apartment);
                }
            }
        }

        public void UpdateHotelReservationApprove(Reservation reservation, Hotel hotel)
        {
            Apartment apartment = apartmentRepository.FindByName(reservation.ApartmentName);
            if (hotel != null && apartment != null)
            {
                var reservationInHotel = hotel.Apartments[apartment.Name].Reservations.FirstOrDefault(r => r.Id == reservation.Id);

                if (reservationInHotel != null)
                {
                    reservationInHotel.Status = ReservationStatus.Accepted;
                    hotelRepository.UpdateHotel(hotel);
                }
            }
        }


        public List<Reservation> GetReservationsByHostJmbg(string hostJmbg, List<Hotel> hotels)
         {
             return reservationRepository.GetReservationsByHostJmbg(hostJmbg, hotels);
         }

        public List<Reservation> GetAllByHostJmbg(string hostJmbg)
        {
            return reservationRepository.GetAllByHostJmbg(hostJmbg);
        }


    }
}
