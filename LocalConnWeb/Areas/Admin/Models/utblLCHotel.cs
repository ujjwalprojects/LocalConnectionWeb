using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCHotel
    {
        [Key]
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public string HotelAddress { get; set; }
        public string HotelDesc { get; set; }
        public string HotelContactNo { get; set; }
        public string HotelEmail { get; set; }
        public long CountryID { get; set; }
        public long StateID { get; set; }
        public long CityID { get; set; }
        public long LocalityID { get; set; }
        public long HomeTypeID { get; set; }
        public long StarRatingID { get; set; }
        public Int16 MaxOccupant { get; set; }
        public Int16 OverallOfferPercentage { get; set; }
        public Int16 TwoOccupantPercentage { get; set; }
        public Int16 ThreeOccupantPercentage { get; set; }
        public Int16 FourPlusOccupantPercentage { get; set; }
        public string ChildOccupantNote { get; set; }
        public bool IsActive { get; set; }
    }
}