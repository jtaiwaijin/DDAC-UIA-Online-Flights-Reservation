using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace uiaflights.Models.ManageViewModels
{
    public class ProfileViewModel
    {
        public string Name { get; set; }
        public string Gender { get; set; }
        [Display(Name = "Date of Birth")]
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime DOB { get; set; }
        public string PhoneNumber { get; set; }
        public string Email {get;set;}
        [Display(Name = "Address Line 1")]
        public string AddLine1 { get; set; }
        [Display(Name = "Address Line 2")]
        public string AddLine2 { get; set; }
        public string City { get; set; }
        public int Postcode { get; set; }
        public string Region { get; set; }
        public string Country { get; set; }
    }
}
