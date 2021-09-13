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
        public List<FtHotelList> FHotelList { get; set; }
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
    }

    public class OrderListVM
    {
        public List<OrderList> orderLists{ get; set; }
    }
}