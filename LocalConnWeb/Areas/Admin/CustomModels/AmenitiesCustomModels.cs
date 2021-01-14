using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class AmenitiesView
    {
        public long AmenitiesID { get; set; }
        public string AmenitiesName { get; set; }
        public decimal AmenitiesBasePrice { get; set; }
    }
    public class AmenitiesAPIVM
    {
        public IEnumerable<AmenitiesView> Amenities { get; set; }
        public int TotalRecords { get; set; }
    }
    public class AmenitiesVM
    {
        public IEnumerable<AmenitiesView> Amenities { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class AmenitiesSaveModel
    {
        public utblLCMstAmenitie Amenities { get; set; }
    }
}