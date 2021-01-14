using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstHotel
    {
        public long HotelID { get; set; }
        public long DestinationID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelContact { get; set; }
        public string HotelEmail { get; set; }
    }
}