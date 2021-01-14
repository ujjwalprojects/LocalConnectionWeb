using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstInclusion
    {
        public long InclusionID { get; set; }
        [Required(ErrorMessage="Enter Inclusion Name")]
        [Display(Name="Inclusion Name")]
        public string InclusionName { get; set; }
        [Required(ErrorMessage = "Enter Inclusion Type")]
        [Display(Name = "Inclusion Type")]
        public string InclusionType { get; set; }
    }
}