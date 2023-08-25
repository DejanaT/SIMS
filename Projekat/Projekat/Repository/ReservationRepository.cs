using Projekat.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Repository
{
    public class ReservationRepository
    {
        private List<Reservation> reservations;
        private JsonSerializer<Reservation> reservationSerializer = new JsonSerializer<Reservation>();
        private string path = @"C:\Users\dejana\Desktop\SIMS\Projekat\Projekat\Data\reservations.json";

        public List<Reservation> GetAll()
        {
            reservations = reservationSerializer.LoadDataFromJson(path) as List<Reservation>;
            return reservations;
        }

        public Reservation GetById(string id)
        {
            reservations = GetAll();

            foreach (Reservation r in reservations)
            {
                if (r.Id.Equals(id))
                {
                    return r;
                }
            }
            return null;
        }

        public void SaveChanges(List<Reservation> reservations)
        {
            reservationSerializer.SaveDataToJson(reservations, path);
        }

        public void AddNewReservation(Reservation reservation)
        {
            reservations = GetAll();
            reservations.Add(reservation);
            SaveChanges(reservations);
        }
    }
}
