using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstPackageType
    {
        [Key]
        public long PackageTypeID { get; set; }
        [Required(ErrorMessage="Enter Package Name")]
        [Display(Name="Enter Package Name")]
        public string PackageTypeName { get; set; }
        public bool IsVisible { get; set; }
    }
}