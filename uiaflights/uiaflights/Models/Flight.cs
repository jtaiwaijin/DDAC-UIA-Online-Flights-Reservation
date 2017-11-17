using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Flight
    {
        public int ID { get; set; }
        [Display(Name = "Flight No")]
        public string FlightNo { get; set; }
        public int OriginAirportID { get; set; }
        public Airport Origin { get; set; }
        public int DestAirportID { get; set; }
        [Display(Name = "Destination")]
        public Airport Dest { get; set; }
        [Display(Name = "Departure Date Time")]
        [DisplayFormat(DataFormatString = "{0:ddd, dd/MM/yyyy, HHmm}", ApplyFormatInEditMode = true)]
        public DateTime DepartureDateTime { get; set; }
        [Display(Name = "Arrival Date Time")]
        [DisplayFormat(DataFormatString = "{0:ddd, dd/MM/yyyy, HHmm}", ApplyFormatInEditMode = true)]
        public DateTime ArrivalDateTime { get; set; }
        public int PlaneID { get; set; }
        public Plane Plane { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
        public ICollection<Tariff> Tariff { get; set; }
    }
}
