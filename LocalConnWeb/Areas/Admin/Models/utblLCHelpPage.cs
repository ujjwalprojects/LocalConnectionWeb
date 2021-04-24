using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblLCHelpPage
    {
        [Key]
        public long HelpPageID { get; set; }
        [Display(Name = "Title")]
        public string HelpPageTitle { get; set; }
        [Display(Name = "Content")]
        public string HelpPageContent { get; set; }
        public string HelpPageImgPath { get; set; }
        [Display(Name = "Contact No")]
        public int HelpPageContactNo { get; set; }
        [Display(Name = "Email ID")]
        public string HelpPageEmailID { get; set; }
    }
}