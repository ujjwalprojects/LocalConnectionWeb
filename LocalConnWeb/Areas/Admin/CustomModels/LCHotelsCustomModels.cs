using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class LCHotelView
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelContactNo { get; set; }
        public string HotelEmail { get; set; }
        public string HomeTypeName { get; set; }
        public string StateName { get; set; }
        public string CityName { get; set; }
        public string LocalityName { get; set; }
        public string StarRatingName { get; set; }
        public decimal HotelBaseFare { get; set; }
        public decimal HotelOfferPrice { get; set; }
        public int OfferPercentage { get; set; }
        public int TotalSingleRooms { get; set; }
        public int TotalDoubleRooms { get; set; }
    }
    public class LCHotelAPIVM
    {
        public IEnumerable<LCHotelView> LCHotels { get; set; }
        public int TotalRecords { get; set; }
    }
    public class LCHotelVM
    {
        public IEnumerable<LCHotelView> LCHotels { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class LCHotelSaveModel
    {
        public long HotelID { get; set; }
        [Required(ErrorMessage = "Enter Hotel Name")]
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string HotelAddress { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        [Display(Name = "Desctiption")]
        public string HotelDesc { get; set; }
        [Required(ErrorMessage = "Enter Contact No")]
        [Display(Name = "Contact No")]
        public string HotelContactNo { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        public string HotelEmail { get; set; }
        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public long CountryID { get; set; }
        [Required(ErrorMessage = "Select State")]
        [Display(Name = "State")]
        public long StateID { get; set; }
        [Required(ErrorMessage = "Select City")]
        [Display(Name = "City")]
        public long CityID { get; set; }
        [Required(ErrorMessage = "Select Locality")]
        [Display(Name = "Locality")]
        public long LocalityID { get; set; }
        [Required(ErrorMessage = "Select Home Type")]
        [Display(Name = "Home Type")]
        public long HomeTypeID { get; set; }
        [Required(ErrorMessage = "Select Star Rating")]
        [Display(Name = "Star Rating")]
        public long StarRatingID { get; set; }
        [Required(ErrorMessage = "Enter Hotel Base Price")]
        [Display(Name = "Hotel Base Fare")]
        public decimal HotelBaseFare { get; set; }
        [Required(ErrorMessage = "Enter Hotel Offer Price")]
        [Display(Name = "Hotel Offer Price")]
        public decimal HotelOfferPrice { get; set; }
        [Required(ErrorMessage = "Enter Offer Percentage")]
        [Display(Name = "Hotel Offer Percentage")]
        public int OfferPercentage { get; set; }
        //public int HotelHitCount { get; set; }
        [Required(ErrorMessage = "Enter Tags")]
        [Display(Name = "Enter Tags")]
        public string MetaText { get; set; }
        [Required(ErrorMessage = "Enter No of Single Rooms")]
        [Display(Name = "Total Single Rooms")]
        public int TotalSingleRooms { get; set; }
        [Required(ErrorMessage = "Enter No of Double Rooms")]
        [Display(Name = "Total Double Rooms")]
        public int TotalDoubleRooms { get; set; }
    }
    public class LCEditHotelSaveModel
    {
        public long HotelID { get; set; }
        [Required(ErrorMessage = "Enter Hotel Name")]
        [Display(Name = "Hotel Name")]
        public string HotelName { get; set; }
        [Required(ErrorMessage = "Enter Address")]
        [Display(Name = "Address")]
        public string HotelAddress { get; set; }
        [Required(ErrorMessage = "Enter Description")]
        [Display(Name = "Desctiption")]
        public string HotelDesc { get; set; }
        [Required(ErrorMessage = "Enter Contact No")]
        [Display(Name = "Contact No")]
        public string HotelContactNo { get; set; }
        [Required(ErrorMessage = "Enter Email")]
        [Display(Name = "Email")]
        public string HotelEmail { get; set; }
        [Required(ErrorMessage = "Select Country")]
        [Display(Name = "Country")]
        public long CountryID { get; set; }
        [Required(ErrorMessage = "Select State")]
        [Display(Name = "State")]
        public long StateID { get; set; }
        [Required(ErrorMessage = "Select City")]
        [Display(Name = "City")]
        public long CityID { get; set; }
        [Required(ErrorMessage = "Select Locality")]
        [Display(Name = "Locality")]
        public long LocalityID { get; set; }
        [Required(ErrorMessage = "Select Home Type")]
        [Display(Name = "Home Type")]
        public long HomeTypeID { get; set; }
        [Required(ErrorMessage = "Select Star Rating")]
        [Display(Name = "Star Rating")]
        public long StarRatingID { get; set; }
        [Required(ErrorMessage = "Enter Hotel Base Price")]
        [Display(Name = "Hotel Base Fare")]
        public decimal HotelBaseFare { get; set; }
        [Required(ErrorMessage = "Enter Hotel Offer Price")]
        [Display(Name = "Hotel Offer Price")]
        public decimal HotelOfferPrice { get; set; }
        [Required(ErrorMessage = "Enter Offer Percentage")]
        [Display(Name = "Hotel Offer Percentage")]
        public int OfferPercentage { get; set; }
        //public int HotelHitCount { get; set; }
        [Required(ErrorMessage = "Enter Tags")]
        [Display(Name = "Enter Tags")]
        public string MetaText { get; set; }
        [Required(ErrorMessage = "Enter No of Single Rooms")]
        [Display(Name = "Total Single Rooms")]
        public int TotalSingleRooms { get; set; }
        [Required(ErrorMessage = "Enter No of Double Rooms")]
        [Display(Name = "Total Double Rooms")]
        public int TotalDoubleRooms { get; set; }
        public List<long> RoomID { get; set; }
    }
    public class LCHotelManageModel
    {
        public LCEditHotelSaveModel LCEditHotel { get; set; }
        public LCHotelSaveModel LCHotel { get; set; }
        public IEnumerable<CountryDD> CountryList { get; set; }
        public IEnumerable<StateDD> StateList { get; set; }
        public IEnumerable<CitiesDD> CitiesList { get; set; }
        public IEnumerable<LocalitiesDD> LocalitiesList { get; set; }
        public IEnumerable<utblLCMstHomeType> HomeTypeList { get; set; }
        public IEnumerable<utblLCMstStarRating> StarRatingList { get; set; }
        public IEnumerable<RoomsTypeDD> RoomsList { get; set; }
        [Required(ErrorMessage = "Select Room Type")]
        [Display(Name = "Room Type List")]
        public List<long> RoomID { get; set; }
    }
    public class LCHotelDD
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
    }

    //Hotel Image
    public class LCHotelImageView
    {
        public long HotelImageID { get; set; }
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public bool IsHotelCover { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        public string PhotoCaption { get; set; }
    }
    public class LCHotelImageAPIVM
    {
        public IEnumerable<LCHotelImageView> LCHotelImageList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class LCHotelImageVM
    {
        public IEnumerable<LCHotelImageView> LCHotelImageList { get; set; }
        public utblLCHotel HotelDetails { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class LCHotelImageSaveModel
    {
        public long HotelImageID { get; set; }
        [Display(Name = "Hotel")]
        public long HotelID { get; set; }
        [Display(Name = "Set As Cover Image")]
        public bool IsHotelCover { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        [Display(Name = "Image Title")]
        public string PhotoCaption { get; set; }
    }
    public class LCHotelImageManageModel
    {
        public LCHotelImageSaveModel LCHotelImage { get; set; }
        public utblLCHotel HotelDetails { get; set; }
        public IEnumerable<LCHotelDD> HotelList { get; set; }
        public ImageStrs ImageStrs { get; set; }
    }
}