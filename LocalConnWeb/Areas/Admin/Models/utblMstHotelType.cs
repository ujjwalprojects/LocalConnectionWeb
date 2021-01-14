using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstHotelType
    {
        public long HotelTypeID { get; set; }
        [Required(ErrorMessage = "Enter Hotel Type Name")]
        [Display(Name = "Hotel Type")]
        public string HotelTypeName { get; set; }
        [Required(ErrorMessage = "Enter Base Fare")]
        [Display(Name = "Base Fare")]
        public decimal BaseFare { get; set; }
    }
}