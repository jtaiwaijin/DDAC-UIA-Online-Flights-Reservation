using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Airport
    {
        public int AirportID { get; set; }
        public string IATA { get; set; }
        public string Name { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public double Timezone { get; set; }
        public ICollection<Flight> Departing { get; set; }
        public ICollection<Flight> Arriving { get; set; }
    }
}
