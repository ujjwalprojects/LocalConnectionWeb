using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstState
    {
        public long StateID { get; set; }
        [Required(ErrorMessage = "Select Country")]
        [Display(Name="Country")]
        public long CountryID { get; set; }
        [Required(ErrorMessage = "Enter State Name")]
        [Display(Name="State Name")]
        public string StateName { get; set; }
    }
}