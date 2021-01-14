using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstExclusion
    {
        public long ExclusionID { get; set; }
        [Display(Name="Exclusion")]
        [Required(ErrorMessage="Enter Exclusion Name")]
        public string ExclusionName { get; set; }
    }
}