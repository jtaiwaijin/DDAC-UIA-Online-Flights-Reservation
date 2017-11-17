using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Ticket
    {
        public int ID { get; set; }
        public int FlightID { get; set; }
        public int PassengerId { get; set; }
        public Flight Flight { get; set; }
        public Passenger Passenger { get; set; }
        public Booking Booking { get; set; }
        [Display(Name = "Cabin Class")]
        public string CabinCl { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
