using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Tariff
    {
        public int ID { get; set; }
        public int FlightID { get; set; }
        public Flight Flight { get; set; }
        [Display(Name = "Cabin Class")]
        public string CabinCl { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal AdultFare { get; set; }
    }
}
