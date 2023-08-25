using Projekat.Model;
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
    }
}
