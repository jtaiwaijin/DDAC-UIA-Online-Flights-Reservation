using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Payment
    {
        public int ID { get; set; }
        public int BookingID { get; set; }
        public Booking Booking { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Amount { get; set; }
    }
}
