using LocalConnWeb.Areas.Admin.CustomModels;
using LocalConnWeb.Areas.Admin.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.ViewModels
{
    public class HomePageVM
    {
        public List<CityList> cityLists { get; set; }
        public List<HotelList> hotelList { get; set; }
        public List<HotelList> resortList { get; set; }
        public List<HotelList> lodgeList { get; set; }
        public List<HotelList> guestHouseList { get; set; }
        public List<HotelList> homestayList { get; set; }
        public List<FtHotelList_Web> FHotelList  { get; set; }
        public List<OfferList> offerLists { get; set; }
        public List<utblMstBanner> bannerlist { get; set; }
    }

    public class HotelDetailsVM
    {
        public HotelDtl hotelDtl { get; set; }
        public List<HAmenitiesList> hAmenities { get; set; }
        public List<HotelPremisesList> hotelPremises { get; set; }
        public List<HotelRoomList> hotelRoomLists { get; set; }
        public HotelRoomTab hotelRoomImgTab  { get; set; }
        public NearbyVM nearByVM { get; set; }
        public TermCondPolicyVM termCondVM { get; set; }
        public PreBookingDtl preBookDtl { get; set; }
        public PreBookingDtl preBookDtls { get; set; }
        public string BookFrom { get; set; }
        public string BookUpTo { get; set; }
        public int NoofDays { get; set; }
        public List<string> amenitesDisplay{ get; set; }
        public string  selectedRoomType { get; set; }
    }

 

    public class OrderListVM
    {
        public List<OrderList> orderLists { get; set; }
    }

    public class GenLCHotelSearchModel
    {
        public IEnumerable<HotelList> hotelList { get; set; }
        public IEnumerable<HotelList_Offer> hotelofferlist { get; set; }
        public int TotalRecords { get; set; }
        public GenLCSearchModel Search { get; set; }
        public IEnumerable<utblLCMstHomeType> HomeTypes { get; set; }
    }

    public class GenLCSearchModel
    {
        public string Where { get; set; }
        public List<long> HomeType { get; set; }
        public int PageNo { get; set; }
        public int PageSize { get; set; }
    }
}