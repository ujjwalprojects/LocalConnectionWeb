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
        public string HotelContact { get; set; }
        public string HotelEmail { get; set; }
        public string HotelTypes { get; set; }
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
        [Required(ErrorMessage = "Enter Hotel Base Fare")]
        [Display(Name = "Hotel Base Fare")]
        public decimal HotelBaseFare { get; set; }
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
    public class LCHotelManageModel
    {
        public LCHotelSaveModel LCHotel { get; set; }
        public IEnumerable<CountryDD> CountryList { get; set; }
        public IEnumerable<StateDD> StateList { get; set; }
        public IEnumerable<CitiesDD> CitiesList { get; set; }
        public IEnumerable<LocalitiesDD> LocalitiesList { get; set; }
        public IEnumerable<utblLCMstHomeType> HomeTypeList { get; set; }
        public IEnumerable<utblLCMstStarRating> StarRatingList { get; set; }
    }
}