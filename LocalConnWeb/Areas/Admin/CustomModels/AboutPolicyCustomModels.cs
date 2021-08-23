using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class AboutPolicyApiVM
    {
        public utblAboutU AboutUs { get; set; }

        public IEnumerable<PolicyPointView> PolicyPointView { get; set; }
        public IEnumerable<utblPolicie> PolicyList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class PolicyVM
    {
        public IEnumerable<PolicyPointView> PolicyPointView { get; set; }
        public IEnumerable<utblPolicie> PolicyList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class PolicyPointView
    {
        public long PolicyPointID { get; set; }
        public long PolicyID { get; set; }
        public string PolicyTitle { get; set; }
        public string PolicyPoints { get; set; }
    }

    public class PolicyPtSaveModel
    {
        public utblPolicie Policy { get; set; }
        public utblPolicyPoint PolicyPt { get; set; }
        public IEnumerable<utblPolicie> PolicyDD { get; set; }
    }
}