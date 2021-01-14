using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblTourPackage
    {
        public long PackageID { get; set; }
        [Required(ErrorMessage = "Enter Package Name")]
        [Display(Name = "Package Name")]
        public string PackageName { get; set; }
        [Required(ErrorMessage = "Select Package Type")]
        [Display(Name = "Package Type")]
        public long PackageTypeID { get; set; }
        [Display(Name = "Package Routing")]
        public string PackageRouting { get; set; }
        [Display(Name = "Pickup Point")]
        public string PickupPoint { get; set; }
        [Display(Name = "Drop Point")]
        public string DropPoint { get; set; }
        [Required(ErrorMessage = "Enter Number of Days")]
        [Range(1, 20, ErrorMessage = "Must be between 1-20")]
        [Display(Name = "No. Of Days")]
        public int TotalDays { get; set; }
        [Display(Name = "Base Fare")]
        public decimal BaseFare { get; set; }
        [Display(Name = "Package Description")]
        public string PackageDesc { get; set; }
        public string LinkText { get; set; }
        [Display(Name = "Meta Text")]
        public string MetaText { get; set; }
        [Display(Name = "Meta Description")]
        public string MetaDesc { get; set; }
        public bool ShowPackage { get; set; }
        [Required(ErrorMessage = "Select Fare Per")]
        [Display(Name = "Fare Per")]
        public string FarePer { get; set; }
    }
}