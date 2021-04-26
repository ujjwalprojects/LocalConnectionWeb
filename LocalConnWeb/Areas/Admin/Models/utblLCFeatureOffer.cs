using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCFeatureOffer
    {
        public long OfferID { get; set; }
        [Display(Name = "Offer Tagline")]
        [Required(ErrorMessage = "Enter Offer Tagline")]
        public string OfferTagLine { get; set; }
        [Display(Name = "Offer Banner Image")]
        [Required(ErrorMessage = "Enter Offer Banner Image")]
        public string OfferImagePath { get; set; }
        [Display(Name = "Select Start Date")]
        [Required(ErrorMessage = "Select Start Date")]
        public DateTime OfferStartDate { get; set; }
        [Display(Name = "Select End Date")]
        [Required(ErrorMessage = "Select End Date")]
        public DateTime OfferEndDate { get; set; }
    }
}