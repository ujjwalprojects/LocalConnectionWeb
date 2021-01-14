using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Helpers;
using LocalConnWeb.Areas.Admin.Models;
namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class ExclusionView
    {
        public long ExclusionID { get; set; }
        public string ExclusionName { get; set; }
    }

    public class ExclusionAPIVM
    {
        public IEnumerable<ExclusionView> ExclusionList{ get; set; }
        public int TotalRecords { get; set; }
    }
    public class ExclusionVM
    {
        public IEnumerable<ExclusionView> ExclusionList{ get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class ExclusionSave
    {
        public utblMstExclusion Exclusion{ get; set; }
    }

}