using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class DestinationView
    {
        public long DestinationID { get; set; }
        public long CountryID { get; set; }
        public long StateID { get; set; }
        public string CountryName { get; set; }
        public string StateName { get; set; }
        public string DestinationName { get; set; }
        public string DestinationDesc { get; set; }
    }
    public class DestinationAPIVM
    {
        public IEnumerable<DestinationView> Destinations { get; set; }
        public int TotalRecords { get; set; }
    }
    public class DestinationVM
    {
        public IEnumerable<DestinationView> Destinations { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class DestinationSaveModel
    {
        public utblMstDestination Destination { get; set; }
        public IEnumerable<CountryDD> CountryList { get; set; }
        public Cropper cropper { get; set; }
    }
}