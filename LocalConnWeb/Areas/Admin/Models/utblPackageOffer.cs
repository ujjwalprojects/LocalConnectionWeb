using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblPackageOffer
    {
        public long PackageOfferID { get; set; }
        [Display(Name = "Offer Discount Percentage")]
        [Required(ErrorMessage = "Enter Offer Discount Percentage")]
        public Int16 OfferDiscount { get; set; }
        [Display(Name = "Offer Description")]
        [Required(ErrorMessage = "Enter Offer Description")]
        public string OfferDesc { get; set; }
        [Display(Name = "Offer Banner Image")]
        [Required(ErrorMessage = "Enter Offer Banner Image")]
        public string OfferImagePath { get; set; }
        [Display(Name = "Select Start Date")]
        [Required(ErrorMessage = "Select Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Select End Date")]
        [Required(ErrorMessage = "Select End Date")]
        public DateTime EndDate { get; set; }
    }
}