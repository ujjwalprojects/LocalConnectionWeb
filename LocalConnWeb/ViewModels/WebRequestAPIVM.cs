using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.ViewModels
{
    public class HotelList
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public decimal RoomTypePrice { get; set; }
        public string HotelDesc { get; set; }
        public string HotelAddress { get; set; }
        public long HomeTypeID { get; set; }
        public long HotelImageID { get; set; }
        public bool IsHotelCover { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoCaption { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
        public bool IsActive { get; set; }
        public decimal OfferPrice { get; set; }
    }
    public class HotelList_Offer
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public decimal RoomTypePrice { get; set; }
        public string HotelDesc { get; set; }
        public string HotelAddress { get; set; }
        public long HomeTypeID { get; set; }
        public string HomeTypeName { get; set; }
        public long HotelImageID { get; set; }
        public bool IsHotelCover { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoCaption { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
        public bool IsActive { get; set; }
        public decimal OfferPrice { get; set; }
    }
    public class CityList
    {
        public long CityID { get; set; }
        public string CityName { get; set; }
        public string CityIconPath { get; set; }
    }
    public class FtHotelList_Web
    {
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string PhotoThumbPath { get; set; }
        public decimal RoomTypePrice { get; set; }
        public decimal OfferPrice { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
    }

    public class HotelDtl
    {
        public long HotelID { get; set; }
        public string PhotoThumbPath { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelDesc { get; set; }
        public string LocalityName { get; set; }
        public decimal RoomTypePrice { get; set; }
        public string DefaultRoomType { get; set; }
        public Int16 MaxOccupant { get; set; }
        public Int16 MaxRooms { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
        public Int16 TwoOccupantPercentage { get; set; }
        public Int16 ThreeOccupantPercentage { get; set; }
        public Int16 FourPlusOccupantPercentage { get; set; }
        public Int16 MaxOccupantPerRoom { get; set; }
        public string ChildOccupantNote { get; set; }
        public bool IsActive { get; set; }
        public string LatLong { get; set; }
    }
    public class HotelRoomList
    {
        public long HotelID { get; set; }
        public long RoomID { get; set; }
        public string RoomType { get; set; }
        public decimal RoomBaseFare { get; set; }
        public int RoomCapacity { get; set; }
        public bool IsStandard { get; set; }
        public decimal RoomTypePrice { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoCaption { get; set; }
        public Int16 MaxOccupant { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
        public Int16 TwoOccupantPercentage { get; set; }
        public Int16 ThreeOccupantPercentage { get; set; }
        public Int16 FourPlusOccupantPercentage { get; set; }
    }
    public class HotelPremisesList
    {
        public long HotelPremID { get; set; }
        public long HotelID { get; set; }
        public String HotelPremName { get; set; }
    }

    public class HotelRoomTab
    {
        public List<HotelPremisesList> premisesList { get; set; }
        public List<HotelRoomImg> roomImgList { get; set; }
    }
    public class HotelRoomImg
    {
        public long HotelImgID { get; set; }
        public long HotelID { get; set; }
        public long HotelPremID { get; set; }
        public string HotelPremName { get; set; }
        public string PhotoNormalPath { get; set; }
    }


    public class PreBookingDtl
    {
        public string BookingID { get; set; }
        public long HotelID { get; set; }
        [Required(ErrorMessage ="Enter Customer Name")]
        public string CustName { get; set; }
        [Required(ErrorMessage = "Enter Customer Email")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Please Enter Valid email address")]
        public string CustEmail { get; set; }
        [StringLength(15, ErrorMessage = "Please Enter Valid Phone No.", MinimumLength = 10)]
        [RegularExpression("([1-9][0-9]*)", ErrorMessage = "Please enter valid phone number")]
        [Required(ErrorMessage = "Enter Customer Mobile")]
        public string CustPhNo { get; set; }
        public string RoomSelect { get; set; }
        public string AdultSelect { get; set; }
        public string ChildrenSelect { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingUpto { get; set; }
        public DateTime BookingDate { get; set; }
        //
        public string CustDetails { get; set; }
        public string BookingStatus { get; set; }
        public decimal FinalFare { get; set; }
        public string PaymentGatewayCode { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
    }

    public class PreBookingTransDtl
    {
        public string BookingID { get; set; }
        public long HotelID { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string CustPhNo { get; set; }
        public DateTime BookingFrom { get; set; }
        public DateTime BookingUpto { get; set; }
        public DateTime BookingDate { get; set; }
        public string CustDetails { get; set; }
        public string BookingStatus { get; set; }
        public decimal FinalFare { get; set; }
        public string PaymentGatewayCode { get; set; }
        public string HotelName { get; set; }
        public string UserID { get; set; }
        public string UserName { get; set; }
    }

    public class OrderList
    {
        public string BookingID { get; set; }
        public long HotelID { get; set; }
        public string PhotoThumbPath { get; set; }
        public string CustName { get; set; }
        public string CustEmail { get; set; }
        public string CustPhNo { get; set; }
        public string BookingFrom { get; set; }
        public string BookingUpto { get; set; }
        public string CustDetails { get; set; }
        public string BookingDate { get; set; }
        public string BookingStatus { get; set; }
        public decimal FinalFare { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public int IsValidCancel { get; set; }
    }
    public class HAmenitiesList
    {
        public long HotelID { get; set; }
        public long AmenitiesID { get; set; }
        public string AmenitiesName { get; set; }
        public string AmenitiesIconPath { get; set; }
        public decimal AmenitiesBasePrice { get; set; }
    }


    public class OfferList
    {
        public long OfferID { get; set; }
        public string OfferTagLine { get; set; }
        public string OfferImagePath { get; set; }

    }

    public class OfferHotelsList
    {
        public List<HomeTypeOnOffer> homeTypeList { get; set; }
        public List<HotelList> hotelList { get; set; }
    }
    public class HomeTypeOnOffer
    {
        public long OfferID { get; set; }
        public long HomeTypeID { get; set; }
        public string HomeTypeName { get; set; }
    }

    public class TermsPolicyList
    {
        public long HotelTermsID { get; set; }
        public long HotelID { get; set; }
        public string TermName { get; set; }
    }
    public class CancellationPolicyList
    {
        public long CancellationID { get; set; }
        public long HotelID { get; set; }
        public string CancellationDesc { get; set; }
    }
    public class TermCondPolicyVM
    {
        public List<TermsPolicyList> termPolicyList { get; set; }
        public List<CancellationPolicyList> cancelPolicyList { get; set; }
    }


    public class NotificationList
    {
        public long NotificationID { get; set; }
        public string NotificationTitle { get; set; }
        public string NotificationDesc { get; set; }
        public string NotificationImagePath { get; set; }
    }

    public class NearbyList
    {
        public long NearByID { get; set; }
        public long HotelID { get; set; }
        public string NearByPoints { get; set; }
        public string NearByDistance { get; set; }
    }
    public class NearbyVM
    {
        public List<NearbyList> nearbyone { get; set; }
        public List<NearbyList> nearbytwo { get; set; }
    }
    public class HelpPageDtl
    {
        public string HelpPageTitle { get; set; }
        public string HelpPageContent { get; set; }
        public string HelpPageImgPath { get; set; }
        public string HelpPageContactNo { get; set; }
        public string HelpPageEmailID { get; set; }
    }

    public class AboutUsDetails
    {
        public long AboutID { get; set; }
        public String AboutContent { get; set; }
    }

    public class PolicyList
    {
        public List<PolicyTitle> PolicyTitle { get; set; }
        public List<PolicyData> PolicyData { get; set; }

    }
    public class PolicyData
    {
        public long PolicyPointID { get; set; }
        public long PolicyID { get; set; }
        public String PolicyPoints { get; set; }
    }
    public class PolicyTitle
    {
        public long PolicyID { get; set; }
        public String PolicyTitleName { get; set; }
    }

}