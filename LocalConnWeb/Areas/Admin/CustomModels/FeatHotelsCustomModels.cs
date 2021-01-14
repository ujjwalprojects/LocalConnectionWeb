using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class FeatHotelsView
    {
        public long FeatureID { get; set; }
        public long HotelID { get; set; }
        public string HotelName { get; set; }
        public DateTime FeatureStartDate { get; set; }
        public DateTime FeatureEndDate { get; set; }
    }
    public class FeatHotelsAPIVM
    {
        public IEnumerable<FeatHotelsView> FeatHotels { get; set; }
        public int TotalRecords { get; set; }
    }
    public class FeatHotelsVM
    {
        public IEnumerable<FeatHotelsView> FeatHotels { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class FeatHotelsSaveModel
    {
        public utblLCFeaturedHotel FeatHotels { get; set; }
        public IEnumerable<LCHotelDD> LCHotelDD { get; set; }
    }
}