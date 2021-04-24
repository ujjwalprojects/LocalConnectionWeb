using LocalConnWeb.Areas.Admin.Models;
using LocalConnWeb.Helpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.CustomModels
{
    public class HelpPageView
    {
        public long HelpPageID { get; set; }
        public string HelpPageTitle { get; set; }
        public string HelpPageContent { get; set; }
        public string HelpPageImgPath { get; set; }
        public string HelpPageContactNo { get; set; }
        public string HelpPageEmailID { get; set; }
    }
    public class HelpPageAPIVM
    {
        public IEnumerable<HelpPageView> HelpPage { get; set; }
        public int TotalRecords { get; set; }
    }
    public class HelpPageVM
    {
        public IEnumerable<HelpPageView> HelpPageList { get; set; }
        public PagingInfo PagingInfo { get; set; }
    }
    public class HelpPageSaveModel
    {
        public utblLCHelpPage HelpPagesave { get; set; }
        public Cropper cropper { get; set; }
    }
}