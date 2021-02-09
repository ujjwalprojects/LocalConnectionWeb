using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCMstAmenitie
    {
        [Key]
        public long AmenitiesID { get; set; }
        [Display(Name = "Amenities Name")]
        public string AmenitiesName { get; set; }
        [Display(Name = "Amenities Icon")]
        public string AmenitiesIconPath { get; set; }
        [Display(Name = "Base Price")]
        public decimal AmenitiesBasePrice { get; set; }
    }
}