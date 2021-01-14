using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstDestination
    {
        public long DestinationID { get; set; }
        [Required(ErrorMessage="Select Country")]
        [Display(Name="Country")]
        public long CountryID { get; set; }
        [Required(ErrorMessage="Select State")]
        [Display(Name="State")]
        public long StateID { get; set; }
        [Required(ErrorMessage="Enter Destination Name")]
        [Display(Name="Destination Name")]
        public string DestinationName { get; set; }
        [Display(Name="Destination Description")]
        public string DestinationDesc { get; set; }
        public string DestinationImagePath { get; set; }
    }
}