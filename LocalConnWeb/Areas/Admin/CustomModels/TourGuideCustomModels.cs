using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Helpers;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class TourGuideAPIVM
    {
        public IEnumerable<TourGuideListView> TourGuides { get; set; }
        public int TotalRecords { get; set; }
    }
    public class TourGuideVM
    {
        public IEnumerable<TourGuideListView> TourGuides { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class TourGuideListView
    {
        public long TourGuideID { get; set; }
        public string TourGuideName { get; set; }
        public string TourGuideLink { get; set; }
    }
}