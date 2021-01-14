using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblTourGuide
    {
        public long TourGuideID { get; set; }
        [Required(ErrorMessage="Enter Tour Guide Name")]
        public string TourGuideName { get; set; }
        [Required]
        public string TourGuideDesc { get; set; }
        public string TourGuideLink { get; set; }
    }
}