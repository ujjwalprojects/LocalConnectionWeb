using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstitinerarie
    {
        public long ItineraryID  { get; set; }
        [Required(ErrorMessage="Enter Itinerary Name")]
        [Display(Name="Itinerary Name")]
        public string ItineraryName { get; set; }
        [Required(ErrorMessage="Enter Itinerary Description")]
        [Display(Name="Itinerary Description")]
        public string ItineraryDesc { get; set; }
        [Display(Name="Select Overnight Destination (Optional)")]
        public long? OvernightDestinationID { get; set; }

    }
}