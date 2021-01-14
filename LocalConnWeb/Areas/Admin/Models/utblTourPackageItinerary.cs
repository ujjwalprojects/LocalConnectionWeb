using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblTourPackageItinerary
    {
        public long PackageItineraryID { get; set; }
        public long PackageID { get; set; }
        [Required(ErrorMessage="Select Itinerary")]
        [Display(Name="Day Itinerary")]
        public long ItineraryID { get; set; }
        [Required]
        public int DayNo { get; set; }
        [Required(ErrorMessage="Enter Itinerary Details")]
        [Display(Name = "Itinerary Details")]
        public string ItineraryRemarks { get; set; }
        [Display(Name = "Overnight Destination")]
        public long? OvernightDestinationID { get; set; }
        public string OvernightDestination { get; set; }
        public bool SightSeeing { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }
        public bool Stay { get; set; }
    }
}