using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstCab
    {
        public long CabID { get; set; }
        [Required(ErrorMessage = "Enter Cab Name")]
        [Display(Name = "Cab Name")]
        public string CabName { get; set; }
        [Required(ErrorMessage = "Enter Cab No")]
        [Display(Name = "Cab No")]
        public string CabNo { get; set; }
        [Required(ErrorMessage = "Select Cab Tye")]
        [Display(Name = "Cab Type")]
        public long CabTypeID { get; set; }
        [Required(ErrorMessage = "Select Cab Head")]
        [Display(Name = "Cab Head")]
        public long CabHeadID { get; set; }
    }
}