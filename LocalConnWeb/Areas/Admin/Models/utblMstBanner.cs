using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstBanner
    {

        public long BannerID { get; set; }
        [Required(ErrorMessage="Select Banner Image")]
        [Display(Name="Banner")]
        public string BannerPath { get; set; }
    }
}