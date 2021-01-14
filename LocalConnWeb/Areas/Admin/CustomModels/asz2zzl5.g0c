using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using TourTravel_Web.Helpers;
using TourTravel_Web.Areas.Admin.Models;
namespace TourTravel_Web.Areas.Admin.CustomModels
{
    public class InclusionView
    {
        public long InclusionID { get; set; }
        public string InclusionName { get; set; }
        public string InclusionType { get; set; }
    }

    public class InclusionAPIVM
    {
        public IEnumerable<InclusionView> InclusionList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class InclusionVM
    {
        public IEnumerable<InclusionView> InclusionList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class InclusionSave
    {
        public utblMstInclusion Inclusion { get; set; }
    }
}