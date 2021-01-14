using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Models
{
    public class utblClientEnquiryItinerarie
    {
        public long ClientItineraryID { get; set; }
        public string EnquiryCode { get; set; }
        public long RefPackageID { get; set; }
        public int DayNo { get; set; }
        [Required(ErrorMessage="Please enter itinerary name")]
        [Display(Name="Itinerary Name")]
        public long ItineraryID { get; set; }
        [Required(ErrorMessage = "Please enter itinerary details")]
        [Display(Name = "Itinerary Desctiption")]
        public string ItineraryRemarks { get; set; }
        [Required(ErrorMessage = "Please select overnight destination")]
        [Display(Name = "Overnight Destination")]
        public long OvernightDestinationID { get; set; }
        public string OvernightDestination { get; set; }
        public bool SightSeeing { get; set; }
        public bool Breakfast { get; set; }
        public bool Lunch { get; set; }
        public bool Dinner { get; set; }
        public bool Stay { get; set; }

    }
}