using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblTourPackageImage
    {
        public long PackageImageID { get; set; }
        [Required]
        public long PackageID { get; set; }
        public string PhotoThumbPath { get; set; }
        public string PhotoNormalPath { get; set; }
        [Display(Name="Image Caption")]
        public string PhotoCaption { get; set; }
        public bool IsPackageCover { get; set; }
    }
}