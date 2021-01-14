using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class TermView
    {
        public long TermID { get; set; }
        public string TermName { get; set; }
    }
    public class TermAPIVM
    {
        public IEnumerable<TermView> TermList { get; set; }
        public int TotalRecords { get; set; }
    }
    public class TermVM
    {
        public IEnumerable<TermView> TermList{ get; set; }
        public PagingInfo PagingInfo { get; set; }
    }

    public class SaveTerm
    {
        public utblMstTerm Term { get; set; }
    }
}