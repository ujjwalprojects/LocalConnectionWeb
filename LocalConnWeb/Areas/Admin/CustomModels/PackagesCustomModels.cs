using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Helpers;
using LocalConnWeb.Areas.Admin.Models;
using System.ComponentModel.DataAnnotations;
namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class PackageView
    {
        public long PackageTypeID { get; set; }
        public string PackageTypeName { get; set; }
        public bool IsVisible { get; set; }
    }
    public class PackageAPIVM
    {
        public IEnumerable<PackageView> PackageList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class PackageVM
    {
        public IEnumerable<PackageView> PackageList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class PackageSaveModel
    {
        public utblMstPackageType PackageType { get; set; }
    }

    public class PackageOfferView
    {
        public long PackageOfferID { get; set; }
        public string PackageName { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }

    }
    public class GenPackageOfferView
    {
        public long PackageOfferID { get; set; }
        public string OfferDesc { get; set; }
        public string OfferImagePath { get; set; }
    }
    public class PackageOfferAPIVM
    {
        public IEnumerable<PackageOfferView> PackageOfferList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class PackageOfferVM
    {
        public IEnumerable<PackageOfferView> PackageOfferList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class PackageOffer
    {
        public long PackageOfferID { get; set; }
        [Display(Name = "Offer Discount Percentage")]
        [Required(ErrorMessage = "Enter Offer Discount Percentage")]
        public Int16 OfferDiscount { get; set; }
        [Display(Name = "Offer Description")]
        [Required(ErrorMessage = "Enter Offer Description")]
        public string OfferDesc { get; set; }
        [Display(Name = "Offer Image Path")]
        [Required(ErrorMessage = "Enter Offer Image Path")]
        public string OfferImagePath { get; set; }
        [Display(Name = "Select Start Date")]
        [Required(ErrorMessage = "Select Start Date")]
        public DateTime StartDate { get; set; }
        [Display(Name = "Select End Date")]
        [Required(ErrorMessage = "Select End Date")]
        public DateTime EndDate { get; set; }

        public List<long> PackageID { get; set; }
    }
    public class SavePackageOffer
    {
        public List<PackageDD> PackageList { get; set; }
        [Required(ErrorMessage = "Select at least single Package ")]
        [Display(Name = "Package List")]
        public List<long> PackageID { get; set; }
        public PackageOffer PackageOffer { get; set; }
        public Cropper cropper { get; set; }
    }

    public class EditPackageOffer
    {
        public utblPackageOffer PackageOffer { get; set; }
        public List<PackageDD> PackageList { get; set; }
        [Required(ErrorMessage = "Select Package")]
        [Display(Name = "Package List")]
        public long PackageID { get; set; }
    }

    public class PackageDD
    {
        public long PackageID { get; set; }
        public string PackageName { get; set; }
    }


}