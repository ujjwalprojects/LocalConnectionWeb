using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCHotelLatLong
    {
        [Key]
        public long LatLongID { get; set; }
        public long HotelID { get; set; }
        public string LatLong { get; set; }
    }
}