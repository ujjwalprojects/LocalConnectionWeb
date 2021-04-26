using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class HotelOfferView
    {
        public long OfferID { get; set; }
        public string OfferTagLine { get; set; }
        public string HotelName { get; set; }
        public DateTime OfferStartDate { get; set; }
        public DateTime OfferEndDate { get; set; }

    }
    public class HotelOfferAPIVM
    {
        public IEnumerable<HotelOfferView> HotelOfferList { get; set; }
        public int TotalRecords { get; set; }
    }

    public class HotelOfferVM
    {
        public IEnumerable<HotelOfferView> HotelOfferList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class SaveHotelOffer
    {
        public List<HotelDD> HotelList { get; set; }
        [Required(ErrorMessage = "Select at least single Hotel ")]
        [Display(Name = "Hotel List")]
        public List<long> HotelID { get; set; }
        public HotelOffer HotelOffer { get; set; }
        public Cropper cropper { get; set; }
    }
    public class HotelDD
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
    }

    public class HotelOffer
    {
        public long OfferID { get; set; }
        [Display(Name = "Offer Tagline")]
        [Required(ErrorMessage = "Enter Offer Tagline")]
        public string OfferTagLine { get; set; }
        [Display(Name = "Offer Image Path")]
        [Required(ErrorMessage = "Enter Offer Image Path")]
        public string OfferImagePath { get; set; }
        [Display(Name = "Select Start Date")]
        [Required(ErrorMessage = "Select Start Date")]
        public DateTime OfferStartDate { get; set; }
        [Display(Name = "Select End Date")]
        [Required(ErrorMessage = "Select End Date")]
        public DateTime OfferEndDate { get; set; }

        public List<long> HotelID { get; set; }
    }
}