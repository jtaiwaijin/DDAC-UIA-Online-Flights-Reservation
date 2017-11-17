using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models
{
    public class Booking
    {
        public int ID { get; set; }
        public Guid CustomerId { get; set; }
        public Customer Customer { get; set; }
        [Display(Name = "Booking Date")]
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy HH:mm}", ApplyFormatInEditMode = true)]
        public DateTime BookingDate { get; set; }
        public String Status { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal? Total { get; set; }
        public ICollection<Ticket> Tickets { get; set; }
    }
}
