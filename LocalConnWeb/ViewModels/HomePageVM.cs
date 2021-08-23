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
}