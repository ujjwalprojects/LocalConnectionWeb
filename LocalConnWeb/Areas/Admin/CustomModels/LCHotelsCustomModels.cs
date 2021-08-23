using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections;
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
        public Int16 MaxOccupant { get; set; }
        public Int16 MaxRooms { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
        public Int16 TwoOccupantPercentage { get; set; }
        public Int16 ThreeOccupantPercentage { get; set; }
        public Int16 FourPlusOccupantPercentage { get; set; }
        public string ChildOccupantNote { get; set; }
        public bool IsActive { get; set; }
        public string RoomType { get; set; }
        public decimal RoomTypePrice { get; set; }
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

        [Required(ErrorMessage = "Enter Max Occupant Per Room")]
        [Display(Name = "Max Occupant")]
        public Int16 MaxOccupant { get; set; }

        [Required(ErrorMessage = "Enter Max Room")]
        [Display(Name = "Max Rooms")]
        public Int16 MaxRooms { get; set; }

        [Required(ErrorMessage = "Enter Offer %")]
        [Display(Name = "Overall Offer Percentage")]
        public Int16 OverallOfferPercentage { get; set; }

        [Required(ErrorMessage = "Enter 2 Occupant %")]
        [Display(Name = "Discount % for 2")]
        public Int16 TwoOccupantPercentage { get; set; }

        [Required(ErrorMessage = "Enter 3 Occupant %")]
        [Display(Name = "Discount % for 3")]
        public Int16 ThreeOccupantPercentage { get; set; }

        [Required(ErrorMessage = "Enter 4 Plus Occupant %")]
        [Display(Name = "Discount % for 4+")]
        public Int16 FourPlusOccupantPercentage { get; set; }


        [Required(ErrorMessage = "Enter Child Occupant Note")]
        [Display(Name = "Child Occupant Note")]
        public string ChildOccupantNote { get; set; }

        [Display(Name = "Hotel Map Latitute/Longitude")]
        public string LatLong { get; set; }

        [Required(ErrorMessage = "Select to enaple hotel")]
        [Display(Name = "Enable/Disable Hotel")]
        public bool IsActive { get; set; }

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

        [Required(ErrorMessage = "Enter Max Occupant Per Room")]
        [Display(Name = "Max Occupant")]
        public Int16 MaxOccupant { get; set; }

        [Required(ErrorMessage = "Enter Max Room")]
        [Display(Name = "Max Rooms")]
        public Int16 MaxRooms { get; set; }

        [Required(ErrorMessage = "Enter Offer %")]
        [Display(Name = "Overall Offer Percentage")]
        public Int16 OverallOfferPercentage { get; set; }

        [Required(ErrorMessage = "Enter 2 Occupant %")]
        [Display(Name = "Discount % for 2")]
        public Int16 TwoOccupantPercentage { get; set; }

        [Required(ErrorMessage = "Enter 3 Occupant %")]
        [Display(Name = "Discount % for 3")]
        public Int16 ThreeOccupantPercentage { get; set; }

        [Required(ErrorMessage = "Enter 4 Plus Occupant %")]
        [Display(Name = "Discount % for 4+")]
        public Int16 FourPlusOccupantPercentage { get; set; }


        [Required(ErrorMessage = "Enter Child Occupant Note")]
        [Display(Name = "Child Occupant Note")]
        public string ChildOccupantNote { get; set; }


        [Required(ErrorMessage = "Select to enaple hotel")]
        [Display(Name = "Enable/Disable Hotel")]
        public string IsActive { get; set; }


        //[Required(ErrorMessage = "Enter Hotel Base Price")]
        //[Display(Name = "Hotel Base Fare")]
        //public decimal HotelBaseFare { get; set; }
        //[Required(ErrorMessage = "Enter Hotel Offer Price")]
        //[Display(Name = "Hotel Offer Price")]
        //public decimal HotelOfferPrice { get; set; }
        //[Required(ErrorMessage = "Enter Offer Percentage")]
        //[Display(Name = "Hotel Offer Percentage")]
        //public int OfferPercentage { get; set; }
        ////public int HotelHitCount { get; set; }
        //[Required(ErrorMessage = "Enter Tags")]
        //[Display(Name = "Enter Tags")]
        //public string MetaText { get; set; }
        //[Required(ErrorMessage = "Enter No of Single Rooms")]
        //[Display(Name = "Total Single Rooms")]
        //public int TotalSingleRooms { get; set; }
        //[Required(ErrorMessage = "Enter No of Double Rooms")]
        //[Display(Name = "Total Double Rooms")]
        //public int TotalDoubleRooms { get; set; }
        public List<long> RoomID { get; set; }
    }
    public class LCHotelManageModel
    {
        public LCEditHotelSaveModel LCEditHotel { get; set; }
        public LCHotelSaveModel LCHotel { get; set; }
        public HotelBriefInfo HotelInfo { get; set; }
        public IEnumerable<CountryDD> CountryList { get; set; }
        public IEnumerable<StateDD> StateList { get; set; }
        public IEnumerable<CitiesDD> CitiesList { get; set; }
        public IEnumerable<LocalitiesDD> LocalitiesList { get; set; }
        public IEnumerable<utblLCMstHomeType> HomeTypeList { get; set; }
        public IEnumerable<utblLCMstStarRating> StarRatingList { get; set; }
        public IEnumerable<RoomsTypeDD> RoomsList { get; set; }
        public HotelRoomTypeMap HotelRoomTypeMap { get; set; }
        public IEnumerable<HotelRoomTypeMapView> HotelRoomTypeMapView { get; set; }

        public List<HotelTerms> Terms { get; set; }
        public List<HotelCancellations> Cancellations { get; set; }

        public List<LCNearByPointsView> LCNearByPointsView { get; set; }
        public List<LCNearBysTypeDD> LCNearByPointsDD { get; set; }
        public LCNearByPoints LCNearByPoints { get; set; }
        public utblLCNearByPoint NearByPoints { get; set; }

        public IEnumerable<AmenitiesDD> AmenitiesDD { get; set; }
        public List<HotelAmenitiesMapView> HotelAmenitiesMapView { get; set; }
        public utblLCHotelAmenitiesMap HotelAmenitiesMap { get; set; }

        public long HotelID { get; set; }
        
    }
    public class LCHotelDD
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
    }
    public class HotelBriefInfo
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
        [Display(Name = "Hotel Premises")]
        public long  HotelPremID { get; set; }
        [Display(Name = "Room Type")]
        public long? RoomID { get; set; }
        [Display(Name = "Room Cover")]
        public bool IsRoomCover { get; set; }
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
        public IEnumerable<utblLCMstHotelPremise> HotelPremiseDD { get; set; }
        public IEnumerable<RoomsTypeDD> RoomTypeDD { get; set; }
        public ImageStrs ImageStrs { get; set; }
    }
    
    //Hotel Room Type Map
    public class HotelRoomTypeMap
    {
        public long HotelID { get; set; }

        [Required(ErrorMessage = "Select Room Type")]
        [Display(Name = "Room Type")]
        public long RoomID { get; set; }

        [Required(ErrorMessage = "Enter Room Rate")]
        [Display(Name = "Room Rate")]
        public decimal RoomTypePrice { get; set; }
        public bool IsStandard { get; set; }
    }
    public class HotelRoomTypeMapView
    {
        public long HotelID { get; set; }
        public long RoomID { get; set; }
        public string RoomType { get; set; }
        public decimal RoomTypePrice { get; set; }
        public bool IsStandard { get; set; }
    }

    //LCHotel Terms and cancellations
    public class HotelTerms
    {
        public long HotelTermsID { get; set; }
        public long HotelID { get; set; }
        public long TermID { get; set; }
        public string TermName { get; set; }
        public bool IsSelected { get; set; }
    }
    public class HotelCancellations
    {
        public long HotelCancID { get; set; }
        public long HotelID { get; set; }
        public long CancellationID { get; set; }
        public string CancellationDesc { get; set; }
        public bool IsSelected { get; set; }
    }

    //LCNearByPoints
    public class LCNearBysTypeDD
    {
        public long NearByID { get; set; }
        public string NearByName { get; set; }
    }
    public class LCNearByPoints
    {
        public long NearbyPointsID { get; set; }
        [Required(ErrorMessage = "Select Type")]
        [Display(Name = "Type")]
        public long NearByID { get; set; }
        public long HotelID { get; set; }
        [Required(ErrorMessage = "Enter Point Name")]
        [Display(Name = "Point Name")]
        public string NearByPoints { get; set; }
        [Required(ErrorMessage = "Enter Distance")]
        [Display(Name = "Distance")]
        public string NearByDistance { get; set; }
    }
    public class LCNearByPointsView
    {
        public long NearbyPointsID { get; set; }
        public long NearByID { get; set; }
        public long HotelID { get; set; }
        
        public string NearByName { get; set; }
        
        public string NearByPoints { get; set; }
      
        public string NearByDistance { get; set; }
    }
    public class LCNearByPointsAPIVM
    {
        public IEnumerable<LCNearByPointsView> LCNearByPointView { get; set; }
        public IEnumerable<LCNearBysTypeDD> NearBysDD { get; set; }
        public int TotalRecords { get; set; }
    }
    public class LCNearByPointsVM
    {
        public IEnumerable<LCNearByPointsView> LCNearByPointView { get; set; }
        public IEnumerable<LCNearBysTypeDD> NearBysDD { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class LCNearByPointsSaveModel
    {
        public utblLCNearByPoint NearPoints { get; set; }
        public LCNearByPoints LCNearByPoints { get; set; }
        public List<LCNearBysTypeDD> LCNearByPointsDD { get; set; }
    }

    //LCHotelAmenities
    public class AmenitiesDD
    {
        public long AmenitiesID { get; set; }
        public string AmenitiesName { get; set; }
    }
    public class HotelAmenitiesMapView
    {
        public long HotelAmenitiesMapID { get; set; }
        public long HotelID { get; set; }
        public long AmenitiesID { get; set; }
        public string AmenitiesName { get; set; }
        public decimal AmenitiesBasePrice { get; set; }
        public bool IsSelected { get; set; }
    }

    //LCCustomerBookingDetails
    public class LCCustomerBookingAPIVM
    {
        public IEnumerable<LCCustomerBookingView> LCCustomerBookingView { get; set; }
        public int TotalRecords { get; set; }
    }
    public class LCCustomerBookingVM
    {
        public IEnumerable<LCCustomerBookingView> LCCustomerBookingView { get; set; }
        public PagingInfo PagingInfo { get; set; }

    }
    public class LCCustomerBookingView
    {
        public string BookingID { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string CustPhNo { get; set; }
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingUpto { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustDetails { get; set; }
        public string BookingStatus { get; set; }
        public decimal FinalFare { get; set; }
        public string paymentstatus { get; set; }
        public DateTime PaymentDate { get; set; }
    }
}