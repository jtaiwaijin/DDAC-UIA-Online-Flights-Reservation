using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models.FlightBookingViewModels
{
    public class FlightBookingViewModel
    {
        public Flight flight;
        public Booking booking;
        [Required]
        [StringLength(100, ErrorMessage = "Name cannot be longer than 100 characters.")]
        [RegularExpression(@"^[A-Z]+[a-zA-Z''-'\s]*$")]
        public string Name { get; set; }
        [Required]
        [StringLength(1)]
        public string Gender { get; set; }
        [Required]
        [StringLength(12, MinimumLength = 10, ErrorMessage = "Phone cannot be longer than 12 characters.")]
        public string Phone { get; set; }
        [Required]
        public string Email { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        [Display(Name = "Passport Number")]
        [Required]
        public string PassportNo { get; set; }
        [Required]
        public string Nationality { get; set; }
        [Display(Name = "Cabin Class")]
        public string CabinCl { get; set; }
        [DataType(DataType.Currency)]
        [Column(TypeName = "money")]
        public decimal Price { get; set; }
    }
}
