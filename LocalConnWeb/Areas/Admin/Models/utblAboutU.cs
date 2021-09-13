using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblAboutU
    {
        [Key]
        public long AboutID { get; set; }
        [Display(Name = "AboutUs")]
        public string AboutContent { get; set; }
    }
}