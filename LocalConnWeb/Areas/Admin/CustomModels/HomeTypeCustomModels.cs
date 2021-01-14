using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class HomeTypeView
    {
        public long HomeTypeID { get; set; }
        public string HomeTypeName { get; set; }
    }
    public class HomeTypeAPIVM
    {
        public IEnumerable<HomeTypeView> HomeTypes { get; set; }
        public int TotalRecords { get; set; }
    }
    public class HomeTypeVM
    {
        public IEnumerable<HomeTypeView> HomeTypes { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class HomeTypeSaveModel
    {
        public utblLCMstHomeType HomeTypes { get; set; }
    }
    public class HomeTypeDD
    {
        public long HomeTypeID { get; set; }
        public string HomeTypeName { get; set; }
    }
}