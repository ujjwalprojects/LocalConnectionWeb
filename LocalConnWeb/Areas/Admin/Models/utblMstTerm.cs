using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LocalConnWeb.Areas.Admin.Models
{
    public class utblMstTerm
    {
        public long TermID { get; set; }
        [Required(ErrorMessage="Enter Term Name")]
        [Display(Name="Term Name")]
        public string TermName { get; set; }
    }
}