using Project.Model;
using Projekat.Model.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Projekat.Model
{
    public class Reservation : BaseModel
    {
        public string GuestJmbg { get; set; }
        public string HostJmbg { get; set; }
        public string ApartmentName { get; set; }
        public string ReservationDate { get; set; }
        public ReservationStatus Status { get; set; }
        public string ReasonReject { get; set; }

    }
}
