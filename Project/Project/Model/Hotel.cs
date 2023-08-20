using System.Collections.Generic;

namespace Project.Model
{
    public class Hotel : BaseModel
    {
        public string HotelCode { get; set; }
        public string HotelName { get; set; }
        public int YearOfConstruction { get; set; }
        public IDictionary<string, Apartment> Apartments { get; set; }
        public int NumberOfStars { get; set; }
        public string HostJmbg { get; set; }

    }
}
