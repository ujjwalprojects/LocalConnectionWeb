using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCHotelAmenitiesMap
    {
        [Key]
        public long HotelAmenitiesMapID { get; set; }
        public long HotelID { get; set; }
        public long AmenitiesID { get; set; }
    }

}