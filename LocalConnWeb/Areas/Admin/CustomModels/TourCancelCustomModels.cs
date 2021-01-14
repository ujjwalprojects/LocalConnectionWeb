using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class TourCancelView
    {
        public long CancellationID { get; set; }
        public string CancellationDesc { get; set; }
    }

    public class TourCancelAPIVM
    {
        public IEnumerable<utblMstTourCancellation> tourCancelList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class TourCancelVM
    {
          public IEnumerable<utblMstTourCancellation> tourCancelList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class TourCancelSave
    {
        public utblMstTourCancellation saveTourCancel { get; set; }
    }
}