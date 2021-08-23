using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCHotelImage
    {
        [Key]
        public long HotelImageID { get; set; }
        public long HotelID { get; set; }
        public long HotelPremID { get; set; }
        public long? RoomID { get; set; }
        public bool IsRoomCover { get; set; }
        public bool IsHotelCover { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        public string PhotoCaption { get; set; }
    }
}